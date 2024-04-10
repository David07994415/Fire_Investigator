using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class MasterController : Controller
    {
        private DbModel db = new DbModel();

        private const int DataSizeInPage = 2;   //設定一頁幾筆

        // GET: BackStage/Master
        public ActionResult Index(int? page,string nameName)    // 包含搜尋和分頁
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            if (nameName != null)    // 有搜尋 string 的資料
            {
                var matchedRecords = db.Master.Where(x => x.Name.Contains(nameName)).OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage);
                if (matchedRecords.TotalItemCount != 0) // 搜尋後資料庫內有資料
                {
                    ViewBag.Count = matchedRecords.TotalItemCount;
                    return View(matchedRecords);
                }
                else     // 搜尋後資料庫內沒有資料
                {
                    ViewBag.ErrorMassage = "沒有找到資料";
                    ViewBag.Count = db.Master.Count();
                    return View(db.Master.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
                }
            }
            else                                 // 沒有搜尋 string 的資料
            {
                ViewBag.Count = db.Master.Count();
                return View(db.Master.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
            }


            //ViewBag.Count = db.Master.Count();   //總資料筆數
            ////返回結果.ToPageList(現在第幾頁,一頁幾筆)
            //return View(db.Master.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));


           // return View(db.Master.ToList());
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

        // POST: BackStage/Master/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Ocupation,PhotoPath,PersonCkContent,UpdateUser,UpdateTime,CreateUser,CreateTime")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Master.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master);
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

        // POST: BackStage/Master/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Ocupation,PhotoPath,PersonCkContent,UpdateUser,UpdateTime,CreateUser,CreateTime")] Master master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master);
        }

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
