namespace SocNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Users",
                url: "users",
                defaults: new { controller = "User", action = "GetAll" });
            routes.MapRoute(
                name: "User-control",
                url: "admin/users",
                defaults: new { controller = "User", action = "Control" });
            routes.MapRoute(
                name: "User-edit",
                url: "user/{id}/edit",
                defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "User-delete",
                url: "user/{id}/delete",
                defaults: new { controller = "User", action = "Delete" });
            routes.MapRoute(
                name: "User",
                url: "user/{id}",
                defaults: new { controller = "User", action = "Get" });
            routes.MapRoute(
                name: "User-create",
                url: "create-user",
                defaults: new { controller = "User", action = "Create" });
            routes.MapRoute(
                name: "User-registration",
                url: "registration",
                defaults: new { controller = "User", action = "Registration" });
            routes.MapRoute(
                name: "Roles",
                url: "roles",
                defaults: new { controller = "Role", action = "GetAll" });
            routes.MapRoute(
               name: "Role-control",
               url: "admin/roles",
               defaults: new { controller = "Role", action = "Control" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "LogOn" });
        }
    }
}
