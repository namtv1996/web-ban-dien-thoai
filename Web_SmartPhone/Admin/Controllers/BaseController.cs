using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_SmartPhone.Common;
using System.Web.Routing;

namespace Web_SmartPhone.Areas.Admin.Controllers
{
    //kế thừa lớp BaseController kiểm tra sesion trong admin
    public class BaseController: Controller
    {
        //controller nay kiem tra session dang nhap trong admin, khi nguoi dung chua dang nhap ma dieu huong den cac trang admin thi no se die huong den trang dang nhap
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Lấy session và ép kiểu sang Account_Login_Session
            var session_login = (Account_Login_Session)Session[CommonConstant.Account_Sesion];
            //Nếu sesion =null điều hướng về trang login
            if (session_login == null)
            {
                 filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary ( new { Controller = "Home", action = "Login",area = "" } ));
                //RedirectToAction("Login", "Home", new { area = "" });
            }
            base.OnActionExecuting(filterContext);

        }
    }
   
}