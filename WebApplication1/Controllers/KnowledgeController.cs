using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication1.Filter;
using WebApplication1.Models;
using MvcPaging;

namespace WebApplication1.Controllers
{
    public class KnowledgeController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]

        // GET: Knowledge
        public ActionResult Index(int? page)
        {
            const int DataSizeInPage = 2;   //設定一頁幾筆
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;   // 現在第幾頁(當前頁面的索引值)

            var KnowkedgeList = db.Knowledge.Where(x => x.IsShow == true).OrderBy(x => x.IsTop).ToList();
            ViewBag.Count = KnowkedgeList.Count();
            return View(db.Knowledge.OrderByDescending(x => x.CreateTime).ToPagedList(currentPageIndex, DataSizeInPage));
        }
    }
}