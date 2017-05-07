using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            //通常會需要轉成 AsQueryable
            var all = db.Product.AsQueryable();
            var data = all.Where(p =>
                p.Active == true 
                //p.ProductName.Contains("Black")
            ).OrderByDescending(p=>p.ProductId).Take(10); //尚未發出 TSQL，需要用到的時候才會載入(延遲載入)
            //var data = all.Where(p => p.Active == true).ToList(); //已產生 SQL Query 並產生資料
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var data = db.Product.Add(product);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = db.Product.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var data = db.Product.Find(id);
                data.ProductName = product.ProductName;
                data.Price = product.Price;
                data.Stock = product.Stock;
                data.OrderLine = product.OrderLine;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        public ActionResult Delete(int id)
        {
            var data = db.Product.Find(id);

            //更精簡的用法 => db.OrderLine.RemoveRange(data.OrderLine);
            foreach (var item in data.OrderLine.ToList())
            {
                db.OrderLine.Remove(item);
            }

            db.Product.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}