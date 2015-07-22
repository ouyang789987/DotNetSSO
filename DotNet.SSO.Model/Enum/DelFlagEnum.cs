using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Enum
{
   /// <summary>
    /// 说明:删除标识枚举
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:DelFlagEnum.cs
    /// </summary>
    public enum DelFlagEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Noraml = 1,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        LogicalDelete = 2,
        /// <summary>
        /// 物理删除
        /// </summary>
        PhysicDelete = 4

    }
}
