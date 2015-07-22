using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
    /// <summary>
    /// 说明:查询唯一域的参数
    /// 作者:Tieria
    /// 时间:2015.07.15
    /// 文件名:DomainSingleParam.cs
    /// </summary>
    public class DomainSingleParam
    {
        /// <summary>
        /// 域的Id
        /// </summary>
        public int DomainId { get; set; }
        /// <summary>
        /// 域标识
        /// </summary>
        public string DomainCode { get; set; }
    }
}
