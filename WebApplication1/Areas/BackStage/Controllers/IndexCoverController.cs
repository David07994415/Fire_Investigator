using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class IndexCoverController : Controller
    {
        private DbModel db = new DbModel();

        public ActionResult Index()
        {
            var CoverData = db.IndexCover.ToList();
            return View(CoverData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBackIndexCoverViewModel Profile, HttpPostedFileBase UploadPhoto)
        {
            string UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            if (ModelState.IsValid)
            {
                IndexCover NewCreateIndexCover = new IndexCover();
                NewCreateIndexCover.CoverName = Profile.CoverName;
                NewCreateIndexCover.IsShow = Profile.IsShow;
                NewCreateIndexCover.UpdateUser = UserId;
                NewCreateIndexCover.CreateUser = UserId;
                NewCreateIndexCover.UpdateTime = DateTime.Now;
                NewCreateIndexCover.CreateTime = DateTime.Now;

                db.IndexCover.Add(NewCreateIndexCover);
                db.SaveChanges();
                int IndexCoverId = NewCreateIndexCover.Id;

                if (UploadPhoto != null && UploadPhoto.ContentLength > 0)
                {
                    string fileType = UploadPhoto.ContentType;
                    string fileName = Path.GetFileName(UploadPhoto.FileName);
                    string fileExtent = Path.GetExtension(UploadPhoto.FileName);
                    if (fileExtent == ".jpg" || fileExtent == ".png" || fileExtent == ".jpeg")
                    {
                        var CoverData = db.IndexCover.FirstOrDefault(x => x.Id == IndexCoverId);
                        CoverData.PhotoPath = fileName;
                        CoverData.UpdateUser = UserId;
                        CoverData.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/IndexCover/");
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadPhoto.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("UploadPhoto", "檔案不吻合格式");
                    return RedirectToAction("Edit", "IndexCover", new { id= IndexCoverId });

                }
                ModelState.AddModelError("UploadPhoto", "檔案為空");
                return RedirectToAction("Edit", "IndexCover", new { id = IndexCoverId });
            }
            else
            {
                ModelState.AddModelError("", "必填");
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexCover CoverData = db.IndexCover.Find(id);
            if (CoverData == null)
            {
                return RedirectToAction("Index");
            }
            return View(CoverData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBackIndexCoverViewModel Profile, HttpPostedFileBase UploadPhoto)
        {
            var CoverUpdateData = db.IndexCover.FirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                CoverUpdateData.CoverName = Profile.CoverName;
                CoverUpdateData.IsShow = Profile.IsShow;
                CoverUpdateData.UpdateUser = UserId;
                CoverUpdateData.UpdateTime = DateTime.Now;
                db.SaveChanges();

                if (UploadPhoto != null && UploadPhoto.ContentLength > 0)
                {
                    string fileType = UploadPhoto.ContentType;
                    string fileName = Path.GetFileName(UploadPhoto.FileName);
                    string fileExtent = Path.GetExtension(UploadPhoto.FileName);
                    if (fileExtent == ".jpg" || fileExtent == ".png" || fileExtent == ".jpeg")
                    {
                        var CoverUpdateDataRe = db.IndexCover.FirstOrDefault(x => x.Id == id);
                        CoverUpdateDataRe.PhotoPath = fileName;
                        CoverUpdateDataRe.UpdateUser = UserId;
                        CoverUpdateDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/IndexCover/");
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadPhoto.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("UploadPhoto", "檔案不吻合格式");
                        return View(CoverUpdateData);
                    }
                }
                else  // 如果沒有上傳檔案
                {
                    var UpdateCoverDataCheck = db.IndexCover.FirstOrDefault(x => x.Id == id);
                    if (UpdateCoverDataCheck.PhotoPath == null)
                    {
                        ModelState.AddModelError("UploadPhoto", "尚未上傳檔案");
                        return View(UpdateCoverDataCheck);
                    }
                    else  // 如果有存在檔案
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "必填");
                return View(CoverUpdateData);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CoverData = db.IndexCover.Find(id);
            if (CoverData != null)
            {
                return View(CoverData);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var CoverData = db.IndexCover.Find(id);
            string PhotoName = CoverData.PhotoPath;
            if (!string.IsNullOrEmpty(PhotoName))
            {
                string targetPath = Server.MapPath("~/Uploads/IndexCover/");
                string TargetPath = Path.Combine(targetPath, PhotoName);
                if (System.IO.File.Exists(TargetPath))
                {
                    System.IO.File.Delete(TargetPath);
                }
            }
            db.IndexCover.Remove(CoverData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}