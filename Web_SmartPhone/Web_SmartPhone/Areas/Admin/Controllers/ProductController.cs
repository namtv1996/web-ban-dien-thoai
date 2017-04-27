using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_SmartPhone.Common;
using Web_SmartPhone.Models.Dao;
using Web_SmartPhone.Models;
using System.IO;

namespace Web_SmartPhone.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1,int pagesize=6)
        {
            var cart = Session[CommonConstant.Account_Sesion];
            ViewBag.v = (Account_Login_Session)cart;
            var dao = new Product_Model();
            var model = dao.ListAllPaging(page, pagesize);
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateResult([Bind(Include ="Name,Alias,CategoryId,CreateDate,Price,Detail,Status,Code,Descriptions,Quantity,MoreImages,Warranty,Tophot,PromotionPrice,Manufactory")] Product pro,HttpPostedFileBase upload)
        {
            Product_Model dao = new Product_Model();

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string filename = Path.GetFileName(upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    upload.SaveAs(path);
                    pro.Images = "/Content/images/" + filename;
                }
                else
                {
                    pro.Images = " ";
                }
                dao.create(pro);
                return RedirectToAction("Index");
            }
            else
                return View("Create");
            
        }
       
     public ActionResult Delete(int id)
        {
            Product_Model dao = new Product_Model();
            var pro = dao.getproductid(id);
            dao.delete(pro);
            return RedirectToAction("Index");
        }
    }
}