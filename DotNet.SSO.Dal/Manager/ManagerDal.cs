using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.IDal;
using DotNet.SSO.Model;
using Dapper;
using DotNet.SSO.DbUtils;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.Model.Parameter;
namespace DotNet.SSO.Dal
{
    /// <summary>
    /// 说明:管理员数据操作
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:ManagerDal.cs
    /// </summary>
    public class ManagerDal : IManagerDal
    {
        #region 添加管理员实体
        /// <summary>
        /// 添加管理员实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Manager AddEntity(Manager entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    string sql = @"Insert Sys_Manager values(@LoginName,@LoginPwd,@EncryptKey,@DelFlag,@ReMark);select @@identity;";
                    var id = connection.ExecuteScalar<int>(sql, new { LoginName = entity.LoginName, LoginPwd = entity.LoginPwd, EncryptKey = entity.EncryptKey, DelFlag = (int)DelFlagEnum.Noraml, ReMark = entity.ReMark });
                    var manager = connection.Query<Manager>(@"Select * from Sys_Manager where ManagerId=@ManagerId", new { ManagerId = id }).FirstOrDefault();
                    return manager;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据参数查询唯一的管理员实体
        /// <summary>
        /// 根据参数查询唯一的管理员实体
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Manager GetEntity(ManagerSingleParam parameter)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                StringBuilder sb = new StringBuilder();
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //根据Id查询
                if (parameter.ManagerId > 0)
                {
                    sb.AppendFormat(" and ManagerId=@ManagerId");
                    param.Add("@ManagerId", parameter.ManagerId);
                }
                //查询登录名
                if (!string.IsNullOrEmpty(parameter.LoginName))
                {
                    sb.AppendFormat(" and LoginName=@LoginName");
                    param.Add("@LoginName", parameter.LoginName);
                }
                string sql = string.Format(@"Select * from Sys_Manager where DelFlag=@DelFlag {0}", sb.ToString());
                var query = connection.Query<Manager>(sql, param);
                return query.FirstOrDefault();
            }
        } 
        #endregion
    }
}
