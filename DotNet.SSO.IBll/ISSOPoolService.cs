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
    /// 说明:单点登录池的业务接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:ISSOPoolService.cs
    /// </summary>
    public interface ISSOPoolService
    {
        /// <summary>
        /// 添加一个单点登录池
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<string> AddSSOPool(SSOPoolAddModel model);
        /// <summary>
        /// 根据池子Id查询池子
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        SSOPool GetSSOPool(int poolId);
        /// <summary>
        /// 带分页的单点登录池
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<SSOPool> GetPagingModel(SSOPoolParam parameter);
        /// <summary>
        /// 删除单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        JsonModel<string> DeleteSSOPool(int poolId);
        /// <summary>
        /// 改变池子的启用或者禁用状态
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        JsonModel<string> ChangeSSOPoolEnabled(int poolId, int isEnabled);
        /// <summary>
        /// 获取所有的单点登录池
        /// </summary>
        /// <returns></returns>
        List<SSOPool> GetSSOPoolList();
        /// <summary>
        /// 查询用来展示的单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        SSOPoolDisplayModel GetDisplayModel(int poolId);
        /// <summary>
        /// 查询用来修改的单点登录池实体
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        SSOPoolEditModel GetEditModel(int poolId);
        /// <summary>
        /// 修改单点登录池
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<string> EditSSOPool(SSOPoolEditModel model);
    }
}
