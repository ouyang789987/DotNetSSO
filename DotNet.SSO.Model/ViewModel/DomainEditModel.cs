using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet.Utils.DataValidate;
using DotNet.SSO.Model.Enum;

namespace DotNet.SSO.Model.ViewModel
{
    /// <summary>
    /// 说明:修改域
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:DomainEditModel.cs
    /// </summary>
    public class DomainEditModel
    {
        /// <summary>
        /// 域的Id
        /// </summary>
        [Required(ErrorMessage="域Id不正确")]
        public int DomainId { get; set; }
        /// <summary>
        /// 域名称
        /// </summary>
        [Required(ErrorMessage = "请输入域的名称")]
        [StringLength(80, ErrorMessage = "域名称长度在80字以内")]
        public string DomainName { get; set; }

        //[Display(Description="密码在6-25字符,包含英文")]
        //[Required(ErrorMessage="请输入域的密码")]
        //[StringLength(30,ErrorMessage="域的密码在30字以内")]
        //[PasswordOne(ErrorMessage="密码格式不正确")]
        //public string DomainPassword { get; set; }
        /// <summary>
        /// 域网址
        /// </summary>
        [Required(ErrorMessage = "请输入域的Url地址")]
        [StringLength(256, ErrorMessage = "域的Url地址应该在256字以内")]
        [UrlAddress(ErrorMessage = "域的Url地址不正确")]
        public string DomainUrl { get; set; }
        /// <summary>
        /// 域的级别
        /// </summary>
        [Range(1, 5, ErrorMessage = "请选择域的级别")]
        public int DomainLevel { get; set; }
        /// <summary>
        /// 父级的域Id
        /// </summary>
        //[RemoteMethod("","","",ErrorMessage="上一级域不存在或者未启用")]
        public int ParentDomainId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [InEnum(typeof(IsEnabledEnum), ErrorMessage = "请选择是否启用")]
        public int IsEnabled { get; set; }
        /// <summary>
        /// 是否启用单点登录
        /// </summary>
        [InEnum(typeof(IsSSOEnum), ErrorMessage = "请选择是否启用单点登录")]
        public int IsSSO { get; set; }
        /// <summary>
        /// 单点登录的地址
        /// </summary>
        [Required(ErrorMessage = "请输入单点登录的Url地址")]
        [StringLength(256, ErrorMessage = "单点登录的Url地址应该在256字以内")]
        [UrlAddress(ErrorMessage = "单点登录的Url地址不正确")]
        public string SSOUrl { get; set; }
        /// <summary>
        /// cookie的域网址
        /// </summary>
        [Required(ErrorMessage = "请输入Cookie所存储的域地址")]
        [StringLength(256, ErrorMessage = "Cookie的域网址应在256字以内")]
        public string CookieDomain { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256, ErrorMessage = "备注应在256字以内")]
        public string ReMark { get; set; }
        /// <summary>
        /// 单点登录池Id
        /// </summary>
        [Range(1, 9999999, ErrorMessage = "请选择单点登录池")]
        //[RemoteMethod("","","",ErrorMessage="该单点登录池不存在或者未启用")]
        public int SSOPoolPoolId { get; set; }
    }
}
