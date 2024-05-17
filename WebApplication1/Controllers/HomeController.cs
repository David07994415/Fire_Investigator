using MailKit.Net.Smtp;
using Microsoft.Ajax.Utilities;
using MimeKit;
using MvcPaging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using WebApplication1.Filter;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    //[AddLayoutBreadcrumb("action")]
    //[AddLayoutSidebar("action")]
    public class HomeController : Controller
    {
        private DbModel db = new DbModel();

        //public DirectoryFrontViewModel DirectoryLayoutViewData { get; set; }
        //public HomeController()
        //{
        //    this.DirectoryLayoutViewData = new DirectoryFrontViewModel();   //has property PageTitle
        //    this.DirectoryLayoutViewData.DirectoryHTML = DirectoryFrontViewModel.GetDirectoryHtml();
        //    this.ViewBag.DirectoryHTML = this.DirectoryLayoutViewData.DirectoryHTML;
        //}

        [AddLayoutMenu]
        public ActionResult Index()
        {
            var newsData=db.News.OrderByDescending(x => x.IssueTime).Where(x=>x.IsTop==true).Take(3).ToList();
            for (var i = 0;i< newsData.Count();i++)
            {
                newsData[i].NewsCkContent = Regex.Replace(newsData[i].NewsCkContent, "<.*?>", string.Empty);
                newsData[i].NewsCkContent = newsData[i].NewsCkContent.Length < 7 ?
                    newsData[i].NewsCkContent + "..." :
                    newsData[i].NewsCkContent.Substring(0, 7) + "...";
            }
            var linkData = db.IndexLink.Where(x => x.IsShow == true).ToList();
            var CoverData= db.IndexCover.Where(x => x.IsShow == true).ToList();
            var PurposeData = db.IndexPurpose.FirstOrDefault();
            var IndexData = new HomeDataViewModel()
            {
                CoverData = CoverData,
                NewsData = newsData,
                PurposeData = PurposeData,
                LinkData = linkData
            };
            return View(IndexData);
        }



        //public ActionResult _PartialBanner(string action) 
        //{
        //    var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == action).RecursiveId;
        //    var NodeDirectoryList = db.Directory.Where( x=>x.Id== ParentDirectoryId || x.Value == action).ToList();
        //    return PartialView(NodeDirectoryList);
        //}

        //public ActionResult _PartialSideBar(string action)
        //{
        //    var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == action).RecursiveId;
        //    var NodeDirectoryList = db.Directory.Where(x => x.Id == ParentDirectoryId || x.RecursiveId == ParentDirectoryId).ToList();
        //    return PartialView(NodeDirectoryList);
        //}

        //[AddLayoutBreadcrumb("id")]
        //[AddLayoutSidebar("id")]
        //public ActionResult Business(string id)
        //{
        //    if (id == "Job" || id == "License" || id == "Consult"||id== "Survey")
        //    {
        //        var JobCkContent = db.BusinessCategory
        //                                        .FirstOrDefault(x => x.Name == id)
        //                                        .BussinessTable.FirstOrDefault().Content;

        //        var DirectoryDate = db.Directory.FirstOrDefault(x => x.Value == id).Title;
        //        ViewBag.PageTitle = DirectoryDate;
        //        ViewBag.Title = id;
        //        return View((object)JobCkContent);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}


        private class ExcludeFilterAttribute : Attribute
        {
        }
    }
}