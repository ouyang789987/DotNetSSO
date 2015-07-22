using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{
    /// <summary>
    /// 说明:帐号搜索的参数
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:AccountParam.cs
    /// </summary>
    public class AccountParam:ParameterContext
    {
        /// <summary>
        /// 帐号Id进行搜索
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// OpenId进行搜索
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 根据登录名查询
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 根据帐号手机号码查询
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 根据Email查询
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 根据帐号提交的域进行查询
        /// </summary>
        public int SubmitDomainId { get; set; }
    }
}
