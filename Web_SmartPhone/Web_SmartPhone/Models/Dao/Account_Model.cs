using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Web_SmartPhone.Models
{
    public class Account_Model
    {
        WebApplication1Entities db = null;
        public Account_Model()
        {
            db = new WebApplication1Entities();
        }
        public Account GetAccount(string username)
        {
            return db.Accounts.First(m => m.Username == username);
        }
        public Account GetAccountId(int id)
        {
            return db.Accounts.SingleOrDefault(m => m.ID == id);
        }
        public int Login(string username, string password)
        {
            var res = db.Accounts.SingleOrDefault(x => x.Username == username);

            if (res == null)
            {
                //Tài khoản không tồn tại
                return 0;

            }
            else
            {
                if (res.Password == password)
                {
                    //Đăng nhập thành công
                    return 1;
                }
                else
                {
                    //Sai mật khẩu
                    return -1;
                }
            }
        }
        public IEnumerable<Account> listAllPaging(int page, int pagesize)
        {
            return db.Accounts.OrderByDescending(x => x.ID).ToPagedList(page, pagesize);
        }
        public void Remove(Account ac)
        {
            db.Accounts.Remove(ac);
            db.SaveChanges();
        }
        public int  Edit(Account ac)
        {
          
            if (db.Accounts.SingleOrDefault(x => x.Username == ac.Username) != null){
                return 0;
            }
            else
            {
                var account = db.Accounts.SingleOrDefault(x => x.ID == ac.ID);
                account.Username = ac.Username;
                account.Password = ac.Password;
                account.Group_Account = ac.Group_Account;
                db.SaveChanges();
                return 1;
            }
         
         
        }
        public int Create(Account ac)
        {
            if (db.Accounts.SingleOrDefault(x => x.Username==(ac.Username)) != null)
            {
                //tai khoan da ton tai
                return 0;
            }
            else
            {
                db.Accounts.Add(ac);
                db.SaveChanges();
                return 1;
            }

        }

    }
}