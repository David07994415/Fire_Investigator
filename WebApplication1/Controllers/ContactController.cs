using MailKit.Net.Smtp;
using Microsoft.Ajax.Utilities;
using MimeKit;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Utility;

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

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFrontViewModel ContactSubmitViewModel)
        {
            var SecretKey = ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            try
            {
                var GoogleUrl = $"https://www.google.com/recaptcha/api/siteverify";
                var WebClientObj= new System.Net.WebClient();
                WebClientObj.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var data = "secret=" + SecretKey + "&response=" + Request.Form["g-recaptcha-response"];
                var json = WebClientObj.UploadString(GoogleUrl, data);
                // JSON 反序化取 .success 屬性 true/false 判斷
                var IsReCaptchaSuccess = JsonConvert.DeserializeObject<JObject>(json).Value<bool>("success");

                if (!IsReCaptchaSuccess)
                {
                    ViewBag.Message = "機器人驗證失敗！！！";
                    return View(ContactSubmitViewModel);
                }
                if (ModelState.IsValid)
                {
                    Mailing.sendGmail(ContactSubmitViewModel.name, ContactSubmitViewModel.email, ContactSubmitViewModel.content);
                    TempData["MailSent"] = true;
                    ModelState.Clear();  // 清空模型中的error和相關數據
                    return View();          
                }
                else
                {
                    ModelState.AddModelError("", "必填");
                    return View(ContactSubmitViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"{ex.Message}");
                return View(ContactSubmitViewModel);
            }
        }
    }
}