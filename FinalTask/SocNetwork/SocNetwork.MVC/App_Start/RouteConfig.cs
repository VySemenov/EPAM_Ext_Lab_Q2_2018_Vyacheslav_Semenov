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
                name: "User edit profile",
                url: "settings",
                defaults: new { controller = "UserDetailInfo", action = "Edit" });

            routes.MapRoute(
                name: "Roles",
                url: "roles",
                defaults: new { controller = "Role", action = "GetAll" });
            routes.MapRoute(
               name: "Role-control",
               url: "admin/roles",
               defaults: new { controller = "Role", action = "Control" });

            routes.MapRoute(
                name: "Friends",
                url: "friends",
                defaults: new { controller = "Friend", action = "Index" });
            routes.MapRoute(
                name: "GetFriends",
                url: "user/{userId}/friends",
                defaults: new { controller = "Friend", action = "Get", userId = UrlParameter.Optional });
            routes.MapRoute(
                name: "Incoming friend request",
                url: "friends/incoming",
                defaults: new { controller = "Friend", action = "GetIncomingRequest" });
            routes.MapRoute(
                name: "Outgoing friend request",
                url: "friends/outgoing",
                defaults: new { controller = "Friend", action = "GetOutgoingRequest" });
            routes.MapRoute(
                name: "Add friend",
                url: "user/{fid}/add",
                defaults: new { controller = "Friend", action = "SendRequest", fid = UrlParameter.Optional });
            routes.MapRoute(
                name: "Accept friend request",
                url: "user/{fid}/accept",
                defaults: new { controller = "Friend", action = "AcceptRequest", fid = UrlParameter.Optional });
            routes.MapRoute(
                name: "Dismiss friend request",
                url: "user/{fid}/dismiss",
                defaults: new { controller = "Friend", action = "DismissRequest", fid = UrlParameter.Optional });

            routes.MapRoute(
                name: "Post create",
                url: "create-post",
                defaults: new { controller = "Post", action = "Create" });
            routes.MapRoute(
                name: "Post delete",
                url: "post/{id}/delete",
                defaults: new { controller = "Post", action = "Delete", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Post edit",
                url: "post/{id}/edit",
                defaults: new { controller = "Post", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Search", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "LogOn" });
        }
    }
}
