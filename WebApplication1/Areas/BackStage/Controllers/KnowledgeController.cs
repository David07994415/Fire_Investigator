using Microsoft.Ajax.Utilities;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class KnowledgeController : Controller
    {
        private DbModel db = new DbModel();
        private const int DataSizeInPage = 2;   //設定一頁幾筆

        // GET: BackStage/Knowledges
        public ActionResult Index(int? page, string SearchString)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            if (SearchString != null)    // 有搜尋 string 的資料
            {
                var matchedRecords = db.Knowledge.Where(x => x.Title.Contains(SearchString) || x.FileName.Contains(SearchString))// or的搜尋                        
                    .OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage);
                if (matchedRecords.TotalItemCount != 0) // 搜尋後資料庫內有資料
                {
                    ViewBag.Count = matchedRecords.TotalItemCount;
                    return View(matchedRecords);
                }
                else     // 搜尋後資料庫內沒有資料
                {
                    ViewBag.ErrorMassage = "沒有找到資料";
                    ViewBag.Count = db.Knowledge.Count();
                    return View(db.Knowledge.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
                }
            }
            else                                 // 沒有搜尋 string 的資料
            {
                ViewBag.Count = db.Knowledge.Count();
                return View(db.Knowledge.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
            }
        }

        // GET: BackStage/Knowledges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // GET: BackStage/Knowledges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackStage/Knowledges/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBackKnowledgeViewModel Profile, HttpPostedFileBase UploadFile)
        {
            String UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            if (ModelState.IsValid)
            {
                Knowledge NewCreateKnows = new Knowledge();
                NewCreateKnows.Title = Profile.Title;
                NewCreateKnows.IsTop = Profile.IsTop;
                NewCreateKnows.IsShow = Profile.IsShow;
                NewCreateKnows.IssueTime = Profile.IssueTime;
                NewCreateKnows.UpdateUser = UserId;
                NewCreateKnows.CreateUser = UserId;
                NewCreateKnows.UpdateTime = DateTime.Now;
                NewCreateKnows.CreateTime = DateTime.Now;

                db.Knowledge.Add(NewCreateKnows);
                db.SaveChanges();
                int KnowsId = NewCreateKnows.Id;

                if (UploadFile != null && UploadFile.ContentLength > 0)
                {
                    // 確認檔案的型別
                    string fileType = UploadFile.ContentType;
                    string fileName = Path.GetFileName(UploadFile.FileName);
                    string fileExtent = Path.GetExtension(UploadFile.FileName);
                    if (fileExtent == ".pdf")
                    {
                        var KnowsData = db.Knowledge.FirstOrDefault(x => x.Id == KnowsId);
                        KnowsData.FileName = fileName;
                        KnowsData.UpdateUser = UserId;
                        KnowsData.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/knowledge/"); // 將檔案保存到 "knowledge" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadFile.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("UploadFile", "檔案不吻合格式");
                    return View();
                    // 執行相應的處理邏輯...

                    // 返回適當的視圖或其他操作
                }
                ModelState.AddModelError("UploadFile", "檔案為空");
                return RedirectToAction("Index");
                // 如果檔案無效，返回相應的視圖或其他操作
            }
            else
            {
                ModelState.AddModelError("", "必填");
                return View();
            }
        }

        // GET: BackStage/Knowledges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: BackStage/Knowledges/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,CreateBackKnowledgeViewModel Profile, HttpPostedFileBase UploadFile)
        {
            String UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            if (ModelState.IsValid)
            {
                var UpdateKnowsData = db.Knowledge.FirstOrDefault(x => x.Id == id);
                UpdateKnowsData.Title = Profile.Title;
                UpdateKnowsData.IsTop = Profile.IsTop;
                UpdateKnowsData.IsShow = Profile.IsShow;
                UpdateKnowsData.IssueTime = Profile.IssueTime;
                UpdateKnowsData.UpdateUser = UserId;
                UpdateKnowsData.UpdateTime = DateTime.Now;
                db.SaveChanges();

                if (UploadFile != null && UploadFile.ContentLength > 0)
                {
                    // 確認檔案的型別
                    string fileType = UploadFile.ContentType;
                    string fileName = Path.GetFileName(UploadFile.FileName);
                    string fileExtent = Path.GetExtension(UploadFile.FileName);
                    if (fileExtent == ".pdf")
                    {
                        var KnowsDataRe = db.Knowledge.FirstOrDefault(x => x.Id == id);
                        KnowsDataRe.FileName = fileName;
                        KnowsDataRe.UpdateUser = UserId;
                        KnowsDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/knowledge/");
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadFile.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("UploadFile", "檔案不吻合格式");
                        return View();
                    }
                }
                else
                {
                    var UpdateKnowsCheck = db.Knowledge.FirstOrDefault(x => x.Id == id);
                    if (UpdateKnowsCheck.FileName == null)
                    {
                        ModelState.AddModelError("UploadFile", "尚未上傳檔案");
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "必填");
                return View();
            }
        }

        // GET: BackStage/Knowledges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledge.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: BackStage/Knowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knowledge knowledge = db.Knowledge.Find(id);

            string FileName=knowledge.FileName;
            string targetPath = Server.MapPath("~/Uploads/knowledge/");
            string uploadPath = Path.Combine(targetPath, FileName);
            if (System.IO.File.Exists(uploadPath))
            {
                System.IO.File.Delete(uploadPath);
            }
            db.Knowledge.Remove(knowledge);
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
