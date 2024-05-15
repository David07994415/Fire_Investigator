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
                var account = db.Member.Where(x => x.Account == LoginInput.AccountName)?.FirstOrDefault();
                if (account != null)
                {
                    string userPasswordInput = LoginInput.Password;
                    string saltInDB = account.Salt;
                    byte[] saltInDBtoArray = Convert.FromBase64String(saltInDB);
                    byte[] HashArray = HashPassword(userPasswordInput, saltInDBtoArray);
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
                ModelState.AddModelError("", "登入失敗，請重新登入");
                return View();
            }
            return View();
        }

        // Hash 處理加鹽的密碼功能
        //將使用者的密碼和產生的鹽作為輸入，使用 Argon2 演算法執行密碼雜湊
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            //設定之前生成的鹽
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 2; // 迭代運算次數，更高的迭代次數可以提高安全性
            argon2.MemorySize = 1024; // 1 GB，定義演算法要使用的記憶體大小（以位元組為單位）

            //Argon2 演算法產生 16 位元組雜湊並將其作為位元組數組傳回
            return argon2.GetBytes(16);
        }



    }
}