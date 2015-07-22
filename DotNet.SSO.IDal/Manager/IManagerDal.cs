using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.IDal
{
   /// <summary>
    /// 说明:管理员数据接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IManagerDal.cs
    /// </summary>
    public interface IManagerDal
    {
        /// <summary>
        /// 添加管理员实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Manager AddEntity(Manager entity);
        /// <summary>
        ///  根据参数查询唯一的管理员实体
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Manager GetEntity(ManagerSingleParam parameter);
    }
}
