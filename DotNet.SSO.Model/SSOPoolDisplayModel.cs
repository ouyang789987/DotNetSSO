using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
    public class SSOPoolDisplayModel
    {
        public SSOPoolDisplayModel()
        {
            Domains = new List<Domain>();
        }
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
        /// 帐号登录方式
        /// </summary>
        public int UnifiedAccount { get; set; }
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
        /// <summary>
        /// SSO池下的所有域
        /// </summary>
        public List<Domain> Domains { get; set; }
        /// <summary>
        /// 单点登录池已经添加的域
        /// </summary>
        public int CurrentDomainCount
        {
            get
            {
                if (Domains != null)
                {
                    return Domains.Count;
                }
                return 0;
            }
           
        }
    }
}
