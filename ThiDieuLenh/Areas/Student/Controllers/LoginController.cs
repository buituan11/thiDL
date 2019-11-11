using DbDieuLenh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Areas.Student.Models;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Student.Controllers
{
    public class LoginController : Controller
    {
        // GET: Student/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var res = new StudentModel().Login(model.SoHieu,model.NamHoc);
                if (res)
                {
                    Session.Add(CommonConstant.STUDENT_SESSION, model.SoHieu);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Thí sinh chưa nằm trong danh sách thi");
                    return View(model);
                }
            }
            return View("Index");
        }
    }
}