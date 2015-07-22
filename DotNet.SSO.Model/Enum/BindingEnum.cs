using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Enum
{
    /// <summary>
    /// 说明:绑定枚举
    /// 作者:Tieria
    /// 时间:2015.07.16
    /// 文件名:BindingEnum.cs
    /// </summary>
    public enum BindingEnum
    {
        /// <summary>
        /// 未绑定
        /// </summary>
        NotBinded=1,
        /// <summary>
        /// 正在绑定中
        /// </summary>
        Binding=2,
        /// <summary>
        /// 已经绑定
        /// </summary>
        Binded=4
    }
}
