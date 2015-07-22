using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IDal;
using DotNet.SSO.Dal;
using DotNet.SSO.Model;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.BllUtility
{
    /// <summary>
    /// 说明:域相关操作
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:DomainHandler.cs
    /// </summary>
    public class DomainHandler
    {
        #region 获取域的唯一标识符
        /// <summary>
        /// 获取域的唯一标识符
        /// </summary>
        /// <returns></returns>
        public static string GetDomainCode()
        {
            string domainCode = DotNet.Utils.Algorithm.GuidHelper.GetGuid().ToLower();
            return domainCode;
        }
        #endregion

        #region 生成域的密钥
        /// <summary>
        /// 生成域的密钥
        /// </summary>
        /// <returns></returns>
        public static string GetDomainKey()
        {
            string key = DotNet.Utils.Rand.RandomOperate.GetInstance().GetAllFixd(8);
            return key;
        }
        #endregion

        #region 域的密码处理,加密和解密
        /// <summary>
        /// 加密域密码
        /// </summary>
        /// <param name="domainPassword"></param>
        /// <param name="domainCode"></param>
        /// <param name="domainKey"></param>
        /// <returns></returns>
        public static string EncryptDomainPassword(string domainPassword, string domainCode, string domainKey)
        {
            string encryptKey = string.Format("{0}{1}", domainCode, domainKey);
            string encryptPassword = DotNet.Utils.Encrypt.EncryptHelper.AESEncryString(domainPassword, encryptKey);
            return encryptPassword;
        }
        /// <summary>
        /// 解密域密码
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="domainCode"></param>
        /// <param name="domainKey"></param>
        /// <returns></returns>
        public static string DecryptDomainPassword(string decryptString, string domainCode, string domainKey)
        {
            string encryptKey = string.Format("{0}{1}", domainCode, domainKey);
            string password = DotNet.Utils.Encrypt.EncryptHelper.AESDecryString(decryptString, encryptKey);
            return password;
        }
        #endregion

        #region 根据当前域的等级和父级Id获取当前域的真实等级
        /// <summary>
        /// 根据当前域的等级和父级Id获取当前域的真实等级
        /// </summary>
        /// <param name="level"></param>
        /// <param name="parentDomainId"></param>
        public static void GetDomainLevel(ref int level, ref int parentDomainId)
        {
            IDomainDal domainDal = new DomainDal();
            //获取当前域的等级,需要先判断上一级的域是否存在
            var parentDomain = domainDal.GetEntity(parentDomainId);
            if (parentDomain != null && parentDomain.DomainId > 0)
            {
                parentDomainId = parentDomain.DomainId;
                level = parentDomain.DomainLevel + 1;
            }
            else
            {
                parentDomainId = 0;
                level = 1;
            }
        }
        #endregion

        #region 域的身份认证
        /// <summary>
        /// 域的身份认证
        /// </summary>
        /// <param name="domainCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static JsonModel<string> DomainIdentityAuth(string domainCode, string password)
        {
            JsonModel<string> jsonModel = new JsonModel<string>()
            {
                Success = false,
                ErrMsg = "域拥有者身份认证不通过",
                SuccessMsg = "域拥有者身份认证通过"
            };
            IDomainDal domainDal = new DomainDal();
            var domain = domainDal.GetEntity(new DomainSingleParam() { DomainCode = domainCode });
            if (domainCode == null)
            {
                jsonModel.ErrMsg = "域不存在";
            }
            if (string.IsNullOrEmpty(domainCode) || string.IsNullOrEmpty(password))
            {
                jsonModel.ErrMsg = "域标识不正确或者域密码不正确";
                return jsonModel;
            }

            string inputEncrypt = EncryptDomainPassword(password, domain.DomainCode, domain.DomainKey);
            if (!inputEncrypt.Trim().Equals(domain.DomainPassword.Trim()))
            {
                jsonModel.ErrMsg = "密码不正确";
            }
            jsonModel.Success = true;
            return jsonModel;
        }
        #endregion
    }
}
