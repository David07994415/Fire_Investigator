﻿using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{

    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]
    public class Member_RegisterController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Member_Register
        public ActionResult Index()
        {
            return View();
        }

        // Post: BackStage/Admin/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FrontMemberRegister RegisterInput)
        {
            if (ModelState.IsValid)
            {
                if (RegisterInput.Password != RegisterInput.PasswordAgain)
                {
                    ModelState.AddModelError("Password", "帳號不同，請再次確認！");
                    return View();
                }

                var User = db.Member.Any(x => x.Account == RegisterInput.Account);
                if (User == false)  // 帳號未曾註冊過
                {
                    string userPasswordInput = RegisterInput.Password;
                    byte[] saltArray = Encrypt.CreateSalt();
                    byte[] passwordArray = Encrypt.HashPassword(userPasswordInput, saltArray);
                    string saltString = Convert.ToBase64String(saltArray);
                    string passwordString = Convert.ToBase64String(passwordArray);

                    var register = new Member
                    {
                        Account = RegisterInput.Account,
                        Password = passwordString,
                        Salt = saltString,
                        Guid = Guid.NewGuid(),
                        NickName = RegisterInput.NickName,
                        IsApproved = false,
                        IdCat = IdentityCategory.FrontOnly,
                        UpdateTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                    };
                    db.Member.Add(register);
                    db.SaveChanges();
                    var registerId = register.Id;

                    var UserUpdate = db.Member.Find(registerId);
                    UserUpdate.UpdateUser = registerId;
                    UserUpdate.CreateUser= registerId;

                    return RedirectToAction("Index", "Member_Login");
                }
                else // 帳號註冊過了
                {
                    ModelState.AddModelError("Account", "帳號已存在，請再次確認！");
                    return View();
                }
            }
            ModelState.AddModelError("", "請填入必要選項！");
            return View();
        }

    }
}