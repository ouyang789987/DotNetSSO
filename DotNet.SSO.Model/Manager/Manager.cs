using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
    /// <summary>
    /// 说明:管理员帐号实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:Manager.cs
    /// </summary>
    public class Manager
    {
        public int ManagerId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 加密的密钥
        /// </summary>
        public string EncryptKey { get; set; }
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
