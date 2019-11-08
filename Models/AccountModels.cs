using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Framework;

namespace Models
{
    class AccountModels
    {
        private ThiDLDbContext context = null;
        public AccountModels()
        {
            context = new ThiDLDbContext();
        }
        public bool Login(string user,string pass)
        {
            var res = context.Database.SqlQuery<string>("select * from dbo.quanly").ToList();
            
            return true;
        }
    }
}
