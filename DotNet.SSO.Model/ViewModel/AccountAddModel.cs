using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet.Utils.DataValidate;
namespace DotNet.SSO.Model.ViewModel
{
    /// <summary>
    /// 说明:帐号添加实体
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:AccountAddModel.cs
    /// </summary>
    public class AccountAddModel
    {
        [Required(ErrorMessage="用户名不能为空")]
        [LoginName(ErrorMessage="用户名格式不正确,必须为6-25位,英文字母(必须包含英文字母)+数字的形式")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [PasswordOne(ErrorMessage = "密码格式不正确,8-25位,英文字母+数字+特殊符号")]
        public string Password { get; set; }

        public string SafePassword { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage="必须包含提交域的域标识")]
        public string SubmitDomainCode { get; set; }

        [StringLength(256, ErrorMessage = "备注在256字符以内")]
        public string ReMark { get; set; }



    }
}
