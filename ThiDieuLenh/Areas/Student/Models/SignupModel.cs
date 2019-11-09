using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThiDieuLenh.Areas.Student.Models
{
    public class SignupModel
    {
        [Required]
        public string SoHieu { set; get; }
        public string HoTen { set; get; }
        public string Lop { set; get; }
        public string ChuyenKhoa { set; get; }
    }
}