using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_SmartPhone.Models
{
    public class Category_Model
    {
        //khoi tao 1 doi tuong dbcontext
        WebApplication1Entities dbContext=null;
        //phuong thu khoi tao cua class Category_Model
        public Category_Model()
        {
            dbContext = new WebApplication1Entities();
        }
        public List<string> getList_Category()
        {
            var list = dbContext.Categories.Select(n => n.Name).ToList();
            return list;
        }
       
    }
}