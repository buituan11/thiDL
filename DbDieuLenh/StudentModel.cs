using Microsoft.Office.Interop.Excel;
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
        private SqlConnection con = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();
        public StudentModel()
        {
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
        public void Add(int tt,string SoHieu,string NamHoc,string HoTen,string Lop, string ChuyenKhoa)
        {
            string t = tt.ToString();
            string SoHieuS = "";
            for (int i = 0; i < SoHieu.Length; i++)
                if (SoHieu[i] != '.')
                    SoHieuS += SoHieu[i];
            string HoTenS = "";
            for (int i = 0; i < HoTen.Length; i++)
                if (HoTen[i] != ' ')
                    HoTenS += HoTen[i];
            string sql = "Insert into tbThiSinh (id, NamHoc, HoTen, Lop, ChuyenKhoa) "
                                                 + " values (@"+t+SoHieuS+", @" + t + NamHoc+", @" + t + HoTenS+", @" + t + Lop+", @" + t + ChuyenKhoa+");";
            cmd.CommandText = sql;

            cmd.Parameters.Add("@" + t + SoHieuS, SqlDbType.NVarChar, 50).Value = SoHieu;
            cmd.Parameters.Add("@" + t + NamHoc, SqlDbType.NVarChar,50).Value = NamHoc;
            cmd.Parameters.Add("@" + t + HoTenS, SqlDbType.NVarChar, 50).Value = HoTen;
            cmd.Parameters.Add("@" + t + Lop, SqlDbType.NVarChar, 50).Value = Lop;
            cmd.Parameters.Add("@" + t + ChuyenKhoa, SqlDbType.NVarChar, 50).Value = ChuyenKhoa;
            int rowCount = cmd.ExecuteNonQuery();

            sql = "Insert into tbUser (UserName, Password, id, NamHoc) "
                                            + "values (@"+t+ "UserName, @" + t + "Password, @" + t + "idS, @" + t + "NamHocS);";
            cmd.CommandText = sql;
            cmd.Parameters.Add("@" + t + "UserName", SqlDbType.NVarChar, 50).Value = SoHieu;
            cmd.Parameters.Add("@" + t + "Password", SqlDbType.NVarChar, 50).Value = NamHoc;
            cmd.Parameters.Add("@" + t + "idS", SqlDbType.NVarChar, 50).Value = SoHieu;
            cmd.Parameters.Add("@" + t + "NamHocS", SqlDbType.NVarChar, 50).Value = NamHoc;
            rowCount = cmd.ExecuteNonQuery();
        }
        public void AddS(string link)
        {
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(link);
            Worksheet xlWorksheet = (Worksheet)xlWorkbook.Sheets.get_Item(1);
            Range xlRange = xlWorksheet.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; ++row)//đọc row hiện có trong Excel
            {
                string id = valueArray[row, 1].ToString();
                string NamHoc = valueArray[row, 2].ToString();
                string HoTen = valueArray[row, 3].ToString();
                string Lop = valueArray[row, 4].ToString();
                string ChuyenKhoa = valueArray[row, 5].ToString();
                var res = new StudentModel().Login(id, NamHoc);
                if (!res)
                    Add(row, id, NamHoc, HoTen, Lop, ChuyenKhoa);
            }
        }
    }
}
