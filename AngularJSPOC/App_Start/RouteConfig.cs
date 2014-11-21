using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AngularJSPOC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // default route goes to our angularjs page
            //routes.MapRoute(
            //    name: "AngularJS",
            //    url: "{*any}",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            // Route override to work with Angularjs and HTML5 routing
            //routes.MapRoute(
            //    name: "AngularJSRouting",
            //    url: "Employee/{*.}",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
            );

            //  routes.MapRoute(
            //    name: "Employee",
            //    url: "Employee/{*catchall}",
            //    defaults: new { controller = "Employee", action = "Index" }
            //);

        //    routes.MapRoute(
        //    name: "Application1Override",
        //    url: "Employee/{*.}",
        //    defaults: new { controller = "Employee", action = "Index" }
        //);
        }
    }
}