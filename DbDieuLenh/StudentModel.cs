using DbDieuLenh.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDieuLenh
{
    public class StudentModel
    {
        private DLDbContext context = null;
        private SqlConnection con = new SqlConnection(@"data source=DESKTOP-ET1FTND;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();
        public StudentModel()
        {
            context = new DLDbContext();
            con.Open();
            cmd.Connection = con;
        }
        public bool Login(string SoHieu,string NamHoc)
        {
            var res = false;
            cmd.CommandText = "select id, NamHoc from tbThiSinh";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbSoHieu = reader.GetString(0);
                        string dbNamHoc = reader.GetString(1);
                        if (SoHieu == dbSoHieu && NamHoc == dbNamHoc)
                            res = true;
                    }
                }
            }
            return res;
        }
        public void Add(string SoHieu,string NamHoc,string HoTen,string Lop, string ChuyenKhoa)
        {
            string sql = "Insert into tbThiSinh (id, NamHoc, HoTen, Lop, ChuyenKhoa) "
                                                 + " values (@SoHieu, @NamHoc, @HoTen, @Lop, @ChuyenKhoa);";
            cmd.CommandText = sql;

            cmd.Parameters.Add("@SoHieu", SqlDbType.NVarChar, 50).Value = SoHieu;
            cmd.Parameters.Add("@NamHoc", SqlDbType.NVarChar,50).Value = NamHoc;
            cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 50).Value = HoTen;
            cmd.Parameters.Add("@Lop", SqlDbType.NVarChar, 50).Value = Lop;
            cmd.Parameters.Add("@ChuyenKhoa", SqlDbType.NVarChar, 50).Value = ChuyenKhoa;
            int rowCount = cmd.ExecuteNonQuery();
        }
    }
}
