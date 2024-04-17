using MailKit.Net.Smtp;
using Microsoft.Ajax.Utilities;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private DbModel db = new DbModel();

        public DirectoryFrontViewModel DirectoryLayoutViewData { get; set; }

        public HomeController()
        {
            this.DirectoryLayoutViewData = new DirectoryFrontViewModel();   //has property PageTitle
            this.DirectoryLayoutViewData.DirectoryHTML = DirectoryFrontViewModel.GetDirectoryHtml();
            this.ViewBag.DirectoryHTML = this.DirectoryLayoutViewData.DirectoryHTML;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Master(int? id)
        {
            if (id != null)  // 如果有路由有指定Id => 要看 Master Detail
            {
                var MasterDetail=db.Master.Where(x=>x.Id==id)?.FirstOrDefault();
                if(MasterDetail != null)  // 如果資料庫有 Id
                {
                    return View("MasterDetail", "_LayoutPage", MasterDetail);  // 返回至新的 View => MasterDeatil View
                }
                else                           // 如果資料庫內沒有Id
                {
                    return RedirectToAction("Master", "Home");  // 返回至原本 Master 網址
                }
            }
            else     // 如果有路由沒有指定 Id=> 要看 Master 總覽
            {
                var MasterList = db.Master.ToList();
                return View(MasterList);
            }
        }


        public ActionResult _PartialBanner(string action) 
        {
            var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == action).RecursiveId;
            var NodeDirectoryList = db.Directory.Where( x=>x.Id== ParentDirectoryId || x.Value == action).ToList();
            return PartialView(NodeDirectoryList);
        }

        public ActionResult _PartialSideBar(string action)
        {
            var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == action).RecursiveId;
            var NodeDirectoryList = db.Directory.Where(x => x.Id == ParentDirectoryId || x.RecursiveId == ParentDirectoryId).ToList();
            return PartialView(NodeDirectoryList);
        }





        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //string directoryhtml = DirectoryFrontViewModel.GetDirectoryHtml();
            //return View(new HomeFrontViewModel { DirectoryHTML = directoryhtml });
            ////還有其他contact data?

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactFrontViewModel ContactSubmitViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //要進行機器人驗證，可以參考 https://captcha.com/mvc/mvc-captcha.html#mvc5-captcha
                    //看要不要存資料庫
                    //這裡要寄信
                    sendGmail(ContactSubmitViewModel.name, ContactSubmitViewModel.email, ContactSubmitViewModel.content);

                    ModelState.Clear();  // 清空模型中的error數據
                    return View(); //返回沒有清空資料
                }
                else
                {
                    return View(ContactSubmitViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"{ex.Message}");
                return View(ContactSubmitViewModel);
            }

            //return View(ContactSubmitViewModel);
        }
        public static void sendGmail(string name, string mail, string content)
        {
            //宣告使用 MimeMessage
            var message = new MimeMessage();
            //設定發信地址 ("發信人", "發信 email")
            message.From.Add(new MailboxAddress("主信箱", "14rocketback@gmail.com"));
            //設定收信地址 ("收信人", "收信 email")
            message.To.Add(new MailboxAddress(name, mail));
            //寄件副本email
            message.Cc.Add(new MailboxAddress("主信箱CC", "14rocketback@gmail.com"));
            //設定優先權
            //message.Priority = MessagePriority.Normal;
            //信件標題
            message.Subject = "鑑定火災網站回饋信件";
            //建立 html 郵件格式
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>鑑定火災網站回饋信件</h1>" +
                $"<h3>{name}，您好：</h3>" +
                $"<h3>已經收到您的回饋信件，內容如下：</h3>" +
                $"<p style='width: 500px;'>{content}</p>" +
                $"<h3>謝謝您的來信</h3>";
            //$"<p>{Comments.Text.Trim()}</p>";
            //設定郵件內容
            message.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定 false 關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密)
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("14rocketback@gmail.com", WebConfigurationManager.AppSettings["MailingKey"]);
                //發信
                client.Send(message);
                //結束連線
                client.Disconnect(true);
            }
        }


    }
}