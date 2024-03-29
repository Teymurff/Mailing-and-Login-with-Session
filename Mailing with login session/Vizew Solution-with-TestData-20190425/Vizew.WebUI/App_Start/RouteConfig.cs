﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Vizew.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Mail", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "Vizew.WebUI.Controllers" }
            );
        }
    }
}
