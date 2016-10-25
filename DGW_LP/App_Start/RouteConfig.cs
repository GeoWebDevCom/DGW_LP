using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DGW_LP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("BinhChon", "binhchon",
           new { controller = "Home", action = "BinhChon" });

            routes.MapRoute("TheLe", "thele",
            new { controller = "Home", action = "TheLe" });

            routes.MapRoute("ClipReview", "watch/{cId}",
                       new { controller = "Home", action = "ClipReview"});


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
