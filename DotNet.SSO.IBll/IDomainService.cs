using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.IBll
{
   /// <summary>
    /// 说明:域的业务接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IDomainService.cs
    /// </summary>
    public interface IDomainService
    {
        /// <summary>
        /// 添加域的实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<Domain> AddDomain(DomainAddModel model);
        /// <summary>
        /// Domain域分页
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<Domain> GetPagingDomain(DomainParam parameter);
        /// <summary>
        /// 根据参数查询出所有的域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<Domain> GetListDomain(DomainParam parameter);
        /// <summary>
        /// 开启或者关闭域
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        JsonModel<string> ChangeDomainEnabled(int domainId, int isEnabled);
        /// <summary>
        /// 删除域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        JsonModel<string> DeleteDomain(int domainId);
        /// <summary>
        /// 根据Id查询出域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        Domain GetDomain(DomainSingleParam parameter);
        /// <summary>
        /// 查询某个池子下的所有域
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        List<Domain> GetPoolDomain(int poolId);
        /// <summary>
        /// 查询需要修改的域的实体
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        DomainEditModel GetEditModel(int domainId);
        /// <summary>
        /// 修改域
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<Domain> EditDomain(DomainEditModel model);
    }
}
