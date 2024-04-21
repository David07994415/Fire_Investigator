using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WebApplication1.Models;
using static System.Collections.Specialized.BitVector32;

namespace WebApplication1.Filter
{
    public class AddLayoutComponent
    {
    }
    public class AddLayoutSidebar : ActionFilterAttribute
    {
        private DbModel db = new DbModel();
        private readonly string ControllerOrActionName;
        public AddLayoutSidebar(string ControllerOrAction)
        {
            ControllerOrActionName = ControllerOrAction;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string TargetUrl = filterContext.RouteData.Values[ControllerOrActionName].ToString();
            if (TargetUrl =="Index"){ return;}
            string htmlSideBarResult = BuildSideBar(TargetUrl);
            filterContext.Controller.ViewBag.SideBarResult = htmlSideBarResult;
        }
        private string BuildSideBar(string actionName)
        {
            var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == actionName).RecursiveId;
            var NodeDirectoryList = db.Directory.Where(x => x.Id == ParentDirectoryId || x.Value == actionName).ToList();
            StringBuilder htmlString = new StringBuilder();
            foreach (var item in NodeDirectoryList)
            {
                if (item.RecursiveId == null) 
                {
                    htmlString.Append($"<h2 class='widget-title'><i class='fa fa-folder-open-o'>&nbsp;</i>{item.Title}</h2>");
                }
            }
            htmlString.Append("<ul class='arrow nav nav-tabs nav-stacked'>");
            foreach (var item in NodeDirectoryList)
            {
                if (item.RecursiveId != null)
                {
                    htmlString.Append($"<li><a href='/Home/{item.Value}'>{item.Title}</a></li>");
                }
            }
            htmlString.Append("</ul>");
            return htmlString.ToString();
        }
    }
    public class AddLayoutBreadcrumb : ActionFilterAttribute
    {
        private DbModel db = new DbModel();
        private readonly string ControllerOrActionName;
        public AddLayoutBreadcrumb(string ControllerOrAction)
        {
            ControllerOrActionName = ControllerOrAction;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string TargetUrl = filterContext.RouteData.Values[ControllerOrActionName].ToString();
            if (TargetUrl == "Index") { return; }
            string htmlBreadCrumbResult = BuildBreadcrumb(TargetUrl);
            filterContext.Controller.ViewBag.BreadcrumbResult = htmlBreadCrumbResult;
        }
        private string BuildBreadcrumb(string actionName)
        {
            var ParentDirectoryId = db.Directory.FirstOrDefault(x => x.Value == actionName).RecursiveId;
            var NodeDirectoryList = db.Directory.Where(x => x.Id == ParentDirectoryId || x.Value == actionName).ToList();
            StringBuilder htmlString = new StringBuilder();
            foreach (var item in NodeDirectoryList)
            {
                if (item.RecursiveId == null)
                {
                    htmlString.Append($"<li>{item.Title}</li>");
                }
                else
                {
                    htmlString.Append($"<li><a href ='/Home/{item.Value}'>{item.Title} </a></li>");
                }
            }
            htmlString.Append("</ul>");
            return htmlString.ToString();
        }
    }
    public class AddLayoutMenu: ActionFilterAttribute
    {
        private DbModel db = new DbModel();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string htmlMenuResult = BuildMenu();
            filterContext.Controller.ViewBag.MenuResult = htmlMenuResult;
        }
        private string BuildMenu()
        {
            List<Directory> DirectroyList = db.Directory.ToList();
            var roots = DirectroyList.Where(x => x.ParentTable == null);
            StringBuilder HTML = new StringBuilder("<ul class='nav navbar-nav'>");  //main start
            foreach (var root in roots)
            {
                HTML.Append("<li class='dropdown'>");     //2nd start
                HTML.Append($@"<a href='/' class='dropdown-toggle' data-toggle='dropdown' title='{root.Title}'>{root.Title}<i class='fa fa-angle-down'></i></a>");
                DirectoryRecursion(root, HTML);  //要有hrefHTML資料欄位==>TB DB first
                HTML.Append("</li>");  //2nd end
            }
            HTML.Append("</ul>");  //main close
            return HTML.ToString();
        }
        public static void DirectoryRecursion(Directory node, StringBuilder html)
        {
            if (node.ChildTable.Count > 0)  //有第三層
            {
                html.Append("<ul class='dropdown-menu' role='menu'>");
                foreach (Directory child in node.ChildTable)
                {
                    if (child.ChildTable.Count > 0)
                    {
                        html.Append("<li class='dropdown'>");
                        html.Append($@"<a href='/Home/{child.Value}' class='dropdown-toggle' data-toggle='dropdown' title='{child.Title}'>{child.Title}<i class='fa fa-angle-down'></i></a>");
                        DirectoryRecursion(child, html);     //要有hrefHTML資料欄位==>TB DB first
                        html.Append("</li>");
                    }
                    else if (child.ChildTable.Count == 0)
                    {
                        html.Append("<li>");
                        html.Append($@"<a href='/Home/{child.Value}'>{child.Title}</a>");
                        html.Append("</li>");     //要有hrefHTML資料欄位==>TB DB first
                    }
                }
                html.Append("</ul>");
            }
        }
    }

}