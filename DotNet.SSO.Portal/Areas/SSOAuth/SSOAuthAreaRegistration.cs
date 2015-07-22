using System.Web.Mvc;

namespace DotNet.SSO.Portal.Areas.SSOAuth
{
    public class SSOAuthAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SSOAuth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SSOAuth_default",
                "SSOAuth/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
