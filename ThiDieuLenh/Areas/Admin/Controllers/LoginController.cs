using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Areas.Admin.Models;
using ThiDieuLenh.Areas.Admin.Code;
using Database;

namespace ThiDieuLenh.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var res = new AccountModel().Login(model.User, model.Pass);
            SessionHelper.SetSession(new UserSession() { User = model.User });
            if  (res && ModelState.IsValid)
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View(model);
            }
        }
    }
}