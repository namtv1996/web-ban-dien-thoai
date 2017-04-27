using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web_SmartPhone.Areas.Admin.Models;
using Web_SmartPhone.Common;
using Web_SmartPhone.Models;

namespace Web_SmartPhone.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin/Admin
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            //var cart = Session[CommonConstant.Account_Sesion];
            //var model = new Account_Login_Session();


            //if (cart!=null)
            //{
            //    model = (Account_Login_Session)cart;
            //}
            var cart = Session[CommonConstant.Account_Sesion];
            ViewBag.v = /*(Account_Login_Session)*/cart;

            var dao = new Account_Model();
            var model = dao.listAllPaging(page, pagesize);


            return View(model);
        }
        //xoa tai khoan
        public ActionResult DeleteAccount(int id)
        {
            var dao = new Account_Model();
            //lay ra account voi id tuong ung
            var account = dao.GetAccountId(id);
            dao.Remove(account);
            return Redirect("Index");
        }
        //them moi tai khoan

        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateResult(Account ac)
        {
            if (ModelState.IsValid)
            {
                var dao = new Account_Model();
                int res = dao.Create(ac);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại!");
                    return View("CreateAccount");
                }
            }
            else return View("CreateAccount");

        }
        //Sửa bảng account
        public ActionResult EditAccount(int id)
        {
            var dao = new Account_Model();
            var model = dao.GetAccountId(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditResult(Account ac)
        {
            if (ModelState.IsValid)
            {

                var dao = new Account_Model();
                //sua thanh cong
                if (dao.Edit(ac) == 1)
                {
                    return RedirectToAction("Index");
                }
                //tai khoan da ton tai
                else
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                    return View("EditAccount");
                }

            }
            else return View("EditAccount");
             


        }
        public ActionResult Logout()
        {
            Session[CommonConstant.Account_Sesion] = null;
            return RedirectToAction("Index","Home",new {area = "" });
        }

    }
}