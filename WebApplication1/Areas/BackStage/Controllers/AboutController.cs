using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;
using System.Web.Routing;
using System.Data.Entity;
using WebApplication1.Areas.BackStage.Filter;
using System.Net.Http;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [AddBackLayoutComponent]
    public class AboutController : Controller
    {
        private DbModel db = new DbModel();
        // GET: BackStage/Master/Edit/5
        public ActionResult Index()
        {
            string controllerName = RouteData.Values["controller"].ToString();
            var WebContentObj=db.Directory.FirstOrDefault(x=>x.Value== controllerName).WebContentTable.FirstOrDefault();   
            return View(WebContentObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EditBackAboutViewModel editBackAboutViewModel)
        {
            string controllerName = RouteData.Values["controller"].ToString();
            var WebContentObj = db.Directory.FirstOrDefault(x => x.Value == controllerName).WebContentTable.FirstOrDefault();

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                WebContentObj.HTMLContent = editBackAboutViewModel.HTMLContent;
                WebContentObj.UpdateTime = DateTime.Now;
                WebContentObj.UpdateUser = UserId;
                db.SaveChanges();

                TempData["UpdateCompleted"] = true;

                return View(WebContentObj);
            }
            else 
            {
                ModelState.AddModelError("HTMLContent", "CK Editor必填相關內容");
                return View(WebContentObj);
            }
        }
    }
}