using AutoMapper;
using MovieRental.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MovieRental
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly MapperConfiguration AutoMapperConfiguation = new MapperConfiguration(mapper => mapper.AddProfile<MappingProfile>());
        
        protected void Application_Start()
        {            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
