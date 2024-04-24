using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class JobController : Controller
    {
        private DbModel db = new DbModel();
        public ActionResult Index()
        {
            string ControllerName = RouteData.Values["Controller"].ToString();
            var BusinessData = db.BusinessCategory.Where(x => x.Name == ControllerName).FirstOrDefault().BussinessTable.FirstOrDefault();
            return View(BusinessData);
        }
        [HttpPost]
        public ActionResult Index(EditBackBusinessViewModel input)
        {
            string UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

            string ckcontent = input.Content;
            string ControllerName = RouteData.Values["Controller"].ToString();
            var BusinessData = db.BusinessCategory.Where(x => x.Name == ControllerName).FirstOrDefault().BussinessTable.FirstOrDefault();
            BusinessData.Content = ckcontent;
            BusinessData.UpdateTime = DateTime.Now;
            BusinessData.UpdateUser = UserId;

            db.SaveChanges();
            return View(BusinessData);
        }
    }
}