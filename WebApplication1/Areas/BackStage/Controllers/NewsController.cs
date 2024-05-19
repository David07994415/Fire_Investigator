using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using MvcPaging;
using System.IO;
using WebApplication1.Models.ViewModels;
using WebApplication1.Filter;
using WebApplication1.Areas.BackStage.Filter;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class NewsController : Controller
    {
        private DbModel db = new DbModel();
        private const int DataSizeInPage = 2;   //設定一頁幾筆

        // GET: BackStage/News
        public ActionResult Index(int? page, string TitleSearch, string CreatorSearch, DateTime? StartTimeSearch, DateTime? EndTimeSearch)    // 包含搜尋和分頁
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)
            var matchedRecords = db.News.AsQueryable();
            if (!string.IsNullOrEmpty(TitleSearch))         // 有 TitleSearch 的資料
            {
                matchedRecords = matchedRecords.Where(x => x.Title.Contains(TitleSearch));
            }
            if (!string.IsNullOrEmpty(CreatorSearch))    // 有 CreatorSearch 的資料
            {
                int UserId;
                if (int.TryParse(CreatorSearch, out UserId))
                {
                    matchedRecords = matchedRecords.Where(x => x.UpdateUser == UserId);
                }
            }
            if (StartTimeSearch != null)    // 有 StartTimeSearch 的資料
            {
                matchedRecords = matchedRecords.Where(x => x.UpdateTime >= StartTimeSearch);
            }
            if (EndTimeSearch != null)    // 有 EndTimeSearch 的資料
            {
                if (StartTimeSearch != null)
                {
                    if (StartTimeSearch <= EndTimeSearch)
                    {
                        var EndTimeAdd = EndTimeSearch.Value.AddHours(23).AddMinutes(59).AddSeconds(58);
                        matchedRecords = matchedRecords.Where(x => x.UpdateTime <= EndTimeAdd);
                    }
                    else { }   // StartTimeSearch > EndTimeSearch，不要 search EndTimeSearch
                }
                else  // StartTimeSearch = null，僅搜尋 EndTimeSearch
                {
                    matchedRecords = matchedRecords.Where(x => x.UpdateTime <= EndTimeSearch);
                }
            }
            var ToPageRecords = matchedRecords.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage);

            if (ToPageRecords.TotalItemCount != 0) // 搜尋後 資料庫內有資料
            {
                return View(ToPageRecords);
            }
            else     // 搜尋後資料庫內沒有資料
            {
                ViewBag.ErrorMassage = "沒有找到資料";   // 給所有資料
                return View(db.News.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
            }
        }


        // GET: BackStage/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: BackStage/News/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase PhotoFile, CreateBackNewsViewModel Profile)
        {
            String UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            if (ModelState.IsValid)
            {
                News NewCreateNews = new News();
                NewCreateNews.Title = Profile.Title;
                NewCreateNews.IsTop = Profile.IsTop;
                NewCreateNews.IsShow = false;  // 預設
                NewCreateNews.IssueTime = Profile.IssueTime;
                NewCreateNews.UpdateUser = UserId;
                NewCreateNews.CreateUser = UserId;
                NewCreateNews.UpdateTime = DateTime.Now;
                NewCreateNews.CreateTime = DateTime.Now;

                db.News.Add(NewCreateNews);
                db.SaveChanges();
                int newId = NewCreateNews.Id;

                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    // 確認檔案的型別
                    string fileType = PhotoFile.ContentType;
                    string fileName = Path.GetFileName(PhotoFile.FileName);
                    string fileExtent = Path.GetExtension(PhotoFile.FileName);
                    if (fileExtent == ".png" || fileExtent == ".jpg")
                    {
                        var NewsData = db.News.FirstOrDefault(x => x.Id == newId);
                        NewsData.PhotoPath = fileName;
                        NewsData.UpdateUser = UserId;
                        NewsData.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/news/");
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

        // GET: BackStage/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: BackStage/News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditBackNewsViewModel EditProfile, HttpPostedFileBase PhotoFile)
        {
            News NewsData = db.News.FirstOrDefault(x => x.Id == id);

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                NewsData.IssueTime = EditProfile.IssueTime;
                NewsData.UpdateTime = DateTime.Now;
                NewsData.UpdateUser = UserId;
                NewsData.Title = EditProfile.Title;
                NewsData.NewsCkContent = EditProfile.NewsCkContent;
                NewsData.IsShow = EditProfile.IsShow;
                NewsData.IsTop = EditProfile.IsTop;
                db.SaveChanges();

                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    string fileType = PhotoFile.ContentType;
                    string fileName = Path.GetFileName(PhotoFile.FileName);
                    string fileExtent = Path.GetExtension(PhotoFile.FileName);
                    if (fileExtent == ".png" || fileExtent == ".jpg")
                    {
                        var NewsDataRe = db.News.FirstOrDefault(x => x.Id == id);
                        NewsDataRe.PhotoPath = fileName;
                        NewsDataRe.UpdateUser = UserId;
                        NewsDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/news/"); // 將檔案保存到 "Uploads" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        PhotoFile.SaveAs(uploadPath);
                        return RedirectToAction("Index"); // 新增成功(有檔案，返回Index列表)
                    }
                    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
                    return View(NewsData);
                }
                return RedirectToAction("Index"); // 新增成功(無檔案，返回Index列表)
            }
            ModelState.AddModelError("", "必填");
            return View(NewsData);
        }

        // GET: BackStage/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: BackStage/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
