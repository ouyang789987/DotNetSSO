using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.ViewModel;

namespace DotNet.SSO.IBll
{
    /// <summary>
    /// 说明:单点登录认证接口
    /// 作者:Tieria
    /// 时间:2015.07.21
    /// 文件名:ISSOAuthService.cs
    /// </summary>
    public interface ISSOAuthService
    {
        /// <summary>
        /// 单点登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<string> Login(SSOLoginModel model);
    }
}
