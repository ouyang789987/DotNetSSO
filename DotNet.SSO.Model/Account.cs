using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
   /// <summary>
    /// 说明:帐号实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:Account.cs
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 帐号Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 加密的密钥
        /// </summary>
        public string EncryptKey { get; set; }
        /// <summary>
        /// 安全密码
        /// </summary>
        public string SafePassword { get; set; }
        /// <summary>
        /// 是否绑定安全密码
        /// </summary>
        public int SafeBinding { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 绑定手机号码
        /// </summary>
        public int MobileBinding { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否绑定邮箱
        /// </summary>
        public int EmailBinding { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public int DelFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建域的Id
        /// </summary>
        public int SubmitDomainId { get; set; }
    }
}
