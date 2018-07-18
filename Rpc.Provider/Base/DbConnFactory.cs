using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Rpc.Provider.Base
{
    public static class DbConnFactory
    {
        static ServiceProvider ServiceProvider = null;

        static DbConnFactory()
        {
            ServiceCollection dbServices = new ServiceCollection();
            dbServices.AddScoped<HsfCoreDb>();///每个线程创建一个可用对象
            ServiceProvider = dbServices.BuildServiceProvider();///依赖注入配置
        }

        public static IDbConnection GetDbConnection(string connKey)
        {
            return ServiceProvider.GetService<HsfCoreDb>().GetDbConnection(connKey);///每个线程只建议一个可用对象
        }


        /// <summary>
        /// 根据本地配置连接字符串，返回数据库连接对象
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static IDbConnection CreateByLocalConfig(string localconn)
        {
            if (string.IsNullOrEmpty(localconn))
            {
                throw new Exception("未配置数据库连接字符串");
            }

            var connectionString = Hsf.NetCore.Config.SDK.CoreLocalConfigUtil.GetAppSetting(localconn);

            if (string.IsNullOrEmpty(localconn))
            {
                throw new Exception("本地xkdhsfcore.json配置文件，AppSettings未配置Key=" + localconn);
            }

            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialectProvider.Instance);
            return dbFactory.OpenDbConnection();

        }

        /// <summary>
        /// 根据配置中心连接字符串，返回连接对象
        /// </summary>
        /// <param name="centerKey"></param>
        /// <returns></returns>
        public static IDbConnection CreateByCenterConfig(string centerKey)
        {
            if (string.IsNullOrEmpty(centerKey))
            {
                throw new Exception("未配置数据库连接字符串");
            }
            var connectionString = Hsf.NetCore.Config.SDK.CoreConfigHelper.GetConnSettting(centerKey);
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialectProvider.Instance);
            return dbFactory.OpenDbConnection();
        }

    }

    public class HsfCoreDb
    {
        private IDbConnection db = null;
        public IDbConnection GetDbConnection(string connKey)
        {
            if (db == null || db.State == ConnectionState.Closed)
            {
                db = DbConnFactory.CreateByLocalConfig(connKey); //new一个新的连接对象
            }
            return db;
        }
    }

}
