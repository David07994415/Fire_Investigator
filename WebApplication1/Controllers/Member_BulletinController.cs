using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{

    [MemberAuthFilter("M03")]
    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]
    public class Member_BulletinController : Controller
    {
        private DbModel db = new DbModel();
        // GET: Member_Bulletin
        public ActionResult Index()
        {
            var bullList = db.Bulletin.ToList();
            return View(bullList);
        }


        public ActionResult Detail(int Id)
        {
            var OneBulletin = db.Bulletin.Find(Id);
            return View(OneBulletin);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBulletinMain BulletinInput)
        {
            if (ModelState.IsValid) 
            {
                var userInfor = HttpContext.User.Identity.Name;
                var userId = db.Member.Where(x => x.Account == userInfor).FirstOrDefault().Id;

                Bulletin NewBulletin=new Bulletin();
                NewBulletin.Theme = BulletinInput.theme;
                NewBulletin.Content = BulletinInput.CkConent;
                NewBulletin.UpdateUser = userId;
                NewBulletin.UpdateTime = DateTime.Now;
                NewBulletin.CreateUser = userId;
                NewBulletin.CreateTime = DateTime.Now;

                db.Bulletin.Add(NewBulletin);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Reply(int Id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(int Id, CreateBulletinMain MessageInput)
        {
            if (ModelState.IsValid)
            {
                var userInfor = HttpContext.User.Identity.Name;
                var userId = db.Member.Where(x => x.Account == userInfor).FirstOrDefault().Id;

                Message NewMessage = new Message();
                NewMessage.Title = MessageInput.theme;
                NewMessage.Content = MessageInput.CkConent;
                NewMessage.UpdateUser = userId;
                NewMessage.UpdateTime = DateTime.Now;
                NewMessage.CreateUser = userId;
                NewMessage.CreateTime = DateTime.Now;
                NewMessage.BulletinId=Id;

                db.Message.Add(NewMessage);
                db.SaveChanges();

                return RedirectToAction("Detail", new { Id });
            }
            else
            {
                return View();
            }
        }


        public ActionResult Edit()
        {
            return View();
        }
    }
}