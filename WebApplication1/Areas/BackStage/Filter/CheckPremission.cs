using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Filter
{
    public class CheckPremissionAttribute : AuthorizeAttribute
    {
        // reference：https://www.c-sharpcorner.com/UploadFile/56fb14/custom-authorization-in-mvc/
        // reference：https://www.c-sharpcorner.com/article/custom-authorization-filter-in-mvc-with-an-example/
        private DbModel db = new DbModel();
        private readonly string[] allowedroles;
        public CheckPremissionAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            String UserName = httpContext.User.Identity.Name;
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;
            if (UserId != null)
            {
                var userRole = db.Member.FirstOrDefault(x => x.Id == UserId).Permission;
                string[] rolelist = userRole.Split(',');
                foreach (var role in rolelist)
                {
                    if (role == allowedroles[0])
                    {
                        return true;
                    }
                }
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "index" }
               });
        }
    }
}
