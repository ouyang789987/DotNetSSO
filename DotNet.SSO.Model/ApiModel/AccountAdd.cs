using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNet.Utils.DataValidate;
namespace DotNet.SSO.Model.ApiModel
{
    /// <summary>
    /// 说明:添加单点登录帐号接口
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:AccountAdd.cs
    /// </summary>
    public class AccountAdd
    {
        [Required(ErrorMessage="域标识不能为空")]
        public string DomainCode { get; set; }

        [StringLength(30,ErrorMessage="密码应在30字符以内")]
        public string Password { get; set; }

        [StringLength(256,ErrorMessage="备注过长,应在256字符内")]
        public string ReMark { get; set; }
    }
}
