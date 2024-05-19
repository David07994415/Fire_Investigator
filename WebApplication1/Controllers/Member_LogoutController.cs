using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    [MemberAuthFilter("M03")]
    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]
    public class Member_LogoutController : Controller
    {
        // GET: Member_Logout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost,ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index_Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }


    }
}