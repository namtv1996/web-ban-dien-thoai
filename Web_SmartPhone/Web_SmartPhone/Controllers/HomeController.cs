using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web_SmartPhone.Models;
//using Web_SmartPhone.Areas.Admin.Controllers;
using Web_SmartPhone.Common;
using System.Web.Script.Serialization;

namespace Web_SmartPhone.Controllers
{

    public class HomeController : Controller
    {
        WebApplication1Entities db = new WebApplication1Entities();
        //trang chu
        public ActionResult Index(int page = 1, int pagesize = 3)
        {

            ViewBag.v = (from x in db.Products select x).Take(4).ToList();
            ViewBag.v1 = (from x in db.Products select x).Take(4).ToList();


            return View(db.Products.OrderByDescending(x => x.Name).ToPagedList(page, pagesize));
        }

        //click vao cac item trong danh muc
        public ActionResult Index2(int? idcate)

        {
            var list_product = from x in db.Products
                               where x.CategoryID == idcate
                               select x;
            // var list_product = db.Products.Select(n => n.CategoryID == idcate).ToList();
            return View(list_product);
        }
        //DANG NHAP
        public ActionResult Login()
        {
            return View();
        }
        //KQ DANG NHAP
        public ActionResult Login_Result(Account ac)
        {
            if (ModelState.IsValid)
            {

                var res = new Account_Model().Login(ac.Username, ac.Password);
                if (res == 1)
                {
                    //lấy account có trong db 
                    var account = new Account_Model().GetAccount(ac.Username);
                    //khởi tạo model của session
                    var session_account = new Account_Login_Session();
                    //gán giá trị
                    session_account.AccountID = account.ID;
                    session_account.Username = account.Username;
                    //them session
                    Session.Add(CommonConstant.Account_Sesion, session_account);
           

                    //return View("~/Areas/Admin/Views/Amin/Index.cshtml", db.Products.OrderByDescending(x => x.Name).ToPagedList(1, 4));
                    //return RouteValueDictionary(new { Controller = "Home", action = "Login",Areas = "Admin" }));
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                else
                {
                    if (res == 0)
                    {
                        ModelState.AddModelError("", "Thông tin tài khoản không tồn tại");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thông tin mật khẩu không chính xác");
                    }

                }
            }
            return View("Login");

        }
        //tim kiem
        public ActionResult Timkiem()
        {
            string key = Request.Form["srchFld"].ToString();


            var model = db.Products.Where(m => m.Name.ToUpper().Contains(key));
            return View(model);
        }
        public ActionResult Product()
        {
            return View();
        }
        //TRANG TRONG
        public ActionResult Blank()
        {
            return View();
        }


        //CHI TIET SAN PHAM
        public ActionResult Product_Details(int masp)
        {
            //lay them anh chi tiet
            ViewBag.v = (from x in db.MoreImages where x.Product_ID == masp select x).ToList();
            //lay cac san pham liem quan cung nha san xuat
            ViewBag.vi = (from x in db.Products from y in db.Products where x.ID == masp && x.Manufactory == y.Manufactory select y).ToList();
            var model = db.Products.SingleOrDefault<Product>(x => x.ID == masp);
            return View(model);

        }
        //LIEN HE
        public ActionResult Contact()
        {
            return View();
        }
        //GIO HANG
        public ActionResult ShoppingCart(int? productId, int? quantity)
        {
            //gọi session
            var cart = Session[CommonConstant.Product_Sesion];
            //lấy 1 product được chọn khi click add to
            var produc_get = db.Products.SingleOrDefault(x => x.ID == productId);
            //khai báo 1 list có kiểu Product)_Cart_Sesison;
            var list = new List<Product_Cart_Session>();
            //nếu sesion !=null ép kiểu session sang list;

            if (cart != null)
            {
                list = (List<Product_Cart_Session>)cart;
                //giỏ hàng đã có sp thì tăng số lượng sp thêm một
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }

                    }
                }
                else
                {
                    //nếu giỏ hàng chưa có sp này
                    var item = new Product_Cart_Session();
                    item.Quantity = quantity;
                    item.Product = produc_get;
                    //thêm sp vào giỏ hàng
                    list.Add(item);
                }
            }
            else
            {
                //nếu giỏ hàng chưa có sp được chọn thì thêm mới vào giỏ
                var item = new Product_Cart_Session();
                item.Quantity = quantity;
                item.Product = produc_get;
                list.Add(item);
            }
            Session[CommonConstant.Product_Sesion] = list;
            //trả về list product trong giỏ hàng
            //return View("Index");
            return RedirectToAction("Show_Cart", list);

        }
        //show cart
        public ActionResult Show_Cart()
        {
            
            var cart = Session[CommonConstant.Product_Sesion];
            var list = (List<Product_Cart_Session>)cart;
            if (list != null && list.Count != 0)
            {
                // list = (List<Product_Cart_Session>)cart;
                return PartialView(list);
            }
            else
                return View("~/Views/Home/Blank.cshtml");


        }
        //xoa gio hang
        public JsonResult DeleteAll_Cart()
        {
            var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
            if (Session[CommonConstant.Product_Sesion] == null||sessioncart.Count==0)
            {
                return Json(new { status = false });
            }
            else
            {
                Session[CommonConstant.Product_Sesion] = null;
                return Json(new { status = true });
            }
          
        }
        //xoa tung sp trong gio hang
        public JsonResult DeleteProduct_Cart(int id)
        {
            var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
            if (Session[CommonConstant.Product_Sesion] == null||sessioncart.Count==0)
            {
                return Json(new { status = false });
            }
            else
            {
                //lay danh sach cac sp tu session
                //var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
                sessioncart.RemoveAll(x => x.Product.ID == id);
                Session[CommonConstant.Product_Sesion] = sessioncart;
                return Json(new { status = true });

            }

        }
  
        //Cap nhat gio hang
        public JsonResult CartUpdate(string cartmodel)

        {
              var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
          
            if (Session[CommonConstant.Product_Sesion] == null||sessioncart.Count==0)
            {
                return Json(new { status = false });
            }
            else
            {
                //chuyen data tu string sang dang list<Product_Cart_Session>
                //danh sach cac san da duoc Deserialize 
                var jsoncart = new JavaScriptSerializer().Deserialize<List<Product_Cart_Session>>(cartmodel);
                // danh sach cac san pham trong session
              //  var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
                foreach (var item in sessioncart)
                {
                    //tim product co id = id tuong ung cua produc ttrong session
                    var jsonitem = jsoncart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                    if (jsonitem != null)
                    {
                        //gan laj
                        item.Quantity = jsonitem.Quantity;
                    }
                }
                Session[CommonConstant.Product_Sesion] = sessioncart;
                return Json(new { status = true });
            }
         
        }
        //tang 1 sp
        public JsonResult Produc_Plus(int id)
        {
            var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
            if (Session[CommonConstant.Product_Sesion] == null||sessioncart.Count==0)
            {
                return Json(new { status = false });
            }
            else
            {
                
                sessioncart.SingleOrDefault(x => x.Product.ID == id).Quantity++;
                Session[CommonConstant.Product_Sesion] = sessioncart;
                return Json(new { status = true });
            }
         

        }

        //giam 1 sp
        public JsonResult Produc_Minus(int id)
        {
            var sessioncart = (List<Product_Cart_Session>)Session[CommonConstant.Product_Sesion];
            if (Session[CommonConstant.Product_Sesion] == null ||sessioncart.Count==0)
            {
                return Json(new { status = false });
            }
            else
            {
               
                sessioncart.SingleOrDefault(x => x.Product.ID == id).Quantity--;
                Session[CommonConstant.Product_Sesion] = sessioncart;
                return Json(new { status = true });
            }

        }

        public ActionResult Register()
        {

            return View();
        }
        [ChildActionOnly]//khong cho nguoi dung dieu huong den action nay
        public PartialViewResult Partial_Dropdown_Category()
        {
            var list_name_category = (from x in db.Categories select x);
            ViewBag.ID = new SelectList(list_name_category, "ID", "Name");

            ViewBag.v1 = db.Categories.ToList();
            return PartialView(db.Categories.ToList());
        }
        //show danh muc trong layout
        public PartialViewResult Patial_Category()
        {
            // var model = new Category_Model().getList_Category();
            var model = db.Categories.Select(n => n).ToList();
            return PartialView(model);
        }
        //hien thi cac sp co trong gio hang o layout
        public PartialViewResult Partial_CountItem()
        {
            var cart = Session[CommonConstant.Product_Sesion];
            var list = new List<Product_Cart_Session>();
            if (cart != null)
            {
                list = (List<Product_Cart_Session>)cart;
            }
            return PartialView(list);
        }

    }
}