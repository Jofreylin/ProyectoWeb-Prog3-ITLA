using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp_Prog3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CategoriasAdmin",
                url: "Categoria/edit/",
                defaults: new { controller = "Categoria", action = "Index"}
            );

            routes.MapRoute(
                name: "EditarPostPoster",
                url: "UserPoster/EditarPost/",
                defaults: new { controller = "UserPoster", action = "ProfileAcc" }
            );

            routes.MapRoute(
                name: "GestionarPosterAdmin",
                url: "UserAdmin/GestionUserPoster/",
                defaults: new { controller = "UserAdmin", action = "BusquedaUserPoster"}
            );

            routes.MapRoute(
                name: "InfoPostPoster",
                url: "UserPoster/InfoPost/",
                defaults: new { controller = "UserPoster", action = "ProfileAcc" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
