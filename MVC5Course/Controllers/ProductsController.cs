using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    /// <summary>
    /// 這是一個精簡版的產品列表
    /// </summary>
    public class ProductsController : BaseController
    {
        ProductRepository repo = RepositoryHelper.GetProductRepository();
        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            //錯誤示範：因為repo沒有包含資料庫連線(只有查詢邏輯)，沒有unit of work
            //var repo = new ProductRepository();
            //var data1 = repo.All();

            //正確寫法
            //var repo = new ProductRepository();
            //repo.UnitOfWork = new EFUnitOfWork();

            //簡潔且正確寫法
            //var repo = RepositoryHelper.GetProductRepository();

            //var data = db.Product
            //        .Where(p => p.Active.HasValue && p.Active.Value == Active).ToList().Take(10);

            var data = repo.GetProduct列表頁所有資料(showAll:false);

            //這句可加可不加
            ViewData.Model = data; //強型別
            ViewData["ppp"] = data; //弱型別
            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            //Model Binding(模型繫結)
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            //repo 沒有 find API, 自己寫一個
            Product product = repo.Get單筆資料byProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repo.insert(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料byProductId(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Bind(Include = "ProductId,ProductName,Price,Active,Stock") 只接受前述 5 個欄位，減少ViewModel的數量
        //用實體 Model 去接，但只接這五個欄位，其他欄位帶 EF 的 Default Value
        //通常這樣驗證會比較麻煩
        //保哥：這樣的程式碼比較不好維護，簡易採 View Model
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //用 Repo 取代
                repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //用Repo取代
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料byProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();

            repo.Delete(id);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        public ActionResult ListProducts()
        {
            //var data = db.Product
            //            .Where(p => p.Active == true)
            //            .Select(p => new ProductListVM()
            //            {
            //                ProductId = p.ProductId,
            //                ProductName = p.ProductName,
            //                Price = p.Price,
            //                Stock = p.Stock
            //            })
            //            .Take(10);
            var data = repo.GetProduct列表頁所有資料().Select(p => new ProductListVM()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock
            }); ;

            return View(data);
        }

        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductListVM data)
        {
            if (ModelState.IsValid)
            {
                //TODO: 儲存資料進資料庫
                TempData["CreateProduct_Result"] = "資料新增成功";
                return RedirectToAction("ListProducts");
            }
            //驗證失敗，繼續顯示原本的表單
            return View();
        }
    }
}
