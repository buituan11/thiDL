using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];               
            return View();
        }
    }
}