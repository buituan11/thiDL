using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Areas.Admin.Models;
using System.Security.Principal;
using DbDieuLenh;
using ThiDieuLenh.Common;

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
            if (ModelState.IsValid)
            {
                var res = new AccountModel().Login(model.User, model.Pass);
                if (res)
                {
                    var name = new AccountModel().GetName(model.User);
                    Session.Add(CommonConstant.ADMIN_SESSION, name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                    return View(model);
                }
            }
            return View("Index");
        }
    }
}