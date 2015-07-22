using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
    /// <summary>
    /// 说明:单点登录池搜索参数
    /// 作者:Tieria
    /// 时间:2015.07.12
    /// 文件名:SSOPoolParam.cs
    /// </summary>
    public class SSOPoolParam:ParameterContext
    {
        /// <summary>
        /// 根据Id进行搜索
        /// </summary>
        public int PoolId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 池名称
        /// </summary>
        public string PoolName { get; set; }
    }
}
