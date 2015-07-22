using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
    /// <summary>
    /// 说明:帐号单个查询参数
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:AccountSingleParam.cs
    /// </summary>
    public class AccountSingleParam
    {
        /// <summary>
        /// 帐号的Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 帐号的OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
