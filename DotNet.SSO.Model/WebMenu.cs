using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.SSO.Model
{
    /// <summary>
    /// 说明:菜单
    /// 作者:Tieria
    /// 时间:2015.07.17
    /// 文件名:WebMenu.cs
    /// </summary>
    public class WebMenu
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 上一级菜单的Id
        /// </summary>
        public int ParentMenuId { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<WebMenu> SubMenus { get; set; }
    }
}
