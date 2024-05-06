using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{

    [MemberAuthFilter("M03")]
    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]
    public class Member_BulletinController : Controller
    {
        // GET: Member_Bulletin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int Id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}