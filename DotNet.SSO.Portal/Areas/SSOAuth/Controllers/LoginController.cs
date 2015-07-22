using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.SSO.Portal.Areas.SSOAuth.Controllers
{
    public class LoginController : Controller
    {
        //单点登录的登录页面
        // GET: /SSOAuth/Login/Index
        #region 单点登录的登录页面
        [HttpGet]
        public ActionResult Index()
        {
            //http%3a%2f%2flocalhost%3a9000%2fManager%2fHome%2fIndex
            bool hasRedirect = BllUtility.SSOAuthHandler.HasRedirect();
            bool hasDomainCode = BllUtility.SSOAuthHandler.HasDomainCode();
            if (hasRedirect && hasDomainCode)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Joke");
            }
        } 
        #endregion

        [HttpGet]
        public ActionResult Joke()
        {

            return View();
        }


    }
}
