using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IBll;
using DotNet.SSO.Model;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.Bll.Manager
{
    public class ManagerService : IManagerService
    {
        #region Manager帐号登录
        /// <summary>
        /// Manager帐号登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public JsonModel<string> ManagerLogin(ManagerLoginModel loginModel)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "登录失败",
                SuccessMsg = "登录成功"
            };
            //实体验证
            var validate = DotNet.Utils.DataValidate.ValidateHelper<ManagerLoginModel>.ValidateModel(loginModel);
            if (!validate.Pass)
            {
                jsonModel.ErrMsg = validate.ResultList.FirstOrDefault().ErrorMessage;
                return jsonModel;
            }
            IManagerDal managerDal = new ManagerDal();
            //查询数据库中是否存在该实体
            var manager = managerDal.GetEntity(new ManagerSingleParam() { LoginName = loginModel.LoginName });
            if (manager == null)
            {
                jsonModel.ErrMsg = "帐号不存在";
                return jsonModel;
            }
            //登录帐号加密后
            string encryptPwd = DotNet.Utils.Encrypt.EncryptHelper.AESEncryString(loginModel.LoginPwd, manager.EncryptKey);
            if (!manager.LoginPwd.Trim().Equals(encryptPwd.Trim()))
            {
                jsonModel.ErrMsg = "密码不正确";
                return jsonModel;
            }
            //验证 验证码
            string imageCodeKey = BllUtility.ManagerHandler.GetImageCodeCookie();
            if (!BllUtility.VerifyCodeHandler.VerifyCode(imageCodeKey, loginModel.ImageCode))
            {
                jsonModel.ErrMsg = "验证码不正确";
                return jsonModel;
            }
            //获取token,在获取的时候已经进行缓存
            string token = BllUtility.ManagerHandler.AddLoginCache(manager);
            //写入到cookie中
            BllUtility.ManagerHandler.WriteLoginCookie(token);
            jsonModel.Success = true;
            jsonModel.Data = token;
            return jsonModel;
        }
        #endregion
    }
}
