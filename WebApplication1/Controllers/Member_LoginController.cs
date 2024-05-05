using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{

    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]

    public class Member_LoginController : Controller
    {
        // GET: Member_Login
        public ActionResult Index()
        {
            return View();
        }
    }
}