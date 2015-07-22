using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.Model;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:单点认证的相关操作
    /// 作者:Tieria
    /// 时间:2015.07.21
    /// 文件名:SSOAuthHandler.cs
    /// </summary>
    public class SSOAuthHandler
    {

        #region 判断是否包含跳转页面
        /// <summary>
        /// 判断是否包含跳转页面
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static bool HasRedirect()
        {
            var redirectUrl = HttpContext.Current.Request["redirectUrl"];
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                string url = HttpUtility.UrlDecode(redirectUrl);
                if (DotNet.Utils.Untility.RegexValidate.IsURL(url))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 判断是否有domainCode参数,并且判断domainCode是否合法
        /// <summary>
        /// 判断是否有domainCode参数,并且判断domainCode是否合法
        /// </summary>
        /// <param name="domainCode"></param>
        /// <returns></returns>
        public static bool HasDomainCode()
        {
            var domainCode = HttpContext.Current.Request["domainCode"];
            if (!string.IsNullOrEmpty(domainCode))
            {
                string code = DotNet.Utils.Untility.StringHelper.FilterHtml(domainCode);
                IDomainDal domainDal = new DomainDal();
                var domain = domainDal.GetEntity(new DomainSingleParam() { DomainCode = code });
                if (domain != null && domain.DomainId > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
