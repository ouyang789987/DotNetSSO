using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
    /// <summary>
    /// 说明:单点登录池
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:SSOPool.cs
    /// </summary>
    public class SSOPool
    {
        /// <summary>
        /// 池的Id
        /// </summary>
        public int PoolId { get; set; }
        /// <summary>
        /// 最大的数量
        /// </summary>
        public int MaxAmount { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 池的名称
        /// </summary>
        public string PoolName { get; set; }
        /// <summary>
        /// 主域Id
        /// </summary>
        public int MainDomainId { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public int DelFlag { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark { get; set; }
      
    }
}
