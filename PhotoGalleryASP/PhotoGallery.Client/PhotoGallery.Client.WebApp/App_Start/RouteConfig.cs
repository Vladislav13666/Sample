using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoGallery.Client.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserPhotos",
                url: "{login}",
                defaults: new { controller = "Album", action = "UserPhotos" });

            routes.MapRoute(
                name: "Manage",
                url: "{login}/manage",
                defaults: new { controller = "UserAlbum", action = "Manage" });

            routes.MapRoute(
                name: "Profile",
                url: "{login}/profile",
                defaults: new { controller = "User", action = "EditProfile" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Album", action = "AllPhotos", id = UrlParameter.Optional }
            );
        }
    }
}
