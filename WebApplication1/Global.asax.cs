using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Areas.BackStage.Filter;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //FilterProviders.Providers.Add(new UpdateMemberCustomFilterProvider(DependencyResolver.Current));
        }
        protected void Application_Error()
        {
            //Exception exception = Server.GetLastError();
            //Response.Clear();

            //// Log the exception if necessary

            //// Redirect to a specific error view
            //Response.Redirect("/Home"); // Assuming "Error" is the action name and "Home" is the controller name
        }
    }
}
