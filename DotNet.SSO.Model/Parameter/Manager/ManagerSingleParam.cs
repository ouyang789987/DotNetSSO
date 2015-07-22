using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
   /// <summary>
    /// 说明:查询管理员
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:ManagerSingleParam.cs
    /// </summary>
    public class ManagerSingleParam
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        public int ManagerId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
    }
}
