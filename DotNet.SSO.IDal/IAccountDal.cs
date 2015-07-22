using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.DbUtils;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.IDal
{
   /// <summary>
    /// 说明:帐号数据接口
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IAccountDal.cs
    /// </summary>
    public interface IAccountDal
    {
        /// <summary>
        /// 添加帐号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Account AddEntity(Account entity);
        /// <summary>
        /// 删除帐户
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        int DeleteEntity(int accountId);
        /// <summary>
        /// 修改帐户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Account UpdateEntity(Account entity);
        /// <summary>
        /// 帐号分页查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PagingModel<Account> GetPaging(AccountParam parameter);
        /// <summary>
        /// 查询唯一的帐号
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Account GetEntity(AccountSingleParam parameter);
        /// <summary>
        /// 根据唯一帐号查询帐号是否存在
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Account CheckExist(AccountSingleParam parameter);
    }
}
