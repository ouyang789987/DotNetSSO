using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.ViewModel
{
    /// <summary>
    /// 说明:删除帐号的实体
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:AccountDeleteModel.cs
    /// </summary>
    public class AccountDeleteModel
    {
        /// <summary>
        /// 根据帐号的主键Id
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// 根据帐号的OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 域标识
        /// </summary>
     //   public string DomainCode { get; set; }
        /// <summary>
        /// 域密码
        /// </summary>
     //   public string DomainPassword { get; set; }
    }
}
