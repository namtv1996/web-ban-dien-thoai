using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_SmartPhone.Models;

namespace Web_SmartPhone.Areas.Admin.Models
{
    public class Admin_Dao
    {
        WebApplication1Entities db = null;
        public Admin_Dao()
        {
            db = new WebApplication1Entities();
        }
        public Account GetAccount(string username)
        {
            var model = db.Accounts.SingleOrDefault(x => x.Username == username);
            return model;
        }
    }
}