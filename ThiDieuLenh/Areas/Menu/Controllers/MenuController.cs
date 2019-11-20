using DbDieuLenh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ThiDieuLenh.Areas.Menu.Models;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Menu.Controllers
{
    public class MenuController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                var res = new AccountModel().Login(model.UserName, model.Password);
                if (res)
                {
                    var id = new AccountModel().GetId(model.UserName);
                    var NamHoc = new AccountModel().GetNamHoc(model.UserName);
                    if (id[0] == 'G')
                    {
                        var name = new AccountModel().GetName(id);
                        Session.Add(CommonConstant.ADMIN_SESSION, name);
                        return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Admin" }));
                    }
                    else
                    {
                        // Thi sinh
                        var name = new AccountModel().GetNameS(id, NamHoc);
                        Session.Add(CommonConstant.STUDENT_SESSION, name);
                        return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Student" }));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}