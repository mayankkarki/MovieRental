using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieRental
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //specific to generic

            routes.MapMvcAttributeRoutes();

            //this part is not necessary after using attrbiute routing 
            //routes.MapRoute("MoviesByReleaseDate",
            //    "movies/released/{year}/{month}",
            //    new { controller = "Movies", action = "ByReleaseDate" },
            //    new { year = @"\d{4}", month = @"\d{2}" });
            //for or condition use |, so year can be limit to 2015 or 2016 by using @"2015|2016"

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
