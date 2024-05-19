using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Areas.BackStage.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Utility;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class HomeController : AuthController  // 繼承 [Authorize]
    {
        private DbModel db = new DbModel();

        //public DirectoryBackViewModel DirectoryBackLayoutViewData { get; set; }
        //public HomeController()
        //{
        //    this.DirectoryBackLayoutViewData = new DirectoryBackViewModel();   //has property PageTitle
        //    this.DirectoryBackLayoutViewData.DirectoryHTML = DirectoryBackViewModel.GetSideBarDirectoryHtml();
        //    this.ViewBag.DirectoryHTML = this.DirectoryBackLayoutViewData.DirectoryHTML;
        //}
        // Return PartialMenuView

        //public ActionResult _PartialMenuView()
        //{
        //    string username = User.Identity.Name;
        //    var user = db.Member.Where(x => x.Account == username)?.FirstOrDefault();
        //    if (user == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        string permissionString = user.Permission;
        //        string[] permissionArray = permissionString.Split(',');
        //        string HTMLmenu = DirectoryBackViewModel.GetSideBarDirectoryHtml(permissionArray);
        //        ViewBag.HHH = HTMLmenu;
        //        return PartialView();
        //        //ViewBag.DirectoryMenuHTML = HTMLmenu;
        //    } 
        //}

        [AddBackLayoutComponent]
        public ActionResult Index()  // GET: BackStage/Home/Index
        {
            string username = User.Identity.Name;
            var user = db.Member.Where(x => x.Account == username)?.FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            else 
            {
                string permissionString = user.Permission;
                string[] permissionArray=permissionString.Split(',');
                string HTMLmenu=DirectoryBackViewModel.GetSideBarDirectoryHtml(permissionArray);
                ViewBag.DirectoryMenuHTML = HTMLmenu;
            }
            return View();
        }

        // GET: BackStage/Home/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // Post: BackStage/Home/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Account,Password")] LoginBackViewModel LoginInput)
        {
            if (ModelState.IsValid)
            {
                var account = db.Member.Where(x => x.Account == LoginInput.Account)?.FirstOrDefault();
                if (account != null)
                {
                    string userPasswordInput = LoginInput.Password;
                    string saltInDB = account.Salt;
                    byte[] saltInDBtoArray = Convert.FromBase64String(saltInDB);
                    byte[] HashArray = Encrypt.HashPassword(userPasswordInput, saltInDBtoArray);
                    string Hashstring = Convert.ToBase64String(HashArray);
                    if (Hashstring == account.Password)
                    {
                        FormsAuthentication.SetAuthCookie(account.Account, false); // 快速設定
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "登入失敗，請重新登入");
                        return View();
                    }
                }
                ModelState.AddModelError("", "登入失敗，請重新登入");
                return View();
            }
            ModelState.AddModelError("", "登入失敗，請重新登入");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        //[AllowAnonymous] 
        //public ActionResult Register()
        //{
        //    return View();
        //}


        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register([Bind(Include = "Account,Password,PasswordAgain,NickName")] RegisterBackViewModel RegisterInput)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (RegisterInput.Password != RegisterInput.PasswordAgain) 
        //        {
        //            ModelState.AddModelError("Password", "帳號不同，請再次確認！");
        //            return View();
        //        }

        //        var User = db.Member.Any(x => x.Account == RegisterInput.Account);
        //        if (User == false)  // 帳號未曾註冊過
        //        {
        //            string userPasswordInput = RegisterInput.Password;
        //            byte[] saltArray = Encrypt.CreateSalt();
        //            byte[] passwordArray = Encrypt.HashPassword(userPasswordInput, saltArray);
        //            string saltString = Convert.ToBase64String(saltArray);
        //            string passwordString = Convert.ToBase64String(passwordArray);

        //            var register = new Member
        //            {
        //                Account = RegisterInput.Account,
        //                Password = passwordString,
        //                Salt = saltString,
        //                Guid = Guid.NewGuid(),
        //                NickName = RegisterInput.NickName
        //            };
        //            db.Member.Add(register);
        //            db.SaveChanges();

        //            return RedirectToAction("Login");
        //        }
        //        else 
        //        {
        //            ModelState.AddModelError("Account", "帳號已存在，請再次確認！");
        //            return View();
        //        }
        //    }
        //    ModelState.AddModelError("", "註冊失敗，請再次確認！");
        //    return View();
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
