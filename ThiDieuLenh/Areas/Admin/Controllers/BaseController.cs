using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ThiDieuLenh.Common;

namespace ThiDieuLenh.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[CommonConstant.ADMIN_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Menu", action = "Index", Area = "Menu" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}