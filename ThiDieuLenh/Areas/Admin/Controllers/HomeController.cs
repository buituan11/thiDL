using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThiDieuLenh.Areas.Admin.Models;
using ThiDieuLenh.Common;
using DbDieuLenh;
using System.Diagnostics;
using System.Web.Routing;

namespace ThiDieuLenh.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            ViewBag.dsTK = new AccountModel().getDS();
            return View();
        }
        [HttpGet]
        public ActionResult CauHoi()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            return View();
        }
        [HttpGet]
        public ActionResult HocSinh()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            return View();
        }
        [HttpGet]
        public ActionResult GiaoVien()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            ViewBag.dsGV = new AccountModel().getDSGV();
            return View();
        }
        [HttpGet]
        public ActionResult TaiKhoan()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            ViewBag.dsTK = new AccountModel().getDS();
            return View();
        }
        [HttpGet]
        public ActionResult DeleteTK(int id)
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            ViewBag.dsTK = new AccountModel().getDS();
            ViewBag.stt = id;
            ViewBag.tk = new AccountModel().getTK(id);
            return View();
        }
        [HttpGet]
        public ActionResult DeleteGV(int id)
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            ViewBag.dsGV = new AccountModel().getDSGV();
            ViewBag.stt = id;
            ViewBag.tk = new AccountModel().getGV(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HomeModel model)
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            if (model.NamHoc != null)
            {
                ViewBag.AddQ = "Đã thêm thành công đề";
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", Area = "Admin" }));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CauHoi(HomeModel model)
        {
            var file = model.File;
            if (file != null && file.ContentLength > 0)                 //Them cau hoi
            {
                foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                string filePath = HttpContext.Server.MapPath("~/dsCauHoi.xlsx");
                string extension = Path.GetExtension(file.FileName);
                string fileName = $"dsCauHoi{extension}";
                file.SaveAs(filePath);
                ViewBag.gtr = new AccountModel().AddQuestion(filePath);
                foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }

                ViewBag.AddQ = "Đã thêm thành công câu hỏi";
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "CauHoi", Area = "Admin" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HocSinh(HomeModel model)
        {
            if (model.id != null || model.FileS != null)           //Them thi sinh
            {
                ViewBag.AddQ = "Đã thêm thành công hoc sinh";
                if (model.id != null)
                {
                    var res = new StudentModel().Login(model.id, model.NamHocS1);
                    if (!res)
                    {
                        new StudentModel().Add(1, model.id, model.NamHocS1, model.NamHocS2, model.HoTen, model.Lop, model.ChuyenKhoa);
                    }
                    else
                    {
                        ViewBag.AddQ = "Thí sinh đã có trong danh sách";
                    }
                }
                var fileS = model.FileS;
                if (fileS != null && fileS.ContentLength > 0)
                {
                    foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                    string filePath = HttpContext.Server.MapPath("~/dsThiSinh.xlsx");
                    string extension = Path.GetExtension(fileS.FileName);
                    string fileName = $"dsThiSinh{extension}";
                    fileS.SaveAs(filePath);
                    new StudentModel().AddS(filePath);
                    foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                }
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "HocSinh", Area = "Admin" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiaoVien(HomeModel model)
        {
            ViewBag.dsGV = new AccountModel().getDSGV();
            if (model.NamHocGV != null || model.FileGV != null)
            {
                ViewBag.AddQ = "Đã thêm thành công giáo viên";
                if (model.NamHocGV != null)
                {
                    var res = new AccountModel().Login(model.TaiKhoan, model.MatKhau);
                    if (!res)
                    {
                        new AccountModel().AddGV(model.NamHocGV, model.HoTenGV, model.TaiKhoan, model.MatKhau);
                    }
                    else
                        ViewBag.AddQ = "Giáo viên đã có trong danh sách";
                }
                var fileGV = model.FileGV;
                if (fileGV != null && fileGV.ContentLength > 0)
                {
                    foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                    string filePath = HttpContext.Server.MapPath("~/dsGV.xlsx");
                    string extension = Path.GetExtension(fileGV.FileName);
                    string fileName = $"dsGV{extension}";
                    fileGV.SaveAs(filePath);
                    new AccountModel().AddDsGV(filePath);
                    foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                }
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "GiaoVien", Area = "Admin" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaiKhoan(HomeModel model)
        {
            if (model.TKQL != null)
            {
                ViewBag.dsTK = new AccountModel().getDS();
                ViewBag.AddQ = "Đã thêm thành công tài khoản";
                if (model.TKQL != null)
                {
                    var res = new AccountModel().Login(model.TKQL, model.MKQL);
                    if (!res)
                    {
                        new AccountModel().AddQL(model.TKQL, model.MKQL, model.NamHocQL);
                    }
                }
            }
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "TaiKhoan", Area = "Admin" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTKK(int id)
        {
            string tk = new AccountModel().getIDTK(id);
            if (tk != "")
                new AccountModel().deleteTK(tk);
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "TaiKhoan", Area = "Admin" }));
        }
    }
}