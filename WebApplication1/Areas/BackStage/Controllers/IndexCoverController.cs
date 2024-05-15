using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class IndexCoverController : Controller
    {
        // GET: BackStage/IndexCover
        public ActionResult Index()
        {
            return View();
        }
    }
}