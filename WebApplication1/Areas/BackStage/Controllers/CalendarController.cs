using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Areas.BackStage.Controllers
{
    public class CalendarController : Controller
    {
        // GET: BackStage/Calendar
        public ActionResult Index()
        {
            return View();
        }
    }
}