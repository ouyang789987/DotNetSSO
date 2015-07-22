using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DotNet.SSO.Model.ApiModel
{
    /// <summary>
    /// 说明:帐号登录
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:AccountLogin.cs
    /// </summary>
    public class AccountLogin
    {
        /// <summary>
        /// 域标识
        /// </summary>
        [Required(ErrorMessage="请传入域标识")]
        [StringLength(40, ErrorMessage = "域标识不正确")]
        public string DomainCode { get; set; }
        /// <summary>
        /// 帐号的OpenId
        /// </summary>
        [Required(ErrorMessage="请传入帐号的OpenId")]
        [StringLength(40,ErrorMessage="OpenId不正确")]
        public string OpenId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(30,ErrorMessage="密码长度不正确")]
        public string Password { get; set; }
    }
}
