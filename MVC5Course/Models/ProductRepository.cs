using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        //只要加上這段，全站的資料表的存取邏輯可套用
        

        public Product Get單筆資料byProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get所有資料(bool active = true)
        {
            return this.Where(p => p.Active == true)
                        .Take(10);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}