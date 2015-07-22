using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet.Utils.DataValidate;
namespace DotNet.SSO.Model
{
   /// <summary>
    /// 说明:管理员登录实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:ManagerLoginModel.cs
    /// </summary>
    public class ManagerLoginModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required(ErrorMessage = "请输入登录名")]
        [RegularExpression(@"^(?=[A-Za-z])[A-Za-z\d]{5,20}$", ErrorMessage = "登录名格式不正确")]
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        [PasswordOne(ErrorMessage="密码格式不正确")]
        public string LoginPwd { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "请输入验证码")]
        [StringLength(10,ErrorMessage="验证码长度在10字以内")]
        public string ImageCode { get; set; }
    }
}
