using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.IDal
{
    /// <summary>
    /// 说明:单点登录池数据接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:ISSOPoolDal.cs
    /// </summary>
    public interface ISSOPoolDal
    {
        /// <summary>
        /// 获取所有的点单点登录池
        /// </summary>
        /// <returns></returns>
        List<SSOPool> GetEntitiyList();
        /// <summary>
        /// 根据池Id查询池实体
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        SSOPool GetEntity(int poolId);
        /// <summary>
        /// 添加池的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        SSOPool AddEntity(SSOPool entity);
        /// <summary>
        /// 修改池子数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        SSOPool UpdateEntity(SSOPool entity);
        /// <summary>
        /// 删除单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        int DeleteEntity(int poolId);
        /// <summary>
        /// 单点登录池分页
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<SSOPool> GetPaging(SSOPoolParam parameter);

    }
}
