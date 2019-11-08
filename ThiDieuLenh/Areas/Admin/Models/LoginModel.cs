using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ThiDieuLenh.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public string User { set; get; }
        public string Pass { set; get; }
    }

    internal class RequiredAttribute : Attribute
    {
    }
}