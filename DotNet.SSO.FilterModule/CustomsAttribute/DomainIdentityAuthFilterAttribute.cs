using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
namespace DotNet.SSO.FilterModule.CustomsAttribute
{
    /// <summary>
    /// 说明:域身份识别过滤器
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:DomainIdentityAuthFilterAttribute.cs
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DomainIdentityAuthFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
       
        }
    }
}
