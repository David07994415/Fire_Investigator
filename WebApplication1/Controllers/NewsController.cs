using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NewsController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        // GET: News
        public ActionResult Index(int? id, int? page)
        {
            const int DataSizeInPage = 2;   //設定一頁幾筆
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            if (id != null)  // 如果有路由有指定Id => 要看 News Detail
            {
                var NewsDetail = db.News.Where(x => x.Id == id && x.IsShow == true)?.FirstOrDefault();
                if (NewsDetail != null)  // 如果資料庫有 Id
                {
                    return View("NewsDetail", "_LayoutPage", NewsDetail);  // 返回至新的 View => NewsDeatil View
                }
                else                           // 如果資料庫內沒有Id
                {
                    return RedirectToAction("Index", "News");  // 返回至原本 News 網址
                }
            }
            else     // 如果有路由沒有指定 Id=> 要看 News 總覽 (有包含分頁)
            {
                var NewsList = db.News.Where(x => x.IsShow == true).ToList();
                ViewBag.Count = NewsList.Count();
                return View(db.News.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
            }
        }
    }
}