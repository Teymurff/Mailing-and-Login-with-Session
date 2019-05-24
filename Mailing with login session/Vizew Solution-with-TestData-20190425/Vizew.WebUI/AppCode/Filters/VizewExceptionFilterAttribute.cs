using System;
using System.Web.Mvc;

namespace Vizew.WebUI
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class VizewExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var area = (filterContext.RouteData.Values["area"] ?? "").ToString();
            var controller = (filterContext.RouteData.Values["controller"] ?? "").ToString();
            var action = (filterContext.RouteData.Values["action"] ?? "").ToString();

            //səbəbkar erroru tapmaq
            while (filterContext.Exception.InnerException!=null)
                filterContext.Exception = filterContext.Exception.InnerException;

            var model = new HandleErrorInfo(filterContext.Exception, controller, action);

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                MasterName = "~/Views/Shared/_Layout.cshtml",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };

            filterContext.ExceptionHandled = true;
        }
    }
}