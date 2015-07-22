using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.ApiModel;

namespace DotNet.SSO.IBll
{
    /// <summary>
    /// 说明:帐号服务接口
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:IAccountService.cs
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 帐号分页查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<Account> GetPagingAccount(AccountParam parameter);
        /// <summary>
        /// 根据唯一条件查询帐号
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Account GetAccount(AccountSingleParam parameter);
        /// <summary>
        /// 添加单点登录帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<Account> AddAccount(AccountAddModel model);
        /// <summary>
        /// 删除单点登录帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonModel<string> DeleteAccount(AccountDeleteModel model);
    }
}
