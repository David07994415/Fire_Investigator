using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JobController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        public ActionResult Index()
        {
            string ControllerName = RouteData.Values["controller"].ToString();
            var CkContent = db.BusinessCategory
                                                .FirstOrDefault(x => x.Name == ControllerName)
                                                .BussinessTable.FirstOrDefault();

            return View(CkContent);
        }
    }
}