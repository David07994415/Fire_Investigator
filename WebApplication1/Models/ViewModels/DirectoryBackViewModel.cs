﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Models.ViewModels
{
    public class DirectoryBackViewModel
    {
        private static DbModel db = new DbModel();
        public string DirectoryHTML { get; set; }
        public static string GetSideBarDirectoryHtml(string[] permissionArray)
        {
            List<string> permissionList = new List<string>();
            foreach (string permission in permissionArray) 
            {
                permissionList.Add(permission);
                string charofpermission=permission.Substring(0,1);
                if (!permissionList.Contains(charofpermission)) 
                {
                    permissionList.Add(charofpermission);
                }
            }
            var result = db.Permission.Where(x => permissionList.Contains(x.Value)).ToList();
           
            var roots = result.Where(x => x.ParentTable == null);

            StringBuilder HTML = new StringBuilder("");  //main start
            foreach (var root in roots)
            {

                HTML.Append("<li class='submenu'>");     //2nd start
                HTML.Append($@"<a href='#'><i class='glyphicon glyphicon-list'></i> {root.Title}<span class='caret pull-right'></span></a>");
                DirectoryMenuRecursion(root, HTML);  //要有hrefHTML資料欄位==>TB DB first
                HTML.Append("</li>");  //2nd end
            }
            //main close
            return HTML.ToString();
        }
        public static void DirectoryMenuRecursion(Permission node, StringBuilder html)
        {
            if (node.ChildTable.Count > 0)  //有第三層
            {
                html.Append("<ul style='display: none;'>");
                foreach (Permission child in node.ChildTable)
                {
                    if (child.ChildTable.Count > 0)
                    {
                        html.Append("<li class='dropdown'>");
                        html.Append($@"<a href='/Home/{child.Value}' class='dropdown-toggle' data-toggle='dropdown' title='{child.Title}'>{child.Title}<i class='fa fa-angle-down'></i></a>");
                        DirectoryMenuRecursion(child, html);     //要有hrefHTML資料欄位==>TB DB first
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