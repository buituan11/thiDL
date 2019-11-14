
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Office.Interop.Excel;
using DbDieuLenh.Framework;
using System;
using System.Data;

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
                        string dbUser = reader.GetString(0);
                        string dbPass = reader.GetString(1);
                        if (user == dbUser && pass == dbPass)
                        {
                            res = true;
                        }

                    }
                }
            }
            return res;
        }
        public string GetName(string user)
        {
            var res = "";
            cmd.CommandText = "select * from tbQuanLy";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbUser = reader.GetString(0);
                        if (user == dbUser)
                        {
                            res = reader.GetString(2);
                        }

                    }
                }
            }
            return res;
        }
        public string AddQuestion(string link)
        {
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(link);
            Worksheet xlWorksheet = (Worksheet)xlWorkbook.Sheets.get_Item(1);
            Range xlRange = xlWorksheet.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            string gtr = "";
            for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; ++row)//đọc row hiện có trong Excel
            {
                string xlsNamHoc = valueArray[row, 1].ToString();
                //giatri += valueArray[row, colum].ToString();//Giá trị = valueArray[dòng, cột]; ToString() là để chuyển giá trị thành dạng String
                string sql = "Insert into tbCauHoi (MaCH, NamHoc, NoiDung, DA_A, DA_B, DA_C, DA_D, DA, Loai) values (@Ma, ";
                for (int i = 1; i <= xlWorksheet.UsedRange.Columns.Count; ++i)
                {
                    string x = Convert.ToString(i);
                    if (i != 8) sql = sql + "@para" + x + ", ";
                    else sql = sql + "@para" + x;
                }
                sql += ");";
                int index = -1;
                cmd.CommandText = "select count(*) from tbCauHoi where NamHoc = "+xlsNamHoc+";";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            index = Convert.ToInt32(reader.GetValue(0));
                        }
                    }
                }
                index++;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@Ma", SqlDbType.NVarChar, 50).Value = index.ToString();
                cmd.Parameters.Add("@para1", SqlDbType.NVarChar, 50).Value = xlsNamHoc;
                for (int i = 2; i <= 6; ++i)
                {
                    string x = Convert.ToString(i);
                    if (valueArray[row, i] == null) valueArray[row, i] = "";
                    cmd.Parameters.Add("@para"+x, SqlDbType.NVarChar, 1000).Value = valueArray[row, i].ToString();
                }
                for(int i=7; i<=8; ++i) {
                    string x = Convert.ToString(i);
                    if (valueArray[row, i] == null) valueArray[row, i] = "";
                    cmd.Parameters.Add("@para" + x, SqlDbType.NVarChar, 50).Value = valueArray[row, i].ToString();
                }
                int run = cmd.ExecuteNonQuery();
            }
            return gtr;
        }
    }
}
