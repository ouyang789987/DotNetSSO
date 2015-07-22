using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.SSO.Portal.CustomsAttribute;
using DotNet.SSO.IBll;
using DotNet.SSO.Bll;
using DotNet.SSO.Model.ApiModel;

namespace DotNet.SSO.Portal.Areas.SSOAuth.Controllers
{
   // [DomainIdentityAuthFilter]
    public class AccountController : Controller
    {
        // POST: /SSOAuth/Account/

        #region 添加域下的单点登录帐号
        [HttpPost]
        public ActionResult AddAccount(AccountAdd model)
        {

            return Json("",JsonRequestBehavior.AllowGet);
        }
        #endregion


        [HttpPost]
        public ActionResult GetLogin()
        {
            return Json(new { },JsonRequestBehavior.AllowGet);
        }
    }
}
