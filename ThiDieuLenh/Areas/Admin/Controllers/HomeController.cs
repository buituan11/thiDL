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

namespace ThiDieuLenh.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];               
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(HomeModel model)
        {
            ViewBag.User = Session[CommonConstant.ADMIN_SESSION];
            var file = model.File;
            if (file != null && file.ContentLength > 0)
            {
                foreach (Process proc in Process.GetProcessesByName("excel")){ proc.Kill(); }
                string filePath = HttpContext.Server.MapPath("~/dsCauHoi.xlsx");
                string extension = Path.GetExtension(file.FileName);
                string fileName = $"dsCauHoi{extension}";
                file.SaveAs(filePath);
                ViewBag.gtr = new AccountModel().AddQuestion(filePath);
                foreach (Process proc in Process.GetProcessesByName("excel")) { proc.Kill(); }
                
                ViewBag.AddQ = "Đã thêm thành công câu hỏi";
            }
            else if (model.id != null || model.FileS != null)
            {
                ViewBag.AddQ = "Đã thêm thành công hs";
                if (model.id != null)
                {
                    var res = new StudentModel().Login(model.id, model.NamHocS);
                    if (!res)
                    {
                        new StudentModel().Add(1,model.id, model.NamHocS, model.HoTen, model.Lop, model.ChuyenKhoa);
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
            else
            {
                ViewBag.AddQ = "Đã thêm thành công đề";
            }
            return View();
        }
    }
}