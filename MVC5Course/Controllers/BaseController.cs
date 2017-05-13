using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        public ActionResult Debug()
        {
            return Content("Hello World!");
        }
    }
}