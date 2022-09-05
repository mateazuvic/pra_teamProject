using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kwizzotronic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "SelectLanguage",
                 url: "Start/SelectLanguage",
                 defaults: new
                 {
                     controller = "Start",
                     action = "SelectLanguage"
                 }
             );

            routes.MapRoute(
                name: "SelectMode",
                url: "Start/SelectMode",
                defaults: new
                {
                    controller = "Start",
                    action = "SelectMode"
                }
            );
            routes.MapRoute(
                name: "Auth",
                url: "Auth/Index",
                defaults: new
                {
                    controller = "Auth",
                    action = "Index"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Start", action = "SelectLanguage", id = UrlParameter.Optional }
            );
        }
    }
}
