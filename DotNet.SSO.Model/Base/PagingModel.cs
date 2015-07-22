using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
   /// <summary>
    /// 说明:分页实体
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:PagingModel.cs
    /// </summary>
    public class PagingModel<T> where T : class,new()
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 每页的大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前的页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> Data { get; set; }
    }
}
