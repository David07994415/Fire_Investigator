using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Filter;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{

    [AddLayoutBreadcrumb("controller")]
    [AddLayoutSidebar("controller")]
    [AddLayoutMenu]
    public class Member_LoginController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Member_Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FrontMemberLogin LoginInput)
        {
            if (ModelState.IsValid)
            {
                var account = db.Member.Where(x => x.Account == LoginInput.AccountName)?
                                        .Where(x=>x.IdCat== IdentityCategory.FrontOnly || x.IdCat == IdentityCategory.Both)?
                                        .Where(x=>x.IsApproved==true)?.FirstOrDefault();
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
                        return RedirectToAction("Index", "Member_Bulletin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "登入失敗，請重新登入");
                        return View();
                    }
                }
                ModelState.AddModelError("", "登入失敗，請重新登入或確認帳號是否已經開通");
                return View();
            }
            ModelState.AddModelError("", "登入失敗，請重新登入");
            return View();
        }
    }
}