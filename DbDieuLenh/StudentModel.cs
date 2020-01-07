using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbDieuLenh
{
    public class StudentModel
    {
        private SqlConnection con = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();
        private SqlConnection con2 = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd2 = new SqlCommand();
        public StudentModel()
        {
            con.Open();
            cmd.Connection = con;
            //cmd.Parameters.Add("@SoHieu", SqlDbType.NVarChar, 50).Value = SoHieu;
            cmd.Parameters.Add("@SoHieuS", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NamHoc1S", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NamHoc2S", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@HoTenS", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LopS", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ChuyenKhoaS", SqlDbType.NVarChar, 50);

            con2.Open();
            cmd2.Connection = con2;
            cmd2.Parameters.Add("@UserNameS", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@PasswordS", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@idS", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@NamHocS", SqlDbType.NVarChar, 50);
        }
        public bool Login(string SoHieu,string NamHocS1)
        {
            SqlConnection con1 = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
            SqlCommand cmd1 = new SqlCommand();
            con1.Open();
            cmd1.Connection = con1;
            var res = false;
            cmd1.CommandText = "select * from tbThiSinh";
            using (DbDataReader reader = cmd1.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbSoHieu = reader.GetString(0);
                        string dbNamHoc = reader.GetString(1);
                        if (SoHieu == dbSoHieu && NamHocS1 == dbNamHoc)
                            res = true;
                    }
                }
            }
            return res;
        }
        public void Add(int tt,string SoHieu,string NamHocS1,string NamHocS2,string HoTen,string Lop, string ChuyenKhoa)
        {
            string sql = "Insert into tbThiSinh (SoHieu, NamHoc1, NamHoc2, HoTen, Lop, ChuyenKhoa) "
                                                 + " values (@SoHieuS, @NamHoc1S, @NamHoc2S, @HoTenS, @LopS, @ChuyenKhoaS);";
            cmd.CommandText = sql;
            cmd.Parameters["@SoHieuS"].Value = SoHieu;
            cmd.Parameters["@NamHoc1S"].Value = NamHocS1;
            cmd.Parameters["@NamHoc2S"].Value = NamHocS2;
            cmd.Parameters["@HoTenS"].Value = HoTen;
            cmd.Parameters["@LopS"].Value = Lop;
            cmd.Parameters["@ChuyenKhoaS"].Value = ChuyenKhoa;
            int rowCount = cmd.ExecuteNonQuery();

            sql = "Insert into tbUser (UserName, Password, id, NamHoc) "
                                            + "values (@UserNameS, @PasswordS, @idS, @NamHocS);";
            
            cmd2.CommandText = sql;
            cmd2.Parameters["@UserNameS"].Value = SoHieu;
            cmd2.Parameters["@PasswordS"].Value = NamHocS1;
            cmd2.Parameters["@idS"].Value = SoHieu;
            cmd2.Parameters["@NamHocS"].Value = NamHocS1;
            rowCount = cmd2.ExecuteNonQuery();
        }
        public void AddS(string link)
        {
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(link);
            Worksheet xlWorksheet = (Worksheet)xlWorkbook.Sheets.get_Item(1);
            Range xlRange = xlWorksheet.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; row++)//đọc row hiện có trong Excel
            {
                string id = valueArray[row, 1].ToString();
                string NamHocS1 = valueArray[row, 2].ToString();
                string NamHocS2 = valueArray[row, 3].ToString();
                string HoTen = valueArray[row, 4].ToString();
                string Lop = valueArray[row, 5].ToString();
                string ChuyenKhoa = valueArray[row, 5].ToString();
                var res = new StudentModel().Login(id, NamHocS1);
                if (!res)
                    Add(row, id, NamHocS1, NamHocS2, HoTen, Lop, ChuyenKhoa);
            }
        }
    }
}
