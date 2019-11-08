using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Framework;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Database
{
    public class AccountModel
    {
        private ThiDLDbContext context = null;
        public AccountModel()
        {
            context = new ThiDLDbContext();           
        }
        public bool Login(string user,string pass)
        {
            object[] sqlPara =
            {
                new SqlParameter("@User", user),
                new SqlParameter("Pass", pass)
            };
            var res = context.Database.SqlQuery<bool>("sp_Account_Login @User, @Pass", sqlPara).SingleOrDefault();
            return true;
        }
    }
}
