using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Enum
{
    /// <summary>
    /// 说明:帐号字段唯一性验证
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:AccountVerifyTypeEnum.cs
    /// </summary>
    public enum AccountVerifyTypeEnum
    {
        /// <summary>
        /// 验证OpenId
        /// </summary>
        OpenId=1,
        /// <summary>
        /// 验证登录名
        /// </summary>
        LoginName=2,
        /// <summary>
        /// 验证手机号码
        /// </summary>
        Mobile=4,
        /// <summary>
        /// Email邮箱
        /// </summary>
        Email=8
    }
}
