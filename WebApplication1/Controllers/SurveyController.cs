﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SurveyController : Controller
    {
        private DbModel db = new DbModel();

        [AddLayoutBreadcrumb("controller")]
        [AddLayoutSidebar("controller")]
        [AddLayoutMenu]
        // GET: Survey
        public ActionResult Index()
        {
            string ControllerName = RouteData.Values["controller"].ToString();
            var CkContent = db.BusinessCategory
                                                .FirstOrDefault(x => x.Name == ControllerName)
                                                .BussinessTable.FirstOrDefault().Content;
            return View((object)CkContent);
        }
    }
}