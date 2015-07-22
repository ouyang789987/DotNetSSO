using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace DotNet.SSO.DbUtils
{
    /// <summary>
    /// 说明:数据连接工厂
    /// 作者:Tieria
    /// 时间:2015.06.24
    /// 文件名:ConnectionFactory.cs
    /// </summary>
    public class ConnectionFactory
    {
        #region 获取SqlServer主库
        public static IDbConnection GetMasterSql()
        {
            var connStr = ConfigurationManager.ConnectionStrings["SqlContext"];
            IDbConnection connection = DbProviderFactories.GetFactory(connStr.ProviderName).CreateConnection();
            connection.ConnectionString = connStr.ConnectionString;
            return connection;
        }
        #endregion
    }
}
