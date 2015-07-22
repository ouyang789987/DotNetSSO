using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet.SSO.Model;
using DotNet.SSO.DbUtils;
using Dapper;
using DotNet.SSO.Model.Enum;
using DotNet.SSO.IDal;
using DotNet.SSO.Model.Parameter;

namespace DotNet.SSO.Dal
{
    /// <summary>
    /// 说明:单点登录池
    /// 作者:Tieria
    /// 时间:2015.07.10
    /// 文件名:SSOPoolDal.cs
    /// </summary>
    public class SSOPoolDal : ISSOPoolDal
    {
        #region 获取所有的单点登录池
        /// <summary>
        /// 获取所有的单点登录池
        /// </summary>
        /// <returns></returns>
        public List<SSOPool> GetEntitiyList()
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var query = connection.Query<SSOPool>(@"Select * from B_SSOPool where DelFlag=@DelFlag", new { DelFlag = (int)DelFlagEnum.Noraml });
                return query.ToList();
            }
        }
        #endregion

        #region 根据池Id查询池实体
        /// <summary>
        /// 根据池Id查询池实体
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public SSOPool GetEntity(int poolId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var query = connection.Query<SSOPool>(@"Select * from B_SSOPool where DelFlag=@DelFlag and PoolId=@PoolId", new { DelFlag=(int)DelFlagEnum.Noraml,PoolId=poolId });
                //var query = connection.Query<SSOPool, Domain, SSOPool>(@"Select * from B_SSOPool a inner join B_Domain b on a.PoolId=b.SSOPoolPoolId where a.PoolId=@PoolId", (pool, domain) => { if (domain.DelFlag == (int)DelFlagEnum.Noraml) { pool.Domains.Add(domain); }; return pool; }, new { PoolId = poolId }, null, true, "SSOPoolPoolId", null, null);
                return query.FirstOrDefault();
            }
        }
        #endregion

        #region 添加池的数据
        /// <summary>
        /// 添加池的数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SSOPool AddEntity(SSOPool entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    string sql = @"Insert into B_SSOPool values(@PoolName,@IsEnabled,@MaxAmount,@MainDomainId,@DelFlag,@ReMark);select @@identity;";
                    var id = connection.ExecuteScalar<int>(sql, new { PoolName = entity.PoolName, IsEnabled = entity.IsEnabled, MaxAmount = entity.MaxAmount, MainDomainId = entity.MainDomainId, DelFlag = (int)DelFlagEnum.Noraml, ReMark = entity.ReMark });
                    var pool = connection.Query<SSOPool>(@"Select * from B_SSOPool where PoolId=@PoolId", new { PoolId = id }).FirstOrDefault();
                    return pool;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 修改池子数据
        /// <summary>
        ///  修改池子数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SSOPool UpdateEntity(SSOPool entity)
        {
            try
            {
                using (var connection = ConnectionFactory.GetMasterSql())
                {
                    var dbPool = connection.Query<Domain>(@"Select * from B_SSOPool where PoolId=@PoolId", new { PoolId = @entity.PoolId }).FirstOrDefault();
                    if (dbPool == null)
                    {
                        return null;
                    }

                    string sql = @"Update B_SSOPool set PoolName=@PoolName,IsEnabled=@IsEnabled,MaxAmount=@MaxAmount,MainDomainId=@MainDomainId,DelFlag=@DelFlag,ReMark=@ReMark where PoolId=@PoolId ";
                    var r = connection.Execute(sql, new { PoolName = entity.PoolName, IsEnabled = entity.IsEnabled, MaxAmount = entity.MaxAmount, MainDomainId = entity.MainDomainId, DelFlag = entity.DelFlag, ReMark = entity.ReMark, PoolId = entity.PoolId });
                    if (r > 0)
                    {
                        var model = connection.Query<SSOPool>(@"Select * from B_SSOPool where PoolId=@PoolId", new { PoolId = @entity.PoolId }).FirstOrDefault();
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

        #region 删除单点登录池
        /// <summary>
        /// 删除单点登录池
        /// </summary>
        /// <param name="poolId"></param>
        /// <returns></returns>
        public int DeleteEntity(int poolId)
        {
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                var r = connection.Execute("Delete from B_SSOPool where PoolId=@PoolId", new { PoolId = poolId });
                return r;
            }
        }
        #endregion

        #region 单点登录池分页
        /// <summary>
        /// 单点登录池分页
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public PagingModel<SSOPool> GetPaging(SSOPoolParam parameter)
        {
            PagingModel<SSOPool> pagingModel = new PagingModel<SSOPool>()
            {
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize
            };
            using (var connection = ConnectionFactory.GetMasterSql())
            {
                //跳过的页数
                int skip = (parameter.PageIndex - 1) * parameter.PageSize;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@" and DelFlag=@DelFlag ");
                #region where条件和参数
                DynamicParameters param = new DynamicParameters();
                param.Add("@DelFlag", (int)DelFlagEnum.Noraml);
                //按照池名称进行搜索
                if (!string.IsNullOrEmpty(parameter.PoolName))
                {
                    sb.AppendFormat(" and PoolName like @PoolName");
                    param.Add("@PoolName", string.Format("%{0}%", parameter.PoolName));
                }
                //按照池Id进行搜索
                if (parameter.PoolId > 0)
                {
                    sb.AppendFormat(" and PoolId=@PoolId");
                    param.Add("@PoolId", parameter.PoolId);
                }
                //按照域的名称来搜索
                if (Enum.IsDefined(typeof(IsEnabledEnum), parameter.IsEnabled))
                {
                    sb.AppendFormat(" and IsEnabled=@IsEnabled");
                    param.Add("@IsEnabled", parameter.IsEnabled);
                }

                #endregion
                string countSql = string.Format("Select count(*) from B_SSOPool where 1=1 {0}", sb.ToString());
                var totalCount = connection.ExecuteScalar<int>(countSql, param);
                pagingModel.TotalRecord = totalCount;

                string sql = string.Format("Select top {0} * from  (Select  row_number() over (order by PoolId) AS RowNumber,* from B_SSOPool ) as tb  where RowNumber>{1} {2}", parameter.PageSize, skip, sb.ToString());
                var query = connection.Query<SSOPool>(sql, param);
                pagingModel.Data = query.ToList();
            }
            return pagingModel;
        }
        #endregion


    }
}
