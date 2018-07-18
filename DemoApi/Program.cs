using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rpc.Contract;

namespace DemoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestRpc();


            CreateWebHostBuilder(args).Build().Run();
        }
        private static void TestRpc()
        {
            var client = Hsf.NetCore.Rpc.Client.HsfRpcFactory.CreateInstance<ITestService>("http://127.0.0.1:6709/xkd.hsfrpc");
            var rs = client.GetData("test");
            var sss = 222;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:6707")
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
