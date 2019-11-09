using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Student.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[CommonConstant.STUDENT_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Student" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}