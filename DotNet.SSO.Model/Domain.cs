using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
   /// <summary>
    /// 说明:域实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:Domain.cs
    /// </summary>
    public class Domain
    {
        public int DomainId { get; set; }
        /// <summary>
        /// 唯一的身份标识
        /// </summary>
        public string DomainCode { get; set; }
        /// <summary>
        /// 域的密钥
        /// </summary>
        public string DomainKey { get; set; }
        /// <summary>
        /// 域的密码
        /// </summary>
        public string DomainPassword { get; set; }
        /// <summary>
        /// 域名称
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 域地址
        /// </summary>
        public string DomainUrl { get; set; }
        /// <summary>
        /// cookie中存放的域
        /// </summary>
        public string CookieDomain { get; set; }
        /// <summary>
        /// 域名的等级
        /// </summary>
        public int DomainLevel { get; set; }
        /// <summary>
        /// 父级域Id
        /// </summary>
        public int ParentDomainId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 是否单点登录
        /// </summary>
        public int IsSSO { get; set; }
        /// <summary>
        /// 如果单点登录,那么需要填写单点登录的Url地址
        /// </summary>
        public string SSOUrl { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public int DelFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark { get; set; }
        /// <summary>
        /// 单点登录的池子Id
        /// </summary>
        public int SSOPoolPoolId { get; set; }

     
        /// <summary>
        /// 单点登录池
        /// </summary>
        public SSOPool Pool { get; set; }
    }
}
