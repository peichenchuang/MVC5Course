using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    [HandleError(ExceptionType =typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();
        [LocalOnly]
        public ActionResult Debug()
        {
            return Content("Hello World!");
        }

        //網址被亂Try的時候 回首頁
        protected override void HandleUnknownAction(string actionName)
        {
            this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
        }
    }
}