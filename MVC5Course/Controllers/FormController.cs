using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        // GET: Form
        public ActionResult Index()
        {
            //取出 drop down list data
            var ratings = (from p in db.Client
                           select p.CreditRating).Distinct().OrderBy(p => p).ToList();

            ViewBag.CreditRatingFilter = new SelectList(ratings);

            var lastName = (from p in db.Client
                           select p.LastName).Distinct().OrderBy(p => p).ToList();

            ViewBag.LastNameFilter = new SelectList(lastName);

            var data = db.Client.Take(10);

            return View(data);
        }

        public ActionResult Edit(int id)
        {
            //這頁有 Model Binding，只有 id
            //ViewData.Model = db.Product.Find(id);
            //ViewData 只有一個強型別 Model 這樣就可以在 View 使用 model
            return View(db.Product.Find(id));
        }
        [HttpPost]
        public ActionResult Action(int id, FormCollection form)
        {
            var product = db.Product.Find(id);

            //只接 ProdcutName 其他都用原始值
            if (TryUpdateModel(product, includeProperties: new string[] { "ProductName"}))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View();
        }
    }
}