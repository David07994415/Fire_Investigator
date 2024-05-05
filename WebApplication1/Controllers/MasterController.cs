using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MasterController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        // GET: Master
        public ActionResult Index(int? id)
        {
            if (id != null)  // 如果有路由有指定Id => 要看 Master Detail
            {
                var MasterDetail = db.Master.Where(x => x.Id == id && x.IsShow == true)?.FirstOrDefault();
                if (MasterDetail != null)  // 如果資料庫有 Id
                {
                    return View("MasterDetail", "_LayoutPage", MasterDetail);  // 返回至新的 View => MasterDeatil View
                }
                else                           // 如果資料庫內沒有Id
                {
                    return RedirectToAction("Index", "Master");  // 返回至原本 Master 網址
                }
            }
            else     // 如果有路由沒有指定 Id=> 要看 Master 總覽
            {
                var MasterList = db.Master.Where(x => x.IsShow == true).ToList();
                return View(MasterList);
            }
        }
    }
}