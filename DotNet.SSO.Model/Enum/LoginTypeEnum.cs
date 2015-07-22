using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Enum
{
    /// <summary>
    /// 说明:登录的类型枚举
    /// 作者:Tieria
    /// 时间:2015.07.22
    /// 文件名:LoginTypeEnum.cs
    /// </summary>
    public enum LoginTypeEnum
    {
        /// <summary>
        /// 使用帐号登录
        /// </summary>
        LoginName=1,
        /// <summary>
        /// 使用手机号码登陆
        /// </summary>
        LoginMobile=2,
        /// <summary>
        /// 使用Email登录
        /// </summary>
        LoginEmail=4
    }
}
