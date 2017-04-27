using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Web_SmartPhone.Models.Dao
{
    public class Product_Model
    {
        WebApplication1Entities db = null;
        public Product_Model()
        {
            db = new WebApplication1Entities();
        }
        //get list
        public IEnumerable<Product> ListAllPaging(int page,int pagesize)
        {
            return db.Products.OrderBy(x => x.ID).ToPagedList(page, pagesize);
        }
        //them
        public void create(Product pro)
        {
            db.Products.Add(pro);
            db.SaveChanges();
        }
        //phuong thuc xoa product
        public void delete(Product pro)
        {
            db.Products.Remove(pro);
            db.SaveChanges();
        }
        public Product getproductid(int id)
        {
            return db.Products.SingleOrDefault(n => n.ID == id);
        }
    }
}