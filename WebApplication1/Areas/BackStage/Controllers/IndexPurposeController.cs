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
            string UserName = User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;


            string ckcontent = input.HTMLContent;
            var PurposeData = db.IndexPurpose.FirstOrDefault();

            if (PurposeData == null)
            {
                var PurposeData_New = new IndexPurpose();
                PurposeData_New.CreateUser = UserId;
                PurposeData_New.CreateTime = DateTime.Now;
                PurposeData_New.HTMLContent = ckcontent;
                PurposeData_New.UpdateUser = UserId;
                PurposeData_New.UpdateTime = DateTime.Now;
                db.IndexPurpose.Add( PurposeData_New );
                db.SaveChanges();
                return View(PurposeData_New);
            }
            else
            {
                PurposeData.HTMLContent = ckcontent;
                PurposeData.UpdateUser = UserId;
                PurposeData.UpdateTime = DateTime.Now;
                db.SaveChanges();
                return View(PurposeData);
            }
        }

    }
}