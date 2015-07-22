using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DotNet.SSO.Model.ViewModel
{
   /// <summary>
    /// 说明:单点登录池登录实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:SSOPoolAddModel.cs
    /// </summary>
    public class SSOPoolAddModel
    {
        /// <summary>
        /// 池名称
        /// </summary>
        [Required(ErrorMessage="请输入单点登录池的名称")]
        public string PoolName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Range(1, 100, ErrorMessage = "请选择单点登录池的开启状态")]
        public int IsEnabled { get; set; }
        /// <summary>
        /// 最大的数量
        /// </summary>
        [Range(2,9999999,ErrorMessage="请输入单点登录池的最大域数量")]
        public int MaxAmount { get; set; }
        /// <summary>
        /// 主验证域的Id
        /// </summary>
        
        public int MainDomainId { get; set; }
        [StringLength(256,ErrorMessage="备注的最大长度不能超过256字")]
        public string ReMark { get; set; }
    }
}
