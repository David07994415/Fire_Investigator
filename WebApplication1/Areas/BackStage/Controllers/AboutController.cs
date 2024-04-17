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

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class AboutController : Controller
    {
        private DbModel db = new DbModel();
        // GET: BackStage/Master/Edit/5
        public ActionResult Edit()
        {
            string controllerName = RouteData.Values["controller"].ToString();
            var WebContentObj=db.Directory.FirstOrDefault(x=>x.Value== controllerName).WebContentTable.FirstOrDefault();   
            return View(WebContentObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditBackAboutViewModel editBackAboutViewModel)
        {
            if (ModelState.IsValid)
            {
                String UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                string controllerName = RouteData.Values["controller"].ToString();
                var WebContentObj = db.Directory.FirstOrDefault(x => x.Value == controllerName).WebContentTable.FirstOrDefault();

                WebContentObj.HTMLContent = editBackAboutViewModel.HTMLContent;
                WebContentObj.UpdateTime = DateTime.Now;
                WebContentObj.UpdateUser = UserId;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }
    }
}