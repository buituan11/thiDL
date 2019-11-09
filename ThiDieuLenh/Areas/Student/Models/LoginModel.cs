using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThiDieuLenh.Areas.Student.Models
{
    public class LoginModel
    {
        [Required]
        public string SoHieu { set; get; }
        public string Year { set; get; }
    }
}