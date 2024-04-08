using System.Web.Mvc;

namespace WebApplication1.Areas.BackStage
{
    public class BackStageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BackStage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BackStage_default",
                "BackStage/{controller}/{action}/{id}",
                new {controller="Home", action = "Index", id = UrlParameter.Optional } //need to add controller="Home"
                //namespaces: new[] { "WebApplication1.Areas.BackStage.Controllers" }
            );
        }
    }
}