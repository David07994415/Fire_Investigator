using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AboutController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        public ActionResult Index()
        {
            string controllerName = RouteData.Values["controller"].ToString();
            var HtmlContent = db.Directory.FirstOrDefault(x => x.Value == controllerName).WebContentTable.FirstOrDefault();
            return View(HtmlContent);
        }
    }
}