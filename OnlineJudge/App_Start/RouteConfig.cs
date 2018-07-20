using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineJudge {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // order of routes is important,
            // angular route must be the last route 
            
            mapAngularRoutes(routes);

//            routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//            );
        }

        private static void mapAngularRoutes(RouteCollection routes){
            routes.MapRoute(
                name: "Angular Routing",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }

//        private static void mapApiRoutes(RouteCollection routes){
//            
//            routes.MapHttpRoute(
//                name: "API",
//                routeTemplate: "api/{action}/{id}",
//                defaults: new { controller = "Temp", action = "Index", id = UrlParameter.Optional }
//            );
//        }
    }
}
