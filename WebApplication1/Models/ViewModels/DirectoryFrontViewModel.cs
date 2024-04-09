using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models.ViewModels
{
    
    public class DirectoryFrontViewModel
    {
        private static DbModel db = new DbModel();
        public string DirectoryHTML { get; set; }
        public static string GetDirectoryHtml()
        {
            List<Directory> DirectroyList = db.Directory.ToList();
            var roots = DirectroyList.Where(x => x.ParentTable == null);
            StringBuilder HTML = new StringBuilder("<ul class='nav navbar-nav'>");  //main start
            foreach (var root in roots)
            {
                HTML.Append("<li class='dropdown'>");     //2nd start
                HTML.Append($@"<a href='{root.Value}' class='dropdown-toggle' data-toggle='dropdown' title='{root.Title}'>{root.Title}<i class='fa fa-angle-down'></i></a>");
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
                        html.Append($@"<a href='Home/{child.Value}' class='dropdown-toggle' data-toggle='dropdown' title='{child.Title}'>{child.Title}<i class='fa fa-angle-down'></i></a>");
                        DirectoryRecursion(child, html);     //要有hrefHTML資料欄位==>TB DB first
                        html.Append("</li>");
                    }
                    else if (child.ChildTable.Count == 0)
                    {
                        html.Append("<li>");
                        html.Append($@"<a href='{child.Value}'>{child.Title}</a>");
                        html.Append("</li>");     //要有hrefHTML資料欄位==>TB DB first
                    }
                    //DirectoryRecursion(child, html);     
                }
                html.Append("</ul>");
            }
        }

    }
    public class HomeFrontViewModel: DirectoryFrontViewModel
    {

    }

}