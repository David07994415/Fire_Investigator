using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public ActionResult Index(int? id, int? page)
        {
            const int DataSizeInPage = 2;   //設定一頁幾筆
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            if (id != null)  // 如果有路由有指定Id => 要查看 News Detail
            {
                var NewsDetail = db.News.Where(x => x.Id == id)
                                            .Where(x=> x.IsShow == true)
                                            .Where(x => x.IsTop == true)?.FirstOrDefault();

                if (NewsDetail != null)  // 如果有找到資料
                {
                    return View("NewsDetail", "_LayoutPage", NewsDetail);  // 返回至新的 View => NewsDeatil View
                }
                else                              // 如果沒有找到資料
                {
                    return RedirectToAction("Index", "News");  // 返回至原本 News 網址
                }
            }
            else     // 如果路由沒有指定 Id=> 要看 News 總覽 (有包含分頁)
            {
                var NewsDataList = db.News.OrderByDescending(x => x.CreateTime)
                                                    .Where(x => x.IsTop == true)
                                                    .Where(x => x.IsShow == true)
                                                    .ToList();

                for (var i = 0; i < NewsDataList.Count(); i++)  // 進行 正規表達式取代
                {
                    NewsDataList[i].NewsCkContent = Regex.Replace(NewsDataList[i].NewsCkContent, "<.*?>", string.Empty);
                    NewsDataList[i].NewsCkContent = NewsDataList[i].NewsCkContent.Length < 10 ?
                        NewsDataList[i].NewsCkContent + "..." :
                        NewsDataList[i].NewsCkContent.Substring(0, 10) + "...";
                }

                var ToPageRecords = NewsDataList.ToPagedList(currentPageIndex, DataSizeInPage);
                ViewBag.Count = ToPageRecords.TotalItemCount;

                return View(ToPageRecords);
            }
        }
    }
}