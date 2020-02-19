using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbDieuLenh;

namespace ThiDieuLenh.Areas.Admin.Models
{
    public class HomeModel
    {
        // Tạo đề (đang sai cần làm lại)
        public string NamHoc { set; get; }
        public string SoCau { set; get; }
        public string loai1 { set; get; }
        public string loai2 { set; get; }
        public string loai3 { set; get; }
        public HttpPostedFileBase File { set; get; }

        //THêm thí sinh 
        public string id { set; get; }
        public string NamHocS1 { set; get; }
        public string NamHocS2 { set; get; }
        public string HoTen { set; get; }
        public string Lop { set; get; }
        public string ChuyenKhoa { set; get; }
        public HttpPostedFileBase FileS { set; get; }

        //Thêm giáo viên
        public string NamHocGV { set; get; }
        public string HoTenGV { set; get; }
        public string TaiKhoan { set; get; }
        public string MatKhau { set; get; }
        public HttpPostedFileBase FileGV { set; get; }

        //Thêm tài khoản
        public string TKQL { set; get; }
        public string MKQL { set; get; }
        public string NamHocQL { set; get; }
    }
}