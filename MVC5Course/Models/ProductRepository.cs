using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        //�u�n�[�W�o�q�A��������ƪ��s���޿�i�M��
        public override IQueryable<Product>All()
        {
            return base.All().Where(p => !p.isDelete);
        }
        public IQueryable<Product> All (bool showAll)
        {
            if(showAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public Product Get�浧���byProductId(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> GetProduct�C���Ҧ����(bool Active = true, bool showAll = false)
        {
            IQueryable<Product> all = this.All();
            if(showAll)
            {
                all = base.All();
            }
            return this.Where(p => p.Active.HasValue && p.Active.Value == Active)
                        .OrderByDescending(p=>p.ProductId)
                        .Take(10);
        }

        public void insert(Product product)
        {
            this.Add(product);
        }

        public void Update (Product product)
        {
            this.UnitOfWork.Context.Entry(product).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product deleteProduct = this.Where(p => p.ProductId == id).FirstOrDefault();
            this.UnitOfWork.Context.Entry(deleteProduct).State = System.Data.Entity.EntityState.Deleted;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}