using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
    /// <summary>
    /// 说明:域分页数据
    /// 作者:Tieria
    /// 时间:2015.07.12
    /// 文件名:DomainParam.cs
    /// </summary>
    public class DomainParam:ParameterContext
    {
        /// <summary>
        /// 根据域名称进行查询
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 域的唯一编码
        /// </summary>
        public string DomainCode { get; set; }
        /// <summary>
        /// 域等级
        /// </summary>
        public int DomainLevel { get; set; }
        /// <summary>
        /// 上一级的Id
        /// </summary>
        public int ParentDomainId { get; set; }
        /// <summary>
        /// 上一级的唯一编码
        /// </summary>
        public string ParentDomainCode { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }
        /// <summary>
        /// 是否单点登录
        /// </summary>
        public int IsSSO { get; set; }
        /// <summary>
        /// 是否使用统一帐号进行登录
        /// </summary>
        public int UnifiedAccount { get; set; }
        /// <summary>
        /// 域的网址
        /// </summary>
        public string DomainUrl { get; set; }
        /// <summary>
        /// 根据池子Id进行查询
        /// </summary>
        public int SSOPoolPoolId { get; set; }
    }
}
