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
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class HomeController : AuthController  // 繼承 [Authorize]
    {
        private DbModel db = new DbModel();

        // GET: BackStage/Home/Index
        public ActionResult Index()
        {
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
                if (LoginInput.Account == "test" && LoginInput.Password == "test")  // for testing login
                {
                    FormsAuthentication.SetAuthCookie("test", false);
                    return RedirectToAction("Index","Home");
                }
                //    var account=db.Member.Where(x=>x.Account== LoginInput.Account)?.FirstOrDefault();
                //    if (account != null) 
                //    {
                //        string userPasswordInput = LoginInput.Password;
                //        string saltInDB = account.Salt;
                //        byte[] saltInDBtoArray=Convert.FromBase64String(saltInDB);
                //        byte[] HashArray = HashPassword(userPasswordInput, saltInDBtoArray);
                //        string Hashstring = Convert.ToBase64String(HashArray);
                //        if (Hashstring == account.Password)
                //        {
                //            FormsAuthentication.SetAuthCookie(account.Account, false);
                //            RedirectToAction("Index","Home");
                //        }
                //        else
                //        {
                //            ModelState.AddModelError("", "登入失敗，請重新登入");
                //            return View();
                //        }
                //    }
                ModelState.AddModelError("", "登入失敗，請重新登入");
                return View();
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

        // GET: BackStage/Admin/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // Post: BackStage/Admin/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Account,Password,PasswordAgain,NickName")] RegisterBackViewModel RegisterInput)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        // GET: BackStage/Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: BackStage/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackStage/Home/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Password,Salt,NickName,Guid,Permission,CreateTime,UpdateTime")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: BackStage/Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: BackStage/Home/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Password,Salt,NickName,Guid,Permission,CreateTime,UpdateTime")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: BackStage/Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: BackStage/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
