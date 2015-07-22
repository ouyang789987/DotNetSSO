using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IDal;
using DotNet.SSO.Model;
using DotNet.SSO.DbUtils;
using Dapper;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.Model.Parameter;
namespace DotNet.SSO.Dal
{
   /// <summary>
    /// 说明:帐号数据Dal
    /// 作者:Tieria
    /// 时间:2015.06.16
    /// 文件名:AccountDal.cs
    /// </summary>
    public class AccountDal : IAccountDal
    {
        #region 添加帐户
        /// <summary>
        /// 添加帐户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Account AddEntity(Account entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    AccountSingleParam singleParam = new AccountSingleParam()
                    {
                        OpenId=entity.OpenId,
                        LoginName=entity.LoginName
                    };
                    string sql = @"Insert into B_Account values(@OpenId,@LoginName,@EncryptKey,@Password,@SafePassword,@SafeBinding,@Mobile,@MobileBinding,@Email,@EmailBinding,@DelFlag,@ReMark,@CreateDate,@SubmitDomainId);select @@identity;";
                    var id = connection.ExecuteScalar<int>(sql, new { OpenId = entity.OpenId, LoginName = entity.LoginName, EncryptKey = entity.EncryptKey, Password = entity.Password, SafePassword = entity.SafePassword, SafeBinding = entity.SafeBinding, Mobile = entity.Mobile, MobileBinding = entity.MobileBinding, Email = entity.Email, EmailBinding = entity.EmailBinding, DelFlag = entity.DelFlag, ReMark = entity.ReMark, CreateDate = entity.CreateDate, SubmitDomainId = entity.SubmitDomainId });
                    var account = connection.Query<Account>(@"Select * from B_Account where AccountId=@AccountId", new { AccountId = id }).FirstOrDefault();
                    return account;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 删除帐户
        /// <summary>
        /// 删除帐户
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public int DeleteEntity(int accountId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var r = connection.Execute("Delete from B_Account where AccountId=@AccountId", new { AccountId = accountId });
                return r;
            }
        }
        #endregion

        #region 修改帐户
        /// <summary>
        /// 修改帐户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Account UpdateEntity(Account entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    var dbAccount = connection.Query<Account>(@"Select * from B_Account where AccountId=@AccountId", new { AccountId = @entity.AccountId }).FirstOrDefault();
                    if (dbAccount == null)
                    {
                        return null;
                    }

                    string sql = @"Update B_Account set EncryptKey=@EncryptKey, [Password]=@Password,SafePassword=@SafePassword,SafeBinding=@SafeBinding,Mobile=@Mobile,MobileBinding=@MobileBinding,Email=@Email,EmailBinding=@EmailBinding,DelFlag=@DelFlag,ReMark=@ReMark where AccountId=@AccountId";
                    var r = connection.Execute(sql, new { EncryptKey = entity.EncryptKey, Password = entity.Password,SafePassword=entity.SafePassword,SafeBinding=entity.SafeBinding,Mobile=entity.Mobile,MobileBinding=entity.MobileBinding,Email=entity.Email,EmailBinding=entity.EmailBinding, DelFlag = entity.DelFlag, ReMark = entity.ReMark, AccountId = entity.AccountId });
                    if (r > 0)
                    {
                        var model = connection.Query<Account>(@"Select * from B_Account where AccountId=@AccountId", new { AccountId = entity.AccountId }).FirstOrDefault();
                        return model;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 帐号分页查询
        /// <summary>
        /// 帐号分页查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<Account> GetPaging(AccountParam parameter)
        {
            PagingModel<Account> pagingModel = new PagingModel<Account>()
            {
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                //跳过的页数
                int skip = (parameter.PageIndex - 1) * parameter.PageSize;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@" and DelFlag=@DelFlag ");
                #region where条件和参数
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //按照帐号的OpenId进行搜索
                if (!string.IsNullOrEmpty(parameter.OpenId))
                {
                    sb.AppendFormat(" and OpenId=@OpenId");
                    param.Add("@OpenId", parameter.OpenId);
                }
                //按照帐号Id进行搜索
                if (parameter.AccountId > 0)
                {
                    sb.AppendFormat(" and AccountId=@AccountId");
                    param.Add("@AccountId", parameter.AccountId);
                }
                //按照提交域的Id进行查询
                if (parameter.SubmitDomainId > 0)
                {
                    sb.AppendFormat(" and SubmitDomainId=@SubmitDomainId");
                    param.Add("@SubmitDomainId", parameter.SubmitDomainId);
                }
                //按照登录名查询
                if (!string.IsNullOrEmpty(parameter.LoginName))
                {
                    sb.AppendFormat(" and LoginName=@LoginName");
                    param.Add("@LoginName", parameter.LoginName);
                }
                //按照绑定手机号码查询
                if (!string.IsNullOrEmpty(parameter.Mobile))
                {
                    sb.AppendFormat(" and Mobile=@Mobile");
                    sb.AppendFormat(" and MobileBinding=@MobileBinding");
                    param.Add("@Mobile", parameter.Mobile);
                    param.Add("@MobileBinding",(int)BindingEnum.Binded);
                }
                //按照绑定Email查询
                if (!string.IsNullOrEmpty(parameter.Email))
                {
                    sb.AppendFormat(" and Email=@Email");
                    sb.AppendFormat(" and EmailBinding=@EmailBinding");
                    param.Add("@Email", parameter.Email);
                    param.Add("@EmailBinding", (int)BindingEnum.Binded);
                }

                #endregion
                string countSql = string.Format("Select count(*) from B_Account where 1=1 {0}", sb.ToString());
                var totalCount = connection.ExecuteScalar<int>(countSql, param);
                pagingModel.TotalRecord = totalCount;

                string sql = string.Format("Select top {0} * from  (Select  row_number() over (order by AccountId) AS RowNumber,* from B_Account ) as tb  where RowNumber>{1} {2}", parameter.PageSize, skip, sb.ToString());
                var query = connection.Query<Account>(sql, param);
                pagingModel.Data = query.ToList();
            }
            return pagingModel;
        }
        #endregion

