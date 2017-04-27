using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_SmartPhone.Models;

namespace Web_SmartPhone.Common
{
    [Serializable]
    public class Product_Cart_Session
    {
        public int? Quantity { get; set; }
        public Product Product { get; set; }
    }
}