using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.ApiModel
{
    /// <summary>
    /// 说明:单点登录帐号缓存
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:OpenCache.cs
    /// </summary>
    public class OpenCache
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 域标识符
        /// </summary>
        public string DomainCode { get; set; }

        
    }
}
