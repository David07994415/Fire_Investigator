﻿using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Areas.BackStage.Filter;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class MasterController : Controller
    {
        private DbModel db = new DbModel();

        private const int DataSizeInPage = 2;   //設定一頁幾筆

        // GET: BackStage/Master
        //[CheckPremission("P02")]
        public ActionResult Index(int? page,string MasterName)    // 包含搜尋和分頁
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            var matchedRecords = db.Master.AsQueryable();

            if (MasterName != null)    // 有搜尋 string 的資料
            {
                matchedRecords = matchedRecords.Where(x => x.Name.Contains(MasterName));
            }

            var ToPageRecords = matchedRecords.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage);
            
            if (ToPageRecords.TotalItemCount != 0)   // 搜尋後資料庫內有資料
            {
                return View(ToPageRecords);
            }
            else     // 搜尋後資料庫內沒有資料
            {
                ViewBag.ErrorMassage = "沒有找到資料！"; // 提供所有資料
                return View(db.Master.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
            }
        }

        // GET: BackStage/Master/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: BackStage/Master/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase PhotoFile, CreateMasterProfileViewModel Profile)
        {
            string UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            if (ModelState.IsValid)
            {
                Master NewCreateMaster = new Master();
                NewCreateMaster.Name = Profile.Name;
                NewCreateMaster.Ocupation = Profile.Ocupation;
                NewCreateMaster.CreateUser = UserId;
                NewCreateMaster.UpdateUser = UserId;
                NewCreateMaster.CreateTime = DateTime.Now;
                NewCreateMaster.UpdateTime = DateTime.Now;
                NewCreateMaster.IsShow = Profile.IsShow;

                db.Master.Add(NewCreateMaster);
                db.SaveChanges();
                int newId = NewCreateMaster.Id;

                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    string fileType = PhotoFile.ContentType;
                    string fileName = Path.GetFileName(PhotoFile.FileName);
                    string fileExtent = Path.GetExtension(PhotoFile.FileName);
                    if (fileExtent == ".png" || fileExtent == ".jpg")
                    {
                        var MasterData = db.Master.FirstOrDefault(x => x.Id == newId);
                        MasterData.PhotoPath = fileName;
                        MasterData.UpdateUser = UserId;
                        MasterData.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/master/"); // 將檔案保存到 "Uploads" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        PhotoFile.SaveAs(uploadPath);
                        return RedirectToAction("Edit", new { id = newId });
                    }
                    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
                    return RedirectToAction("Edit", new { id = newId });
                }
                ModelState.AddModelError("PhotoPath", "檔案為空");
                return RedirectToAction("Edit", new { id = newId });
            }
            else 
            {
                ModelState.AddModelError("", "必填");
                return View();
            }
        }

        // GET: BackStage/Master/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,EditMasterProfileViewModel EditProfile, HttpPostedFileBase PhotoFile)
        {
            Master MasterData = db.Master.FirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                MasterData.UpdateTime = DateTime.Now;
                MasterData.UpdateUser= UserId;
                MasterData.Name = EditProfile.Name;
                MasterData.Ocupation = EditProfile.Ocupation;
                MasterData.PersonCkContent = EditProfile.PersonCkContent;
                MasterData.IsShow= EditProfile.IsShow;
                db.SaveChanges();

                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    string fileType = PhotoFile.ContentType;
                    string fileName = Path.GetFileName(PhotoFile.FileName);
                    string fileExtent = Path.GetExtension(PhotoFile.FileName);
                    if (fileExtent == ".png" || fileExtent == ".jpg")
                    {
                        var MasterDataRe = db.Master.FirstOrDefault(x => x.Id == id);
                        MasterDataRe.PhotoPath = fileName;
                        MasterDataRe.UpdateUser = UserId;
                        MasterDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/master/"); // 將檔案保存到 "Uploads" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        PhotoFile.SaveAs(uploadPath);
                        return RedirectToAction("Index"); // 新增成功(有檔案，返回Index列表)
                    }
                    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
                    return View(MasterData);
                }
                return RedirectToAction("Index"); // 新增成功(無檔案，返回Index列表)
            }
            ModelState.AddModelError("", "必填");
            return View(MasterData);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UploadPhoto(HttpPostedFileBase PhotoFile)
        //{
        //    String UserName = User.Identity.Name;
        //    var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;
        //    int MasterId = Convert.ToInt32(RouteData.Values["id"].ToString());

        //    if (PhotoFile != null && PhotoFile.ContentLength > 0)
        //    {
        //        // 確認檔案的型別
        //        string fileType = PhotoFile.ContentType;
        //        string fileName = Path.GetFileName(PhotoFile.FileName);
        //        string fileExtent= Path.GetExtension(PhotoFile.FileName);
        //        if (fileExtent == ".png" || fileExtent == ".jpg") 
        //        {
        //            var MasterData=db.Master.FirstOrDefault(x => x.Id == MasterId);
        //            MasterData.PhotoPath = fileName;
        //            MasterData.UpdateUser = UserId;
        //            MasterData.UpdateTime=DateTime.Now;
        //            db.SaveChanges();

        //            string targetPath = Server.MapPath("~/Uploads/master/"); // 將檔案保存到 "Uploads" 資料夾中
        //            string uploadPath = Path.Combine(targetPath, fileName);
        //            PhotoFile.SaveAs(uploadPath);
        //            return RedirectToAction("Edit", new { id = MasterId });

        //        }
        //        ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
        //        return RedirectToAction("Edit", new { id = MasterId });
        //        // 執行相應的處理邏輯...

        //        // 返回適當的視圖或其他操作
        //    }
        //    ModelState.AddModelError("PhotoPath", "檔案為空");
        //    return RedirectToAction("Edit", new { id= MasterId });

        //    // 如果檔案無效，返回相應的視圖或其他操作
        //}

        //[HttpPost]
        //public ActionResult UploadPhotockeditor()
        //{
        //    if (Request.Files.Count > 0) 
        //    {
        //        try
        //        {
        //            HttpFileCollectionBase files = Request.Files;
        //            if(files.Count==1)
        //            //for (int i = 0; i < files.Count; i++)
        //            {
        //                HttpPostedFileBase file = files[0];//files[i];
        //                string fileName = Path.GetFileName(file.FileName);
        //                string fileExtent = Path.GetExtension(file.FileName);
        //                if (fileExtent == ".png" || fileExtent == ".jpg")
        //                {
        //                    string targetPath = Server.MapPath("~/Uploads/master/"); // 將檔案保存到 "Uploads" 資料夾中
        //                    string uploadPath = Path.Combine(targetPath, fileName);
        //                    file.SaveAs(uploadPath);
        //                    return Json(new { uploaded = true, url = $"/Uploads/master/{fileName}", JsonRequestBehavior.AllowGet });
        //                }
        //                return Json(new { uploaded = true, JsonRequestBehavior.AllowGet });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { uploaded = true, error =ex.Message, JsonRequestBehavior.AllowGet }); 
        //        }
        //    }
        //    return Json(new { uploaded = true, error = "沒有檔案", JsonRequestBehavior.AllowGet });
        //    //if (PhotoFile != null && PhotoFile.ContentLength > 0)
        //    //{
        //    //    // 確認檔案的型別
        //    //    string fileType = PhotoFile.ContentType;
        //    //    string fileName = Path.GetFileName(PhotoFile.FileName);
        //    //    string fileExtent = Path.GetExtension(PhotoFile.FileName);
        //    //    if (fileExtent == ".png" || fileExtent == ".jpg")
        //    //    {


        //    //        string targetPath = Server.MapPath("~/Uploads/master/"); // 將檔案保存到 "Uploads" 資料夾中
        //    //        string uploadPath = Path.Combine(targetPath, fileName);
        //    //        PhotoFile.SaveAs(uploadPath);
        //    //        return RedirectToAction("index");

        //    //    }
        //    //    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
        //    //    return RedirectToAction("index");
        //    //    // 執行相應的處理邏輯...

        //    //    // 返回適當的視圖或其他操作
        //    //}
        //    //ModelState.AddModelError("PhotoPath", "檔案為空");
        //    //return RedirectToAction("index");


        //}



        // GET: BackStage/Master/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Master.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: BackStage/Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Master.Find(id);
            db.Master.Remove(master);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
