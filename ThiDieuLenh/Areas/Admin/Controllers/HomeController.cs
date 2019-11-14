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
            else
            {
                ViewBag.AddQ = "Đã thêm thành công đề";
            }
            return View();
        }
    }
}