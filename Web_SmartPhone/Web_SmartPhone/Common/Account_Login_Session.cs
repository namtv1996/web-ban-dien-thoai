using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_SmartPhone.Models;

namespace Web_SmartPhone.Common
{
    //[Serializable]

    public class Account_Login_Session
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
    }
}