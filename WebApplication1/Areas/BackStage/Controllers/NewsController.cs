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

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class NewsController : Controller
    {
        private DbModel db = new DbModel();
        private const int DataSizeInPage = 2;   //設定一頁幾筆

        // GET: BackStage/News
        public ActionResult Index(int? page, string SearchString)    // 包含搜尋和分頁
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            if (SearchString != null)    // 有搜尋 string 的資料
            {
                var matchedRecords = db.News.Where(x => x.Title.Contains(SearchString) || x.NewsCkContent.Contains(SearchString))// or的搜尋
                                                                                                                      //.Where(x=> x.Ocupation.Contains(nameName))  //這樣會變成 and 搜尋
                    .OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage);
                if (matchedRecords.TotalItemCount != 0) // 搜尋後資料庫內有資料
                {
                    ViewBag.Count = matchedRecords.TotalItemCount;
                    return View(matchedRecords);
                }
                else     // 搜尋後資料庫內沒有資料
                {
                    ViewBag.ErrorMassage = "沒有找到資料";
                    ViewBag.Count = db.News.Count();
                    return View(db.News.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
                }
            }
            else                                 // 沒有搜尋 string 的資料
            {
                ViewBag.Count = db.News.Count();
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

        // GET: BackStage/News/Create
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
                NewCreateNews.IssueTime = Profile.IssueTime;
                NewCreateNews.UpdateUser = UserId;
                NewCreateNews.CreateUser = UserId;
                NewCreateNews.UpdateTime = DateTime.Now;
                NewCreateNews.UpdateTime = DateTime.Now;

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

                        string targetPath = Server.MapPath("~/Uploads/news/"); // 將檔案保存到 "Uploads" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        PhotoFile.SaveAs(uploadPath);
                        return RedirectToAction("Edit", new { id = newId });

                    }
                    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
                    return RedirectToAction("Edit", new { id = newId });
                    // 執行相應的處理邏輯...

                    // 返回適當的視圖或其他操作
                }
                ModelState.AddModelError("PhotoPath", "檔案為空");
                return RedirectToAction("Edit", new { id = newId });

                // 如果檔案無效，返回相應的視圖或其他操作
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
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Edit(int id, EditBackNewsViewModel EditProfile, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                String UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;
                int NewsId = Convert.ToInt32(RouteData.Values["id"].ToString());

                News NewsData = db.News.FirstOrDefault(x => x.Id == id);
                NewsData.UpdateTime = DateTime.Now;
                NewsData.UpdateUser = UserId;
                NewsData.Title = EditProfile.Title;
                NewsData.NewsCkContent = EditProfile.NewsCkContent;
                NewsData.IsShow = EditProfile.IsShow;
                NewsData.IsTop = EditProfile.IsTop;
                db.SaveChanges();

                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    // 確認檔案的型別
                    string fileType = PhotoFile.ContentType;
                    string fileName = Path.GetFileName(PhotoFile.FileName);
                    string fileExtent = Path.GetExtension(PhotoFile.FileName);
                    if (fileExtent == ".png" || fileExtent == ".jpg")
                    {
                        var NewsDataRe = db.News.FirstOrDefault(x => x.Id == NewsId);
                        NewsDataRe.PhotoPath = fileName;
                        NewsDataRe.UpdateUser = UserId;
                        NewsDataRe.UpdateTime = DateTime.Now;
                        db.SaveChanges();

                        string targetPath = Server.MapPath("~/Uploads/news/"); // 將檔案保存到 "Uploads" 資料夾中
                        string uploadPath = Path.Combine(targetPath, fileName);
                        PhotoFile.SaveAs(uploadPath);
                        return RedirectToAction("Edit", new { id = NewsId });

                    }
                    ModelState.AddModelError("PhotoPath", "檔案不吻合格式");
                    return RedirectToAction("Edit", new { id = NewsId });
                    // 執行相應的處理邏輯...

                    // 返回適當的視圖或其他操作
                }
                // 如果檔案無效，返回相應的視圖或其他操作

                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
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
