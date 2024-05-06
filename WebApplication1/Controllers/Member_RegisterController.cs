using Konscious.Security.Cryptography;
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
        public ActionResult Register(FrontMemberRegister RegisterInput)
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
                    byte[] saltArray = CreateSalt();
                    byte[] passwordArray = HashPassword(userPasswordInput, saltArray);
                    string saltString = Convert.ToBase64String(saltArray);
                    string passwordString = Convert.ToBase64String(passwordArray);

                    var register = new Member
                    {
                        Account = RegisterInput.Account,
                        Password = passwordString,
                        Salt = saltString,
                        Guid = Guid.NewGuid(),
                        NickName = RegisterInput.NickName
                    };
                    db.Member.Add(register);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Member_Login");
                }
                else
                {
                    ModelState.AddModelError("Account", "帳號已存在，請再次確認！");
                    return View();
                }
            }
            return View();
        }


        // Argon2 加密
        //產生 Salt 功能
        //使用加密安全隨機數產生器 ( ) 產生隨機鹽
        private byte[] CreateSalt()
        {
            //建立一個大小為 16 ( ) 的位元組陣列buffer儲存產生的 salt
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
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