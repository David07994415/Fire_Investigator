using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1.Filter
{
    public class MemberAuthFilter: AuthorizeAttribute
    {
        private DbModel db = new DbModel();
        private readonly string[] allowedroles;
        public MemberAuthFilter(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            string UserName = httpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            var UserId = db.Member.FirstOrDefault(x => x.Account == UserName).Id;
            //if (UserId != null)
            //{
            var userRole = db.Member.FirstOrDefault(x => x.Id == UserId).Permission;
            string[] rolelist = userRole.Split(',');
            foreach (var role in rolelist)
            {
                if (role == allowedroles[0])
                {
                    return true;
                }
            }
            //}
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