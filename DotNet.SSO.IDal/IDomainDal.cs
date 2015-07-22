using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.IDal
{
   /// <summary>
    /// 说明:域的数据接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IDomainDal.cs
    /// </summary>
    public interface IDomainDal
    {
        /// <summary>
        /// 根据主键Id获取域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        Domain GetEntity(int domainId);
        /// <summary>
        /// 根据唯一参数查询域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Domain GetEntity(DomainSingleParam parameter);
        /// <summary>
        /// 查询某个SSO池子下的所有域
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        List<Domain> GetPoolDomain(int poolId);
        /// <summary>
        /// 添加一个域
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Domain AddEntity(Domain entity);
        /// <summary>
        /// 删除一个域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        int DeleteEntity(int domainId);
        /// <summary>
        /// 修改域的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Domain UpdateEntity(Domain entity);
        /// <summary>
        /// 域的分页
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<Domain> GetPaging(DomainParam parameter);
        /// <summary>
        /// 根据条件查询出所有的域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<Domain> GetList(DomainParam parameter);
    }
}
