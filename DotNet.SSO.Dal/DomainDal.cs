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
    /// 说明:域的数据实现
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:IDomainDal.cs
    /// </summary>
    public class DomainDal : IDomainDal
    {

        #region 根据主键Id获取域
        /// <summary>
        /// 根据主键Id获取域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public Domain GetEntity(int domainId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var query = connection.Query<Domain, SSOPool, Domain>(@"Select * from B_Domain a inner join B_SSOPool b on a.SSOPoolPoolId=b.PoolId where a.DomainId=@DomainId and a.DelFlag=@DelFlag", (d, p) => { d.Pool =p; return d; }, new { DomainId = domainId, DelFlag = (int)DelFlagEnum.Noraml }, null, true, "PoolId", null, null);
                //var domain = connection.Query<Domain>(@"Select * from B_Domain where DomainId=@DomainId", new { DomainId = domainId }).FirstOrDefault();
                return query.FirstOrDefault();
            }
        }
        #endregion

        #region 根据唯一条件查询域
       /// <summary>
        /// 根据唯一条件查询域
       /// </summary>
       /// <param name="parameter"></param>
       /// <returns></returns>
        public Domain GetEntity(DomainSingleParam parameter)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                StringBuilder sb = new StringBuilder();
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //根据Id查询
                if (parameter.DomainId > 0)
                {
                    sb.AppendFormat(" and DomainId=@DomainId");
                    param.Add("@DomainId", parameter.DomainId);
                }
                //域标识查询
                if (!string.IsNullOrEmpty(parameter.DomainCode))
                {
                    sb.AppendFormat(" and DomainCode=@DomainCode");
                    param.Add("@DomainCode", parameter.DomainCode);
                }
                string sql = string.Format(@"Select * from B_Domain  where DelFlag=@DelFlag {0}", sb.ToString());
                var query = connection.Query<Domain>(sql, param);
                return query.FirstOrDefault();
            }
        }
        #endregion

        #region 查询某个SSO池子下的所有域
        /// <summary>
        /// 查询某个SSO池子下的所有域
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public List<Domain> GetPoolDomain(int poolId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var query = connection.Query<Domain>(@"Select * from B_Domain where SSOPoolPoolId=@PoolId", new { PoolId = poolId });
                return query.ToList();
            }
        }
        #endregion

        #region 添加一个域
        /// <summary>
        /// 添加一个域
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Domain AddEntity(Domain entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    string sql = @"Insert into B_Domain (DomainName,DomainCode,DomainKey,DomainPassword,DomainUrl,DomainLevel,ParentDomainId,IsEnabled,IsSSO,SSOUrl,CookieDomain,DelFlag,ReMark,SSOPoolPoolId) values(@DomainName,@DomainCode,@DomainKey,@DomainPassword,@DomainUrl,@DomainLevel,@ParentDomainId,@IsEnabled,@IsSSO,@SSOUrl,@CookieDomain,@DelFlag,@ReMark,@SSOPoolPoolId);  select @@identity;";
                    var id = connection.ExecuteScalar<int>(sql, new { DomainName = entity.DomainName, DomainCode = entity.DomainCode, DomainUrl = entity.DomainUrl, DomainLevel = entity.DomainLevel, ParentDomainId = entity.ParentDomainId, IsEnabled = entity.IsEnabled, IsSSO = entity.IsSSO, SSOUrl = entity.SSOUrl, DomainKey = entity.DomainKey, DomainPassword = entity.DomainPassword, CookieDomain = entity.CookieDomain, DelFlag = (int)DelFlagEnum.Noraml, ReMark = entity.ReMark, SSOPoolPoolId = entity.SSOPoolPoolId });
                    var domain = connection.Query<Domain>(@"Select * from B_Domain where DomainId=@DomainId", new { DomainId = id }).FirstOrDefault();
                    return domain;
                }
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region 删除一个域
        /// <summary>
        /// 删除一个域
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public int DeleteEntity(int domainId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                int r = connection.Execute("Delete from B_Domain where DomainId=@DomainId", new { DomainId = domainId });
                return r;
            }
        }
        #endregion

        #region 修改域的数据
        /// <summary>
        /// 修改域的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Domain UpdateEntity(Domain entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    var dbDomain = connection.Query<Domain>(@"Select * from B_Domain where DomainId=@DomainId", new { DomainId = @entity.DomainId }).FirstOrDefault();
                    if (dbDomain == null)
                    {
                        return null;
                    }

                    string sql = @"Update B_Domain set DomainName=@DomainName,DomainUrl=@DomainUrl,DomainLevel=@DomainLevel,ParentDomainId=@ParentDomainId,IsEnabled=@IsEnabled,IsSSO=@IsSSO,SSOUrl=@SSOUrl,DomainKey=@DomainKey,DomainPassword=@DomainPassword,CookieDomain=@CookieDomain,DelFlag=@DelFlag,ReMark=@ReMark,SSOPoolPoolId=@SSOPoolPoolId where DomainId=@DomainId";
                    var r = connection.Execute(sql, new { DomainName = entity.DomainName, DomainUrl = entity.DomainUrl, DomainLevel = entity.DomainLevel, ParentDomainId = entity.ParentDomainId, IsEnabled = entity.IsEnabled, IsSSO = entity.IsSSO, SSOUrl = entity.SSOUrl, DomainKey = entity.DomainKey,DomainPassword=entity.DomainPassword, CookieDomain = entity.CookieDomain, DelFlag = entity.DelFlag, ReMark = entity.ReMark, SSOPoolPoolId = entity.SSOPoolPoolId, DomainId = entity.DomainId });
                    if (r > 0)
                    {
                        var model = connection.Query<Domain>(@"Select * from B_Domain where DomainId=@DomainId", new { DomainId = @entity.DomainId }).FirstOrDefault();
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

        #region 域的分页
        /// <summary>
        /// 域的分页
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<Domain> GetPaging(DomainParam parameter)
        {
            PagingModel<Domain> pagingModel = new PagingModel<Domain>()
            {
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                //跳过的页数
                int skip = (parameter.PageIndex - 1) * parameter.PageSize;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@" and DelFlag=@DelFlag");
                #region where条件和参数
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //按照域代码进行搜索
                if (!string.IsNullOrEmpty(parameter.DomainCode))
                {
                    sb.AppendFormat(" and DomainCode=@DomainCode");
                    param.Add("@DomainCode", parameter.DomainCode);
                }
                //按照域级别进行搜索
                if (parameter.DomainLevel > 0)
                {
                    sb.AppendFormat(" and DomainLevel=@DomainLevel");
                    param.Add("@DomainLevel", parameter.DomainLevel);
                }
                //根据池子Id查询出所有的域
                if (parameter.SSOPoolPoolId > 0)
                {
                    sb.AppendFormat(" and SSOPoolPoolId=@SSOPoolPoolId");
                    param.Add("@SSOPoolPoolId", parameter.SSOPoolPoolId);
                }
                //按照域的名称来搜索
                if (!string.IsNullOrEmpty(parameter.DomainName))
                {
                    sb.AppendFormat(" and DomainName like @DomainName");
                    param.Add("@DomainName", string.Format("%{0}%", parameter.DomainName));
                }
                //域网址
                if (!string.IsNullOrEmpty(parameter.DomainUrl))
                {
                    sb.AppendFormat(" and DomainUrl like @DomainUrl");
                    param.Add("@DomainUrl", string.Format("%{0}%", parameter.DomainUrl));
                }
                //是否启用
                if (Enum.IsDefined(typeof(IsEnabledEnum), parameter.IsEnabled))
                {
                    sb.AppendFormat(" and IsEnabled=@IsEnabled");
                    param.Add("@IsEnabled", parameter.IsEnabled);
                }
                //是否单点登录
                if (Enum.IsDefined(typeof(IsSSOEnum), parameter.IsSSO))
                {
                    sb.AppendFormat(" and IsSSO=@IsSSO");
                    param.Add("@IsSSO", parameter.IsSSO);
                }
                //按上一级的Id
                if (parameter.ParentDomainId > 0)
                {
                    sb.AppendFormat(" and ParentDomainId=@ParentDomainId");
                    param.Add("@ParentDomainId", parameter.ParentDomainId);
                }
                //按上一级的域代码
                //if (!string.IsNullOrEmpty(parameter.ParentDomainCode))
                //{
                //    sb.AppendFormat("and ParentDomainCode=@ParentDomainCode");
                //    param.Add("@ParentDomainCode", parameter.ParentDomainCode);
                //}
                #endregion
                string countSql = string.Format("Select count(*) from B_Domain where 1=1 {0}", sb.ToString());
                var totalCount = connection.ExecuteScalar<int>(countSql, param);
                pagingModel.TotalRecord = totalCount;

                string sql = string.Format("Select top {0} * from  (Select  row_number() over (order by DomainLevel) AS RowNumber,* from B_Domain ) as tb  where RowNumber>{1} {2}", parameter.PageSize, skip, sb.ToString());
                var query = connection.Query<Domain>(sql, param);
                pagingModel.Data = query.ToList();
            }
            return pagingModel;
        }
        #endregion

        #region 按照某个条件查询出所有的域
        /// <summary>
        /// 按照某个条件查询出所有的域
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Domain> GetList(DomainParam parameter)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@" and DelFlag=@DelFlag");
                #region where条件和参数
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //按照域代码进行搜索
                if (!string.IsNullOrEmpty(parameter.DomainCode))
                {
                    sb.AppendFormat(" and DomainCode=@DomainCode");
                    param.Add("@DomainCode", parameter.DomainCode);
                }
                //按照域级别进行搜索
                if (parameter.DomainLevel > 0)
                {
                    sb.AppendFormat(" and DomainLevel=@DomainLevel");
                    param.Add("@DomainLevel", parameter.DomainLevel);
                }
                //根据池子Id查询出所有的域
                if (parameter.SSOPoolPoolId > 0)
                {
                    sb.AppendFormat(" and SSOPoolPoolId=@SSOPoolPoolId");
                    param.Add("@SSOPoolPoolId", parameter.SSOPoolPoolId);
                }

                //按照域的名称来搜索
                if (!string.IsNullOrEmpty(parameter.DomainName))
                {
                    sb.AppendFormat(" and DomainName like @DomainName");
                    param.Add("@DomainName", string.Format("%{0}%", parameter.DomainName));
                }
                //域网址
                if (!string.IsNullOrEmpty(parameter.DomainUrl))
                {
                    sb.AppendFormat(" and DomainUrl like @DomainUrl");
                    param.Add("@DomainUrl", string.Format("%{0}%", parameter.DomainUrl));
                }
                //是否启用
                if (Enum.IsDefined(typeof(IsEnabledEnum), parameter.IsEnabled))
                {
                    sb.AppendFormat(" and IsEnabled=@IsEnabled");
                    param.Add("@IsEnabled", parameter.IsEnabled);
                }
                //是否单点登录
                if (Enum.IsDefined(typeof(IsSSOEnum), parameter.IsSSO))
                {
                    sb.AppendFormat(" and IsSSO=@IsSSO");
                    param.Add("@IsSSO", parameter.IsSSO);
                }
                //按上一级的Id
                if (parameter.ParentDomainId > 0)
                {
                    sb.AppendFormat(" and ParentDomainId=@ParentDomainId");
                    param.Add("@ParentDomainId", parameter.ParentDomainId);
                }
                #endregion
                string sql = string.Format("Select * from B_Domain where 1=1 {0}", sb.ToString());
                var query = connection.Query<Domain>(sql, param);
                return query.ToList();
            }
        }
        #endregion
    }
}
