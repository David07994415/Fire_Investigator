using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class IndexLinkController : Controller
    {
        // GET: BackStage/IndexLink
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string text)
        {
            return View();
        }

    }
}