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
            if (!ModelState.IsValid)            // Khong can check ModalState ??
            {
                var res = false;
                var id = "";
                var NamHoc = "";
                if (model.UserS != null && model.Password != null)
                {
                   res = new AccountModel().Login(model.UserS, model.Password);
                   if (res)
                    {
                        id = new AccountModel().GetId(model.UserS);
                        NamHoc = new AccountModel().GetNamHoc(model.UserS);
                        if ('0' <= id[0] && id[0] <= '9')
                        {
                            //var name = new AccountModel().GetName(id);            // Viet GetName cho Student
                            var name = "them GetName cho Student";
                            Session.Add(CommonConstant.STUDENT_SESSION, name);
                            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Student" }));
                        }
                        else
                        {
                            ModelState.AddModelError("", "Số hiệu hoặc mật khẩu không đúng");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Số hiệu hoặc mật khẩu không đúng");
                        return View();
                    }
                }
                else if (model.UserQL != null && model.Password != null)
                {
                    res = new AccountModel().Login(model.UserQL, model.Password);
                    if (res)
                    {
                        id = new AccountModel().GetId(model.UserQL);
                        NamHoc = new AccountModel().GetNamHoc(model.UserQL);
                        if (id[0] == 'A')
                        {
                            var name = new AccountModel().GetName(id);
                            Session.Add(CommonConstant.ADMIN_SESSION, name);
                            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Admin" }));
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                    return View();
                }
            }
            else
            {
                var res = false;
                var id = "";
                var NamHoc = "";
                if (model.UserName != null && model.Password != null)
                {
                    res = new AccountModel().Login(model.UserName, model.Password);
                    if (res)
                    {
                        id = new AccountModel().GetId(model.UserName);
                        NamHoc = new AccountModel().GetNamHoc(model.UserName);
                        if (id[0] == 'G')
                        {
                            var name = new AccountModel().GetName(id);
                            Session.Add(CommonConstant.ADMIN_SESSION, name);
                            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Admin" }));
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                    return View();
                }
            }
        }
    }
}