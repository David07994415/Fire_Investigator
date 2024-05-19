using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Reflection;
using System.Web;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/bundles/SampeJSFolder").Include(
          "~/Scripts/SampleJS2/jquery.js",
           "~/Scripts/SampleJS2/bootstrap.min.js",
             "~/Scripts/SampleJS2/owl.carousel.min.js",
              "~/Scripts/SampleJS2/jquery.counterup.min.js",
               "~/Scripts/SampleJS2/waypoints.min.js",
                "~/Scripts/SampleJS2/jquery.colorbox.js",
                 "~/Scripts/SampleJS2/isotope.js",
                "~/Scripts/SampleJS2/ini.isotope.js",
                "~/Scripts/SampleJS2/gmap3.min.js",
                "~/Scripts/SampleJS2/custom.js"));


            bundles.Add(new Bundle("~/bundles/BackStageJSFolder").Include(
                 "~/Scripts/jquery.js",
                "~/Scripts/BackStageJS/bootstrap.min.js",
                 "~/Scripts/BackStageJS/custom.js"));

            bundles.Add(new StyleBundle("~/Content/BackStageCSSFolder").Include(
          "~/Content/BackStageCSS/bootstrap.min.css",
          "~/Content/BackStageCSS/styles.css",
          "~/Content/BackStageCSS/Paging.css"
          ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/SampleCSSFolder").Include(
          "~/Content/SampleCSS2/bootstrap.min.css",
          "~/Content/SampleCSS2/style.css",
          "~/Content/SampleCSS2/responsive.css",
          "~/Content/SampleCSS2/font-awesome.min.css",
          "~/Content/SampleCSS2/animate.css",
          "~/Content/SampleCSS2/owl.carousel.css",
          "~/Content/SampleCSS2/owl.theme.css",
          "~/Content/SampleCSS2/colorbox.css"));
        }
    }
}