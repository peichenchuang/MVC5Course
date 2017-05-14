using System;
using System.Web.Mvc;
namespace MVC5Course.Controllers
{
    public class SharedViewBagAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your Application description page.";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

    }
}