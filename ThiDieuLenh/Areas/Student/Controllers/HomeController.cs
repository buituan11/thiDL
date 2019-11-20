using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Student.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Student/Home
        public ActionResult Index()
        {
            ViewBag.User = Session[CommonConstant.STUDENT_SESSION];
            return View();
        }
    }
}