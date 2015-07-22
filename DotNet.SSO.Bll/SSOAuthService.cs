using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IBll;
using DotNet.SSO.Model;
using DotNet.SSO.Model.ViewModel;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.Bll
{
    /// <summary>
    /// 说明:单点登录认证相关逻辑
    /// 作者:Tieria
    /// 时间:2015.07.21
    /// 文件名:SSOAuthService.cs
    /// </summary>
    public class SSOAuthService : ISSOAuthService
    {
        #region 单点登录
        /// <summary>
        /// 单点登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel<string> Login(SSOLoginModel model)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false
            };
            try
            {
                //实体中的验证
                var validate = DotNet.Utils.DataValidate.ValidateHelper<SSOLoginModel>.ValidateModel(model);
                if (!validate.Pass)
                {
                    jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                    return jsonModel;
                }
                //过滤
                model.LoginStr = DotNet.Utils.Untility.StringHelper.FilterHtml(model.LoginStr);
                model.LoginPassword = DotNet.Utils.Untility.StringHelper.FilterHtml(model.LoginPassword);
                //判断帐号的类型
                int loginType = BllUtility.SSOLoginHandler.GetLoginType(model.LoginStr);
                if (!Enum.IsDefined(typeof(LoginTypeEnum), loginType))
                {
                    jsonModel.ErrMsg = "您输入的帐号格式不正确";
                    return jsonModel;
                }
                //查询帐号
                var param = BllUtility.SSOLoginHandler.GetLoginTypeParam(loginType, model.LoginStr);
                IAccountDal accountDal = new AccountDal();
                var account = accountDal.GetEntity(param);
                if (account == null)
                {
                    jsonModel.ErrMsg = "帐号不存在";
                    return jsonModel;
                }
                //密码校验
                string enLoginPassword = BllUtility.AccountHandler.EncryptPassword(account.OpenId, model.LoginPassword, account.EncryptKey);
                if (!enLoginPassword.Trim().Equals(account.Password.Trim()))
                {
                    //添加监控
                    DotNet.Utils.Monitor.MonitorHelper.AddMonitor(account.OpenId, 5, 300, 60);
                    jsonModel.ErrMsg = "密码不正确";
                    return jsonModel;
                }
                //判断监控是否密码输入错误次数达到了5次,进行验证码认证
                if (DotNet.Utils.Monitor.MonitorHelper.IsMonitorMax(account.OpenId))
                {
                    if (string.IsNullOrEmpty(model.VerifyKey) || string.IsNullOrEmpty(model.Code))
                    {
                        jsonModel.ErrMsg = "请输入验证码";
                        return jsonModel;
                    }
                    if (!BllUtility.VerifyCodeHandler.VerifyCode(model.VerifyKey, model.Code))
                    {
                        jsonModel.ErrMsg = "验证码不正确";
                        return jsonModel;
                    }
                }
                jsonModel.Success = true;
                //登录成功,移除监控
                DotNet.Utils.Monitor.MonitorHelper.RemoveMonitor(account.OpenId);

            }
            catch
            {
                jsonModel.ErrMsg = "登录失败";
            }
            return jsonModel;
        }
        #endregion



    }
}
