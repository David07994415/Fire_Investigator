﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
        public ActionResult Edit(int id, string Permission, [Required]bool IsApproved)
        {
            var user = User.Identity.Name;
            var userId = db.Member.FirstOrDefault(x => x.Account == user).Id;

            var member = db.Member.Find(id);
            if (member == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                member.Permission = Permission;
                member.IsApproved = IsApproved;
                member.UpdateTime = DateTime.Now;
                member.UpdateUser = userId;

                TempData["UpdateCompleted"] = true;

                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Back_Premission", new { id = id });
        }


    }
}