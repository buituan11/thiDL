using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThiDieuLenh.Areas.Admin.Models
{
    public class HomeModel
    {
        public string NamHoc { set; get; }
        public string SoCau { set; get; }
        public string loai1 { set; get; }
        public string loai2 { set; get; }
        public string loai3 { set; get; }
        public HttpPostedFileBase File { set; get; }

        public string id { set; get; }
        public string NamHocS { set; get; }
        public string HoTen { set; get; }
        public string Lop { set; get; }
        public string ChuyenKhoa { set; get; }
        public HttpPostedFileBase FileS { set; get; }
    }
}