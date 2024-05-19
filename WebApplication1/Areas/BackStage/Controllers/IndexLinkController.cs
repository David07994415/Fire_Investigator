using System;
using System.Collections.Generic;
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
    public class IndexLinkController : Controller
    {
        private DbModel db = new DbModel();

        public ActionResult Index()
        {
            var LinkData = db.IndexLink.ToList();
            return View(LinkData);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBackIndexLinkViewModel Profile, HttpPostedFileBase UploadPhoto)
        {
            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                IndexLink NewCreateIndexLink = new IndexLink();
                NewCreateIndexLink.LinkName = Profile.LinkName;
                NewCreateIndexLink.LinkPath = Profile.LinkPath;
                NewCreateIndexLink.IsShow = Profile.IsShow;
                NewCreateIndexLink.UpdateUser = UserId;
                NewCreateIndexLink.CreateUser = UserId;
                NewCreateIndexLink.UpdateTime = DateTime.Now;
                NewCreateIndexLink.CreateTime = DateTime.Now;

                db.IndexLink.Add(NewCreateIndexLink);
                db.SaveChanges();
                int IndexLinkId = NewCreateIndexLink.Id;

                if (UploadPhoto != null && UploadPhoto.ContentLength > 0)
                {
                    string fileType = UploadPhoto.ContentType;
                    string fileName = Path.GetFileName(UploadPhoto.FileName);
                    string fileExtent = Path.GetExtension(UploadPhoto.FileName);
                    if (fileExtent == ".jpg" || fileExtent == ".png" || fileExtent == ".jpeg")
                    {
                        var LinkData = db.IndexLink.FirstOrDefault(x => x.Id == IndexLinkId);
                        LinkData.PhotoPath = fileName;
                        LinkData.UpdateUser = UserId;
                        LinkData.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/IndexLink/");
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadPhoto.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("UploadPhoto", "檔案不吻合格式");
                    return RedirectToAction("Edit", "IndexLink", new { id = IndexLinkId });

                }
                ModelState.AddModelError("UploadPhoto", "檔案為空");
                return RedirectToAction("Edit", "IndexLink", new { id = IndexLinkId });
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
            IndexLink LinkData = db.IndexLink.Find(id);
            if (LinkData == null)
            {
                return RedirectToAction("Index");
            }
            return View(LinkData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateBackIndexLinkViewModel Profile, HttpPostedFileBase UploadPhoto)
        {
            var LinkUpdateData = db.IndexLink.FirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                LinkUpdateData.LinkName = Profile.LinkName;
                LinkUpdateData.LinkPath = Profile.LinkPath;
                LinkUpdateData.IsShow = Profile.IsShow;
                LinkUpdateData.UpdateUser = UserId;
                LinkUpdateData.UpdateTime = DateTime.Now;
                db.SaveChanges();

                if (UploadPhoto != null && UploadPhoto.ContentLength > 0)
                {
                    string fileType = UploadPhoto.ContentType;
                    string fileName = Path.GetFileName(UploadPhoto.FileName);
                    string fileExtent = Path.GetExtension(UploadPhoto.FileName);
                    if (fileExtent == ".jpg" || fileExtent == ".png" || fileExtent == ".jpeg")
                    {
                        var LinkUpdateDataRe = db.IndexLink.FirstOrDefault(x => x.Id == id);
                        LinkUpdateDataRe.PhotoPath = fileName;
                        LinkUpdateDataRe.UpdateUser = UserId;
                        LinkUpdateDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/IndexLink/");
                        string uploadPath = Path.Combine(targetPath, fileName);
                        UploadPhoto.SaveAs(uploadPath);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("UploadPhoto", "檔案不吻合格式");
                        return View(LinkUpdateData);
                    }
                }
                else
                {
                    var UpdateLinkDataCheck = db.IndexLink.FirstOrDefault(x => x.Id == id);
                    if (UpdateLinkDataCheck.PhotoPath == null)
                    {
                        ModelState.AddModelError("UploadPhoto", "尚未上傳檔案");
                        return View(UpdateLinkDataCheck);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "必填");
                return View(LinkUpdateData);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var LinkData = db.IndexLink.Find(id);
            if (LinkData != null)
            {
                return View(LinkData);
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
            var LinkData = db.IndexLink.Find(id);
            string PhotoName = LinkData.PhotoPath;
            if (!string.IsNullOrEmpty(PhotoName))
            {
                string targetPath = Server.MapPath("~/Uploads/IndexLink/");
                string TargetPath = Path.Combine(targetPath, PhotoName);
                if (System.IO.File.Exists(TargetPath))
                {
                    System.IO.File.Delete(TargetPath);
                }
            }
            db.IndexLink.Remove(LinkData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}