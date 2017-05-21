using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : BaseController
    {
        // GET: Form
        public ActionResult Index(int CreditRatingFilter = -1, string LastNameFilter = "")
        {
            //取出 drop down list data
            var ratings = (from p in db.Client
                           select p.CreditRating).Distinct().OrderBy(p => p).ToList();

            ViewBag.CreditRatingFilter = new SelectList(ratings);

            var lastName = (from p in db.Client
                            select p.LastName).Distinct().OrderBy(p => p).ToList();

            ViewBag.LastNameFilter = new SelectList(lastName);

            var data = db.Client.Where(p => p.LastName.Contains(LastNameFilter));
            if (CreditRatingFilter != -1)
            {
                data = data.Where(p => p.CreditRating == CreditRatingFilter);
            }
            data = data.Take(10);
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
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = db.Product.Find(id);

            //只接 ProdcutName 其他都用原始值
            if (TryUpdateModel(product, includeProperties: new string[] { "ProductName" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ClientEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ViewBag.CreditRating = new SelectList(items);

            ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }
    }
}