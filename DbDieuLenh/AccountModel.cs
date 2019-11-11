using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDieuLenh.Framework;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Common;

namespace DbDieuLenh
{
    public class AccountModel
    {
        private DLDbContext context = null;
        private SqlConnection con = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();

        public AccountModel()
        {
            context = new DLDbContext();
            con.Open();
            cmd.Connection = con;
        }
        public bool Login(string user, string pass)
        {
            var res = false;
            cmd.CommandText = "select * from tbQuanLy";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int index = Convert.ToInt32(reader.GetValue(0));
                        string dbUser = reader.GetString(1);
                        string dbPass = reader.GetString(2);
                        if (user == dbUser && pass == dbPass)
                        {
                            res = true;
                        }

                    }
                }
            }
            return res;
        }
        public int GetAdminId(string user)
        {
            cmd.CommandText = "select * from tbQuanLy";
            int res= new int();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbUser = reader.GetString(1);
                        if (user == dbUser)
                            res = Convert.ToInt32(reader.GetValue(0));
                    }
                }
            }
            return res;
        }
    }
}
