using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThiDieuLenh.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Index()
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Menu", action = "Index", Area = "Menu" })); ;
        }
    }
}