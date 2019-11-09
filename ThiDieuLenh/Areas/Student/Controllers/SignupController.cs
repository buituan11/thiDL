using DbDieuLenh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Areas.Student.Models;

namespace ThiDieuLenh.Areas.Student.Controllers
{
    public class SignupController : Controller
    {
        // GET: Student/Signup
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                var res = new StudentModel().Login(model.SoHieu);
                if (!res)
                {
                    new StudentModel().Add(model.SoHieu, model.HoTen, model.Lop, model.ChuyenKhoa);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Thí sinh đã nằm trong danh sách thi");
                    return View(model);
                }
            }
            return View("Index");
        }
    }
}