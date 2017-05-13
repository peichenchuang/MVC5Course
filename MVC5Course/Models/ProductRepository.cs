using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        //�u�n�[�W�o�q�A��������ƪ��s���޿�i�M��
        

        public Product Get�浧���byProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get�Ҧ����(bool active = true)
        {
            return this.Where(p => p.Active == true)
                        .Take(10);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}