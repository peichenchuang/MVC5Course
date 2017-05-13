using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult PartitalAbout()
        {
            ViewBag.Message = "Your application description page.";

            //假如 request 是從 ajax 來的，就回傳PartialView
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult SomeAction()
        {
            //盡量不要把JavaScript放在controller，所以不要用Response.Write
            //Response.Write("<script>alert('建立成功!'); location.href='/';</script>");
            //return "<script>alert('建立成功!'); location.href='/';</script>";
            //return Content("<script>alert('建立成功!'); location.href='/';</script>");
            return PartialView("SuccessRedirect", "/");
        }

        //33 練習 FileResult 使用方式
        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/XXX.jpg"), "image/png", "NewName.png"));
        }
    }
}