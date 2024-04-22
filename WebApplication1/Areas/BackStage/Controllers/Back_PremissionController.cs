using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Controllers
{
    [Authorize]
    [AddBackLayoutComponent]
    public class Back_PremissionController : Controller
    {
        DbModel db = new DbModel();

        // GET: BackStage/Back_Premission
        public ActionResult Index()
        {
            var memberList = db.Member.ToList();
            return View(memberList);
        }

        [UpdateMemberPremission]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) 
            {
                return RedirectToAction("Index");
            }
            else
            {
                var member = db.Member.Find(id);
                if (member == null)
                {
                    return RedirectToAction("Index");
                }
                return View(member);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string Permission)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var member = db.Member.Find(id);
                if (member == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    member.Permission= Permission;
                    db.SaveChanges();
                }
                return RedirectToAction("Edit", "Back_Premission", new { id=id});
            }
        }


    }
}