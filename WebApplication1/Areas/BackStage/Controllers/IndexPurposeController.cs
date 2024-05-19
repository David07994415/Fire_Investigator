using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Migrations;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebGrease.Configuration;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class IndexPurposeController : Controller
    {
        private DbModel db = new DbModel();

        // GET: BackStage/IndexPurpose
        public ActionResult Index()
        {
            var PurposeData=db.IndexPurpose.FirstOrDefault();
            return View(PurposeData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index( EditBackIndexPurposeViewModel input)
        {
            string ckcontent = input.HTMLContent;
            var PurposeData = db.IndexPurpose.FirstOrDefault();

            if (ModelState.IsValid)
            {
                string UserName = User.Identity.Name;
                var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;

                PurposeData.HTMLContent = input.HTMLContent;
                PurposeData.UpdateUser = UserId;
                PurposeData.UpdateTime = DateTime.Now;

                db.SaveChanges();

                TempData["UpdateCompleted"] = true;

                return View(PurposeData);
            }
            else
            {
                ModelState.AddModelError("HTMLContent", "CK Editor必填相關內容");
                return View(PurposeData);
            }
        }

    }
}