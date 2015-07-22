using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model.Parameter
{

    public class ParameterContext
    {

        private int pageSize;
        private int pageIndex;
        /// <summary>
        /// 每页的大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (this.pageSize <= 4)
                {
                    return 5;
                }
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
            }
        }

        /// <summary>
        ///当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (this.pageIndex < 1)
                {
                    return 1;
                }
                return this.pageIndex;
            }
            set
            {
                this.pageIndex = value;
            }
        }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPaging { get; set; }
    }
}
