using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:单点登录操作
    /// 作者:Tieria
    /// 时间:2015.07.22
    /// 文件名:SSOLoginHandler.cs
    /// </summary>
    public class SSOLoginHandler
    {

        #region 根据帐号获取帐号类型,如果都不符合,返回0
        /// <summary>
        /// 根据帐号获取帐号类型,如果都不符合,返回0
        /// </summary>
        /// <param name="loginStr"></param>
        /// <returns></returns>
        public static int GetLoginType(string loginStr)
        {
            if (DotNet.Utils.Untility.RegexValidate.IsMobileNumber(loginStr))
            {
                return (int)LoginTypeEnum.LoginMobile;
            }
            else if (DotNet.Utils.Untility.RegexValidate.IsEmailAddress(loginStr))
            {
                return (int)LoginTypeEnum.LoginEmail;
            }
            else
            {
                if (DotNet.Utils.Untility.RegexValidate.IsLoginName(loginStr, 6, 25))
                {
                    return (int)LoginTypeEnum.LoginName;
                }
                else
                {
                    return 0;
                }

            }
        }
        #endregion

        #region 根据帐号的类型和帐号的输入内容,获取帐号的查询参数
        /// <summary>
        /// 根据帐号的类型和帐号的输入内容,获取帐号的查询参数
        /// </summary>
        /// <param name="loginType"></param>
        /// <param name="loginStr"></param>
        /// <returns></returns>
        public static AccountSingleParam GetLoginTypeParam(int loginType, string loginStr)
        {
            //过滤
            loginStr = DotNet.Utils.Untility.StringHelper.FilterHtml(loginStr);
            AccountSingleParam param = new AccountSingleParam()
            {
                AccountId = 0
            };
            switch (loginType)
            {
                case (int)LoginTypeEnum.LoginName:
                    param = new AccountSingleParam() { LoginName = loginStr };
                    break;
                case (int)LoginTypeEnum.LoginMobile:
                    param = new AccountSingleParam() { Mobile = loginStr };
                    break;
                case (int)LoginTypeEnum.LoginEmail:
                    param = new AccountSingleParam() { Email = loginStr };
                    break;
            }
            return param;
        } 
        #endregion

       
    }
}
