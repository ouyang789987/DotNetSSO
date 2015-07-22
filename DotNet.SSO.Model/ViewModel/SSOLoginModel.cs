using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet.Utils.DataValidate;
namespace DotNet.SSO.Model.ViewModel
{
    /// <summary>
    /// 说明:单点登录实体
    /// 作者:Tieria
    /// 时间:2015.07.22
    /// 文件名:SSOLoginModel.cs
    /// </summary>
    public class SSOLoginModel
    {
        [Required(ErrorMessage="请输入帐号/手机号码/邮箱")]
        [StringLength(40,ErrorMessage="输入的帐号长度过长")]
        public string LoginStr { get; set; }

        [Required(ErrorMessage="请输入登录密码")]
        public string LoginPassword { get; set; }

        [StringLength(50,ErrorMessage="验证码的key值")]
        public string VerifyKey { get; set; } 

        [StringLength(20,ErrorMessage="验证码长度太长")]
        public string Code { get; set; }
    }
}
