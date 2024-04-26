using MailKit.Net.Smtp;
using Microsoft.Ajax.Utilities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFrontViewModel ContactSubmitViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //要進行機器人驗證，可以參考 https://captcha.com/mvc/mvc-captcha.html#mvc5-captcha
                    //看要不要存資料庫
                    sendGmail(ContactSubmitViewModel.name, ContactSubmitViewModel.email, ContactSubmitViewModel.content);

                    ModelState.Clear();  // 清空模型中的error數據
                    return View();           // 返回沒有清空資料
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