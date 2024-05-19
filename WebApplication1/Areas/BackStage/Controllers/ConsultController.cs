using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class ConsultController : Controller
    {
        private DbModel db = new DbModel();
        public ActionResult Index()
        {
            string ControllerName = RouteData.Values["Controller"].ToString();
            var BusinessData = db.BusinessCategory.Where(x => x.Name == ControllerName).FirstOrDefault().BussinessTable.FirstOrDefault();
            return View(BusinessData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EditBackBusinessViewModel input)
        {
            string ControllerName = RouteData.Values["Controller"].ToString();
            var BusinessData = db.BusinessCategory.Where(x => x.Name == ControllerName).FirstOrDefault().BussinessTable.FirstOrDefault();

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                string ckcontent = input.Content;
                BusinessData.Content = ckcontent;
                BusinessData.UpdateTime = DateTime.Now;
                BusinessData.UpdateUser = UserId;

                db.SaveChanges();

                TempData["UpdateCompleted"] = true;

                return View(BusinessData);
            }
            else 
            {
                ModelState.AddModelError("Content", "CK Editor必填相關內容");
                return View(BusinessData);
            }
        }
    }
}