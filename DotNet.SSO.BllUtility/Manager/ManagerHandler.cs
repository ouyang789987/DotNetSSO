using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.Utils.Cache.CacheShare;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:帐号相关操作
    /// 作者:Tieria
    /// 时间:2015.07.11
    /// 文件名:ManagerHandler.cs
    /// </summary>
    public class ManagerHandler
    {

        #region 获取验证码图片
        /// <summary>
        /// GetLoginCode
        /// </summary>
        /// <returns></returns>
        public static byte[] GetLoginCode()
        {
            string key = GetImageCodeCookie();
            byte[] bytes;
            if (!string.IsNullOrEmpty(key))
            {
                bytes = VerifyCodeHandler.GetVerifyCodeImage(key);
                if (bytes.Length > 0)
                {
                    return bytes;
                }
            }
            key = VerifyCodeHandler.GetVerifyCodeKey((int)DotNet.Utils.Rand.RandomCodeType.OnlyNumber, 4, DateTime.Now.AddSeconds(180));
            bytes = VerifyCodeHandler.GetVerifyCodeImage(key);
            WriteImageCodeCookie(key);
            return bytes;
        }
        #endregion

        #region 图片验证码cookie的读写
        /// <summary>
        ///  写入图片验证码cookie
        /// </summary>
        /// <param name="value"></param>
        public static void WriteImageCodeCookie(string value)
        {
            DotNet.Utils.Untility.CookieHelper.WriteCookie("imgcode", value);
        }
        /// <summary>
        /// 读取图片验证码的cookie
        /// </summary>
        /// <returns></returns>
        public static string GetImageCodeCookie()
        {
            string val = DotNet.Utils.Untility.CookieHelper.Get("imgcode");
            return val;
        }
        #endregion


        #region 添加Manager登录的缓存
        /// <summary>
        /// 添加Manager登录的缓存
        /// </summary>
        /// <param name="token"></param>
        /// <param name="manager"></param>
        public static string AddLoginCache(Manager manager)
        {
            string token = DotNet.Utils.Algorithm.GuidHelper.GetGuid();
            ShareContext<Manager> context = new ShareContext<Manager>()
            {
                Token = token,
                TokenPrefix = "Manager",
                GainSeconds = 7200,
                EntityKey = manager.ManagerId.ToString(),
                Entity = manager
            };
            CacheShareHandler<Manager>.AddShareCache(context);
            return token;
        }
        #endregion

        #region 帐号Cookie的读写
        public static void WriteLoginCookie(string token)
        {
            DotNet.Utils.Untility.CookieHelper.WriteCookie("token", token);
        }

        public static string GetLoginCookie()
        {
            string token = DotNet.Utils.Untility.CookieHelper.Get("token");
            return token;
        }
        #endregion
    }
}
