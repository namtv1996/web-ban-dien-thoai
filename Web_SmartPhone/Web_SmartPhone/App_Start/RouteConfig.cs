using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_SmartPhone
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"Web_SmartPhone.Controller"}
            );
            routes.MapRoute(
               name: "Add cart",
               url: "them-gio-hang",
               defaults: new { controller = "Home", action = "ShoppingCart", id = UrlParameter.Optional },
               namespaces: new[] { "Web_SmartPhone.Controller" }
           );
        }
    }
}