        #region 查询唯一的帐号实体
        /// <summary>
        /// 查询唯一的帐号实体
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Account GetEntity(AccountSingleParam parameter)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                StringBuilder sb = new StringBuilder();
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //根据Id查询
                if (parameter.AccountId > 0)
                {
                    sb.AppendFormat(" and AccountId=@AccountId ");
                    param.Add("@AccountId", parameter.AccountId);
                }
                //根据OpenId查询
                if (!string.IsNullOrEmpty(parameter.OpenId))
                {
                    sb.AppendFormat(" and OpenId=@OpenId ");
                    param.Add("@OpenId", parameter.OpenId);
                }
                //登录帐号进行查询
                if (!string.IsNullOrEmpty(parameter.LoginName))
                {
                    sb.AppendFormat(" and LoginName=@LoginName ");
                    param.Add("@LoginName", parameter.LoginName);
                }
                //手机号码查询
                if (!string.IsNullOrEmpty(parameter.Mobile))
                {
                    sb.AppendFormat(" and Mobile=@Mobile and MobileBinding=@MobileBinding");
                    param.Add("@Mobile", parameter.Mobile);
                    param.Add("@MobileBinding", (int)BindingEnum.Binded);
                }
                //Email邮箱查询
                if (!string.IsNullOrEmpty(parameter.Email))
                {
                    sb.AppendFormat(" and Email=@Email and EmailBinding=@EmailBinding");
                    param.Add("@Email", parameter.Email);
                    param.Add("@EmailBinding", (int)BindingEnum.Binded);
                }

                string sql = string.Format(@"Select * from B_Account  where DelFlag=@DelFlag {0}", sb.ToString());
                var query = connection.Query<Account>(sql, param);
                return query.FirstOrDefault();
            }
        }
        #endregion

        #region 根据唯一参数,判断是否存在帐号
        /// <summary>
        /// 根据唯一参数,判断是否存在帐号
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Account CheckExist(AccountSingleParam parameter)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" 1>2 ");
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //OpenId
                if (!string.IsNullOrEmpty(parameter.OpenId))
                {
                    sb.AppendFormat(" or OpenId=@OpenId");
                    param.Add("@OpenId", parameter.OpenId);
                }
                //登录名检测
                if (!string.IsNullOrEmpty(parameter.LoginName))
                {
                    sb.AppendFormat(" or LoginName=@LoginName");
                    param.Add("@LoginName", parameter.LoginName);
                }
                //手机号码检测
                if (!string.IsNullOrEmpty(parameter.Mobile))
                {
                    sb.AppendFormat(" or (Mobile=@Mobile and MobileBinding=@MobileBinding)");
                    param.Add("@Mobile", parameter.Mobile);
                    param.Add("@MobileBinding", (int)BindingEnum.Binded);
                }
                //Email邮箱
                if (!string.IsNullOrEmpty(parameter.Email))
                {
                    sb.AppendFormat(" or (Email=@Email and EmailBinding=@EmailBinding)");
                    param.Add("@Email", parameter.Email);
                    param.Add("@EmailBinding", (int)BindingEnum.Binded);
                }
                string sql = string.Format(@"Select * from B_Account where DelFlag=@DelFlag and ( {0} )", sb.ToString());
                var query = connection.Query<Account>(sql, param);
                return query.FirstOrDefault();
            }
        } 
        #endregion
    }
}
