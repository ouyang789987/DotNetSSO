using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DotNet.SSO.BllUtility
{
    public class VerifyCodeHandler
    {
        #region 获取验证码的key值,会将验证码的具体内容添加到缓存中
        /// <summary>
        /// 获取验证码的key值
        /// </summary>
        /// <param name="codeType">验证码的类型</param>
        /// <param name="length">长度</param>
        /// <param name="expired">过期时间</param>
        /// <returns></returns>
        public static string GetVerifyCodeKey(int codeType, int length, DateTime expired)
        {
            string guid = DotNet.Utils.Algorithm.GuidHelper.GetGuid();
            string code = string.Empty;
            //根据类型生成验证码内容
            switch (codeType)
            {
                case (int)DotNet.Utils.Rand.RandomCodeType.OnlyNumber:
                default:
                    code = DotNet.Utils.Rand.RandomOperate.GetInstance().GetOnlyNumber(length);
                    break;
                case (int)DotNet.Utils.Rand.RandomCodeType.OnlyLowLetters:
                    code = DotNet.Utils.Rand.RandomOperate.GetInstance().GetOnlyLowLetters(length);
                    break;
                case (int)DotNet.Utils.Rand.RandomCodeType.NumberLow:
                    code = DotNet.Utils.Rand.RandomOperate.GetInstance().GetNumberLow(length);
                    break;
                case (int)DotNet.Utils.Rand.RandomCodeType.AllFixd:
                    code = DotNet.Utils.Rand.RandomOperate.GetInstance().GetAllFixd(length);
                    break;
            }
            //如果过期时间太短，那么就让过期时间变为3分钟以后
            if (expired <= DateTime.Now.AddSeconds(180))
            {
                expired = DateTime.Now.AddSeconds(180);
            }
            //写入缓存
            DotNet.Utils.Cache.CacheHelper.WriteCache(guid, code, expired);
            return guid;
        }
        #endregion

        #region  获取key值对应验证码的图片二进制
        public static byte[] GetVerifyCodeImage(string  key)
        {

            byte[] bytes;
            object obj = DotNet.Utils.Cache.CacheHelper.Get(key);
            if (obj != null)
            {
                string cacheCode = obj.ToString();
                if (!string.IsNullOrEmpty(cacheCode))
                {
                    Image img = DotNet.Utils.Img.VerificationImage.GetInstance().CreateCode(cacheCode);
                    bytes = DotNet.Utils.Img.ImageHelper.ImageToByte(img);
                    return bytes;
                }
            }
            bytes = new byte[0];
            return bytes;
        }
        #endregion

        #region 对验证码进行验证
        public static bool VerifyCode(string key, string code)
        {
            object obj = DotNet.Utils.Cache.CacheHelper.Get(key);
            if (obj != null)
            {
                string cacheCode = obj.ToString();
                if (!string.IsNullOrEmpty(cacheCode) && !string.IsNullOrEmpty(code))
                {
                    if (cacheCode.Equals(code))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
