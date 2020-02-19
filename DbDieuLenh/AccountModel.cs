
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;

namespace DbDieuLenh
{
    public class QL
    {
        private string TKQL;
        private string MKQL;
        private string NamHocQL;
        public void setTKQL(string TK) { TKQL = TK; }
        public void setMKQL(string MK) { MKQL = MK; }
        public string getTKQL() { return TKQL; }
        public string getMKQL() { return MKQL; }
        public void setNamHocQL(string NamHoc) { NamHocQL = NamHoc; }
        public string getNamHocQL() { return NamHocQL; }
    }
    public class AccountModel
    {
        private SqlConnection con = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();
        private SqlConnection con1 = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd1 = new SqlCommand();

        private SqlConnection con2 = new SqlConnection(@"data source=.;initial catalog=DieuLenh;user id=sa;password=buituan112;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd2 = new SqlCommand();
        //DESKTOP-SSAH8PS\SQLEXPRESS

        public AccountModel()
        {
            con.Open();
            cmd.Connection = con;
            con1.Open();
            cmd1.Connection = con1;
            cmd1.Parameters.Add("@Id", SqlDbType.NVarChar, 50);
            cmd1.Parameters.Add("@Name", SqlDbType.NVarChar, 50);

            con2.Open();
            cmd2.Connection = con2;
            cmd2.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@Id", SqlDbType.NVarChar, 50);
            cmd2.Parameters.Add("@NamHoc", SqlDbType.NVarChar, 50);


        }
        public bool Login(string user, string pass)
        {
            var res = false;
            cmd.CommandText = "select * from tbUser;";

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
        public string GetId(string user)
        {
            var id = "";
            cmd.CommandText = "select * from tbUser;";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbUser = reader.GetString(0);
                        if (user == dbUser)
                        {
                            id = reader.GetString(2);
                        }

                    }
                }
            }
            return id;
        }
        public string GetNamHoc(string user)
        {
            var NamHoc = "";
            cmd.CommandText = "select * from tbUser;";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbUser = reader.GetString(0);
                        if (user == dbUser)
                        {
                            NamHoc = reader.GetString(3);
                        }

                    }
                }
            }
            return NamHoc;
        }

        // Admin
        public string GetName(string id)
        {

            cmd.CommandText = "select * from tbQuanLy;";
            var res = ""; 
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbID = reader.GetString(0);
                        if (id == dbID)
                        {
                            res = reader.GetString(1);
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

        //QL Giáo viên
        public void AddGV(string NamHocGV, string HoTenGV, string TaiKhoan, string MatKhau)
        {
            cmd.CommandText = "select * from tbGiaoVien;";
            var res = "";
            
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                  
                        res = reader.GetString(0);        
                    }
                }
            }
            string tmp = "";
            if (res == "") res = "GV0";
            for (int i = 2; i < res.Length; i++)
                tmp += res[i];
            int num;
            num = Convert.ToInt32(tmp);
            num++;
            tmp = num.ToString();
            res = "GV" + tmp;

            string sql = "Insert into tbGiaoVien (Id, Name) values (@Id, @Name);";
            cmd1.CommandText = sql;
            cmd1.Parameters["@Id"].Value = res;
            cmd1.Parameters["@Name"].Value = HoTenGV;
            int rowCount = cmd1.ExecuteNonQuery();

            string sql2 = "Insert into tbUser (UserName, Password, Id, NamHoc) values (@UserName, @Password, @Id, @NamHoc);";
            cmd2.CommandText = sql2;
            cmd2.Parameters["@UserName"].Value = TaiKhoan;
            cmd2.Parameters["@Password"].Value = MatKhau;
            cmd2.Parameters["@Id"].Value = res;
            cmd2.Parameters["@NamHoc"].Value = NamHocGV;
            int rowCount2 = cmd2.ExecuteNonQuery();
        }
        public void AddDsGV(string link)
        {
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(link);
            Worksheet xlWorksheet = (Worksheet)xlWorkbook.Sheets.get_Item(1);
            Range xlRange = xlWorksheet.UsedRange;
            object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
            for (int row = 2; row <= xlWorksheet.UsedRange.Rows.Count; row++)//đọc row hiện có trong Excel
            {
                string NamHocGV = valueArray[row, 1].ToString();
                string HoTenGV = valueArray[row, 2].ToString();
                string TaiKhoan = valueArray[row, 3].ToString();
                string MatKhau = valueArray[row, 4].ToString();
                
                //Kiem tra xem da co trong csdl chua
                var res = new AccountModel().Login(TaiKhoan, MatKhau);
                if (!res)
                    AddGV(NamHocGV, HoTenGV, TaiKhoan, MatKhau);
            }
        }
        public QL[] getDSGV()
        {
            int len = 0;
            cmd.CommandText = "select * from tbUser;";
            using (DbDataReader reader = cmd.ExecuteReader())       //Đến số lượng
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(2)[0] == 'G')
                        {
                            len++;
                        }
                    }
                }
            }
            QL[] giaovien = new QL[len];                              //Tạo mảng đối tượng  vơi sl đã đếm
            len = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(2)[0] == 'G')              //Set các trường cho từng đối tượng trong mảng
                        {
                            giaovien[len] = new QL();
                            giaovien[len].setTKQL(reader.GetString(0));
                            giaovien[len].setMKQL(reader.GetString(1));
                            giaovien[len].setNamHocQL(reader.GetString(3));
                            len++;
                        }
                    }
                }
            }
            return giaovien;
        }
        public string getGV(int stt)
        {
            string GV = "";
            int len = 1;
            cmd.CommandText = "select * from tbUser;";
            using (DbDataReader reader = cmd.ExecuteReader())       //tìm tài khoản có số thứ tự đúng với id của url
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (len == stt)
                        {
                            GV = reader.GetString(0);
                        }
                        if (reader.GetString(2)[0] == 'G') len++;
                    }
                }
            }
            return GV;
        }
        //QL Tài khoản
        public void AddQL(string TKQL, string MKQL, string NamHocQL)
        {
            cmd.CommandText = "select * from tbQuanly;";
            var res = "";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        res = reader.GetString(0);
                    }
                }
            }
            string tmp = "";
            if (res == "") res = "AD0";
            for (int i = 2; i < res.Length; i++)
                tmp += res[i];
            int num;
            num = Convert.ToInt32(tmp);
            num++;
            tmp = num.ToString();
            res = "AD" + tmp;

            string sql = "Insert into tbQuanly (Id, Name) values (@Id, @Name);";
            cmd1.CommandText = sql;
            cmd1.Parameters["@Id"].Value = res;
            cmd1.Parameters["@Name"].Value = TKQL;
            int rowCount = cmd1.ExecuteNonQuery();

            string sql2 = "Insert into tbUser (UserName, Password, Id, NamHoc) values (@UserName, @Password, @Id, @NamHoc);";
            cmd2.CommandText = sql2;
            cmd2.Parameters["@UserName"].Value = TKQL;
            cmd2.Parameters["@Password"].Value = MKQL;
            cmd2.Parameters["@Id"].Value = res;
            cmd2.Parameters["@NamHoc"].Value = NamHocQL;
            int rowCount2 = cmd2.ExecuteNonQuery();
        }
        public QL[] getDS()
        {
            int len = 0;
            cmd.CommandText = "select * from tbUser;";
            using (DbDataReader reader = cmd.ExecuteReader())       //Đến số lượng
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(2)[0] == 'A')
                        {
                            len++;
                        }
                    }
                }
            }
            QL[] quanly = new QL[len];                              //Tạo mảng đối tượng  vơi sl đã đếm
            len = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(2)[0] == 'A')              //Set các trường cho từng đối tượng trong mảng
                        {
                            quanly[len] = new QL();
                            quanly[len].setTKQL(reader.GetString(0));
                            quanly[len].setMKQL(reader.GetString(1));
                            quanly[len].setNamHocQL(reader.GetString(3));
                            len++;
                        }
                    }
                }
            }
            return quanly;
        }
        public string getIDTK(int stt)
        {
            string id = "";
            int len = 1;
            cmd.CommandText = "select * from tbQuanly;";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (len == stt)
                        {
                            id = reader.GetString(0);
                        }
                        len++;
                    }
                }
            }
            return id;
        }
        public string getTK(int stt)
        {
            string TK = "";
            int len = 1;
            cmd.CommandText = "select * from tbUser;";
            using (DbDataReader reader = cmd.ExecuteReader())       //tìm tài khoản có số thứ tự đúng với id của url
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (len == stt)
                        {
                            TK = reader.GetString(0);
                        }
                        if (reader.GetString(2)[0] == 'A') len++;
                    }
                }
            }
            return TK;
        }
        public void deleteTK(string id)
        {
            cmd.CommandText = "delete from tbQuanly where Id='" + id + "';";
            int rowCount = cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from tbUser where Id='" + id + "';";
            rowCount = cmd.ExecuteNonQuery();
        }

        //Student
        public string GetNameS(string id, string NamHoc)
        {
            cmd.CommandText = "select * from tbThiSinh;";
            var res = "";
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dbID = reader.GetString(0);
                        string dbNamHoc = reader.GetString(1);
                        if (id == dbID && NamHoc == dbNamHoc)
                        {
                            res = reader.GetString(2);
                        }
                    }
                }
            }
            return res;
        }
    }
}
