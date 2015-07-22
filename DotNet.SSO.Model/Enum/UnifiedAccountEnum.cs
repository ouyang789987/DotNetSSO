using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Enum
{
    /// <summary>
    /// 说明:是否使用统一帐号枚举
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:UnifiedAccountEnum.cs
    /// </summary>
    public enum UnifiedAccountEnum
    {
        /// <summary>
        /// 使用统一帐号进行登录,那么就需要提交的是认证中心的帐号密码
        /// </summary>
        Unified=1,
        /// <summary>
        /// 独立帐号登录,每个系统自己进行登录认证,然后提交到认证中心登录的状态
        /// </summary>
        Independent=2
    }
}
