using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IBll;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.Model.ApiModel;

namespace DotNet.SSO.Bll
{
    /// <summary>
    /// 说明:帐号服务
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:AccountService.cs
    /// </summary>
    public class AccountService : IAccountService
    {

        #region 帐号分页查询
        /// <summary>
        /// 帐号分页查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<Account> GetPagingAccount(AccountParam parameter)
        {
            PagingModel<Account> pagingModel = new PagingModel<Account>();
            IAccountDal accountDal = new AccountDal();
            //过滤
            parameter.OpenId = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.OpenId);
            parameter.LoginName = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.LoginName);
            parameter.Mobile = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.Mobile);
            parameter.Email = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.Email);

            pagingModel = accountDal.GetPaging(parameter);
            return pagingModel;
        }
        #endregion

        #region 根据唯一条件查询帐号
        /// <summary>
        /// 根据唯一条件查询帐号
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Account GetAccount(AccountSingleParam parameter)
        {
            //过滤
            parameter.OpenId = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.OpenId);
            parameter.LoginName = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.LoginName);
            parameter.Mobile = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.Mobile);
            parameter.Email = DotNet.Utils.Untility.StringHelper.FilterHtml(parameter.Email);

            IAccountDal accountDal = new AccountDal();
            var account = accountDal.GetEntity(parameter);
            return account;
        }
        #endregion

        #region 添加单点登录的帐号
        /// <summary>
        /// 添加单点登录的帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<Account> AddAccount(AccountAddModel model)
        {
            JsonModel<Account> jsonModel = new JsonModel<Account>()
            {
                Success = false,
                ErrMsg = "添加失败",
                SuccessMsg = "添加成功"
            };
            try
            {
                //对实体进行验证
                var validate = DotNet.Utils.DataValidate.ValidateHelper<AccountAddModel>.ValidateModel(model);
                if (!validate.Pass)
                {
                    jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                    return jsonModel;
                }
                //过滤
                model.LoginName = DotNet.Utils.Untility.StringHelper.FilterHtml(model.LoginName);
                model.Mobile = DotNet.Utils.Untility.StringHelper.FilterHtml(model.Mobile);
                model.LoginName = DotNet.Utils.Untility.StringHelper.FilterHtml(model.LoginName);

                #region 验证
                if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { LoginName = model.LoginName }))
                {
                    jsonModel.ErrMsg = "用户名已经存在";
                    return jsonModel;
                };
                //验证Mobile
                int mobileBinding = (int)BindingEnum.NotBinded;
                if (!string.IsNullOrEmpty(model.Mobile))
                {
                    if (!DotNet.Utils.Untility.RegexValidate.IsMobileNumber(model.Mobile))
                    {
                        jsonModel.ErrMsg = "手机号码格式不正确";
                        return jsonModel;
                    }
                    mobileBinding=(int)BindingEnum.Binded;
                    if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { Mobile = model.Mobile }))
                    {
                        jsonModel.ErrMsg = "手机号码已经存在";
                        return jsonModel;
                    };
                }
                //验证Email
                 int emailBinding = (int)BindingEnum.NotBinded;
                if (!string.IsNullOrEmpty(model.Email))
                {
                    if (!DotNet.Utils.Untility.RegexValidate.IsEmailAddress(model.Email))
                    {
                        jsonModel.ErrMsg = "Email格式不正确";
                        return jsonModel;
                    }
                    emailBinding=(int)BindingEnum.Binded;
                    if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { Email = model.Email }))
                    {
                        jsonModel.ErrMsg = "邮箱已经存在";
                        return jsonModel;
                    };
                }
                
                //验证安全密码
                int safeBinding = (int)BindingEnum.NotBinded;
                if (!string.IsNullOrEmpty(model.SafePassword))
                {
                    if (!DotNet.Utils.Untility.RegexValidate.IsPasswordOne(model.SafePassword, 6, 25))
                    {
                        jsonModel.ErrMsg = "安全密码格式不正确";
                        return jsonModel;
                    }
                    model.SafePassword = BllUtility.AccountHandler.EncryptSafePassword(model.SafePassword);
                    safeBinding = (int)BindingEnum.Binded;
                }
            
               
                //验证提交的域是否存在
                IDomainDal domainDal = new DomainDal();
                var domain = domainDal.GetEntity(new DomainSingleParam() { DomainCode=model.SubmitDomainCode });
                if (domain == null || domain.DomainId <= 0)
                {
                    jsonModel.ErrMsg = "域不存在";
                    return jsonModel;
                }
                #endregion

                string openId = BllUtility.AccountHandler.CreateOpenId();
                string encryptKey = BllUtility.AccountHandler.CreateEncryptKey();
                string encryptPassword = BllUtility.AccountHandler.EncryptPassword(openId, model.Password, encryptKey);
                string mobile = string.IsNullOrEmpty(model.Mobile) ? "" : model.Mobile;
                string email = string.IsNullOrEmpty(model.Email) ? "" : model.Email;
                string safePassword = string.IsNullOrEmpty(model.SafePassword) ? "" : model.SafePassword;
                Account account = new Account()
                {
                    OpenId = openId,
                    LoginName = model.LoginName,
                    EncryptKey = encryptKey,
                    Password = encryptPassword,
                    Mobile = mobile,
                    MobileBinding = mobileBinding,
                    Email = email,
                    EmailBinding = emailBinding,
                    SafePassword = safePassword,
                    SafeBinding = safeBinding,
                    CreateDate = DateTime.Now,
                    DelFlag = (int)DelFlagEnum.Noraml,
                    ReMark = model.ReMark,
                    SubmitDomainId = domain.DomainId
                };
                IAccountDal accountDal = new AccountDal();
                var r = accountDal.AddEntity(account);
                if (r != null && r.AccountId > 0)
                {
                    jsonModel.Success = true;
                    jsonModel.Data = r;
                }
                else
                {
                    jsonModel.ErrMsg = "数据插入失败";
                }
            }
            catch
            {
                jsonModel.ErrMsg = "系统内部错误";
            }
       
            return jsonModel;
        }
        #endregion

        #region 删除单点登录帐号
        /// <summary>
        /// 删除单点登录帐号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<string> DeleteAccount(AccountDeleteModel model)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                SuccessMsg = "删除成功",
                ErrMsg = "删除失败"
            };
            //身份认证
            //var auth = BllUtility.DomainHandler.DomainIdentityAuth(model.DomainCode,model.DomainPassword);
            //if (!auth.Success)
            //{
            //    jsonModel.ErrMsg = auth.ErrMsg;
            //    return jsonModel;
            //}
            //过滤
            if (!string.IsNullOrEmpty(model.OpenId))
            {
                model.OpenId = DotNet.Utils.Untility.StringHelper.FilterHtml(model.OpenId);
            }
            IAccountDal accountDal = new AccountDal();
            var account = accountDal.GetEntity(new AccountSingleParam() { AccountId = model.AccountId, OpenId = model.OpenId });
            if (account == null)
            {
                jsonModel.ErrMsg = "帐号不存在";
                return jsonModel;
            }
            account.DelFlag = (int)DelFlagEnum.LogicalDelete;

            var r = accountDal.UpdateEntity(account);
            if (r != null)
            {
                jsonModel.Success = true;
            }
            return jsonModel;
        }
        #endregion


       

    }
}
