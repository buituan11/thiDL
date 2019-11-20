using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThiDieuLenh.Areas.Menu.Models
{
    public class MenuModel
    {
        [Required]
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}