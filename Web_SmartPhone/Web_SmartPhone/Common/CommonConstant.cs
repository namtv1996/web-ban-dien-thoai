using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_SmartPhone.Common
{
    //khai báo static để gọi các sesion trong toàn bộ trang wweb
    public static class CommonConstant
    {
        //khai báo 1 biến lưu trữ 1 session có tên Account_Session
        public static string Account_Sesion = "Account_Session";
        public static string Product_Sesion = "Product_Sesion";
    }
}