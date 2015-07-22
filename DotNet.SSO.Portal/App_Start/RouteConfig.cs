using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotNet.SSO.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "SSOLogin",
            url: "Login",
            defaults: new { area = "SSOAuth", controller = "Login", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "DotNet.SSO.Portal.Areas.SSOAuth.Controllers" }).DataTokens.Add("Area", "SSOAuth");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "Manager", controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DotNet.SSO.Portal.Areas.Manager.Controllers" }
            ).DataTokens.Add("Area", "Manager");
            //
            //routes.MapRoute(
            //name: "SSOLogin",
            //url: "Login",
            //defaults: new { area = "SSOAuth", controller = "Login", action = "Index", id = UrlParameter.Optional },
            //namespaces: new string[] { "DotNet.SSO.Portal.Areas.SSOAuth.Controllers" }).DataTokens.Add("Area","SSOAuth");
        }
    }
}