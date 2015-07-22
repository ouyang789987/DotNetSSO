using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;

namespace DotNet.SSO.IBll
{
   /// <summary>
    /// 说明:管理员业务接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IManagerService.cs
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// Manager帐号登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        JsonModel<string> ManagerLogin(ManagerLoginModel loginModel);
    }
}
