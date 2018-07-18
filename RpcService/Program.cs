using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RpcService.Biz;

namespace RpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitRpcService();//Rpc服务初始化

            //DebugTest(); 本项目调试代码，正式发布时需要注释掉

            BuildWebHost(args).Run();///构建Http服务器
        }

        private static void InitRpcService()
        {
            ///启动RPC服务字典,对外提供服务
            Hsf.NetCore.Rpc.Service.RpcProcessHelper.StartRpcService();
            TestDateHelper.RegisterMaps();///注册AutoMaper全局的Dto\Dao映射关系,全局只注册一次
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:6709")
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
        }

        /// <summary>
        /// 调试代码的方法，
        /// </summary>
        private static void DebugTest()
        {
            //var rs = TestDateHelper.AddData();
            //var rs3 = TestDateHelper.AddData();
            //var rs1 = TestDateHelper.GetList();
            //var rs2 = TestDateHelper.UpdateData();
            //var rs4 = TestDateHelper.DeleteData();
        }

    }
}
