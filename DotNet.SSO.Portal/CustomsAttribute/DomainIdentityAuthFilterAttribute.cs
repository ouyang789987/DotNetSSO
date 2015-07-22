using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.SSO.Portal.CustomsAttribute
{
    /// <summary>
    /// 说明:域身份过滤器
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:DomainIdentityAuthFilterAttribute.cs
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DomainIdentityAuthFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            /*
            string domainCode = string.Empty;
            string domainPassword = string.Empty;
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["DomainCode"]))
            {
                domainCode = filterContext.HttpContext.Request.Headers["DomainCode"];
            }
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["domainPassword"]))
            {
                domainCode = filterContext.HttpContext.Request.Headers["domainPassword"];
            }
            //认证,是否为域的拥有者
            var auth = BllUtility.DomainHandler.DomainIdentityAuth(domainCode, domainPassword);
            if (auth.Success)
            {
                return;
            }
            else
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new { success = false, msg = auth.ErrMsg },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            */
        }
    }
}