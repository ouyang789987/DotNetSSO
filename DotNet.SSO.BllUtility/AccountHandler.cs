using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;
using DotNet.SSO.Model.Enum;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:帐号的相关操作
    /// 作者:Tieria
    /// 时间:2015.07.14
    /// 文件名:AccountHandler.cs
    /// </summary>
    public class AccountHandler
    {
        #region 生成帐号的OpenId
        /// <summary>
        /// 生成帐号的OpenId
        /// </summary>
        /// <returns></returns>
        public static string CreateOpenId()
        {
            string openId = DotNet.Utils.Algorithm.GuidHelper.GetGuid();
            return openId;
        }
        #endregion

        #region 登录帐号的加密解密
       /// <summary>
       /// 登录帐号加密
       /// </summary>
       /// <param name="openId"></param>
       /// <param name="password"></param>
       /// <param name="encryptKey"></param>
       /// <returns></returns>
        public static string EncryptPassword(string openId,string password, string encryptKey)
        {
            string encryptPassword = DotNet.Utils.Encrypt.EncryptHelper.AESEncryString(password, string.Format("{0}{1}",openId,encryptKey));
            return encryptPassword;
        }
        /// <summary>
        /// 登录帐号解密
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="encryptPassword"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DecryptPassword(string openId, string encryptPassword, string encryptKey)
        {
            string pass = string.Format("{0}{1}",openId,encryptKey);
            string password = DotNet.Utils.Encrypt.EncryptHelper.AESDecryString(encryptPassword, pass);
            return password;
        }
        #endregion

        #region 安全密码的加密
       /// <summary>
       /// 安全帐号的加密
       /// </summary>
       /// <param name="safePassword"></param>
       /// <returns></returns>
        public static string EncryptSafePassword(string safePassword)
        {
            if (!string.IsNullOrEmpty(safePassword))
            {
                safePassword = DotNet.Utils.Encrypt.EncryptHelper.GetDoubleMD5(safePassword);
            }
            return safePassword;
        } 
        #endregion

        #region 生成随机加密密钥
        /// <summary>
        /// 生成随机加密密钥
        /// </summary>
        /// <returns></returns>
        public static string CreateEncryptKey()
        {
            string encryptKey = DotNet.Utils.Rand.RandomOperate.GetInstance().GetAllFixd(12);
            return encryptKey;
        }
        #endregion

        #region 检测是否唯一
        /// <summary>
        /// 检测是否唯一
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static  bool VerifyOnly(AccountSingleParam parameter)
        {
            IAccountDal accountDal = new AccountDal();
            var account = accountDal.GetEntity(parameter);
            if (account == null || account.AccountId <= 0)
            {
                return true;
            }
            return false;
        }

        public static JsonModel<string> AccountVerifyOnly(AccountSingleParam parameter)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "",
                SuccessMsg = "验证成功"
            };

            //验证登录名
            if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { LoginName = parameter.LoginName }))
            {
                jsonModel.ErrMsg = "用户名已经存在";
                return jsonModel;
            };
            if (!string.IsNullOrEmpty(parameter.Mobile))
            {
                if (!DotNet.Utils.Untility.RegexValidate.IsMobileNumber(parameter.Mobile))
                {
                    jsonModel.ErrMsg = "手机号码格式不正确";
                    return jsonModel;
                }
                if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { Mobile = parameter.Mobile }))
                {
                    jsonModel.ErrMsg = "手机号码已经存在";
                    return jsonModel;
                };
            }
            if (!string.IsNullOrEmpty(parameter.Email))
            {
                if (!DotNet.Utils.Untility.RegexValidate.IsEmailAddress(parameter.Email))
                {
                    jsonModel.ErrMsg = "Email格式不正确";
                    return jsonModel;
                }
                if (!BllUtility.AccountHandler.VerifyOnly(new AccountSingleParam() { Email = parameter.Email }))
                {
                    jsonModel.ErrMsg = "邮箱已经存在";
                    return jsonModel;
                };
            }
            jsonModel.Success = true;
            return jsonModel;
        }
        #endregion

        #region 生成随机的帐号的密码
        /// <summary>
        /// 帐号的密码
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            string password = DotNet.Utils.Rand.RandomOperate.GetInstance().GetOnlyNumber(8);
            return password;
        }
        #endregion
    }
}
