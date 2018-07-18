using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hsf.NetCore.Rpc.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RpcService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(RequestRpc);
        }

        Task RequestRpc(HttpContext context)
        {
            var Request = context.Request;
            var Response = context.Response;
            var url = Request.Path.Value;

            ///处理非RPC请求的
            if (!url.EndsWith(".hsfrpc"))
            {
                Response.ContentType = "text/html;charset=utf-8";
                var rs = FilterWebRequest(url);
                return Response.WriteAsync(rs); ;///非RPC请求，终止
            }


            ///处理RPC请求
            var protocol = Request.Headers["Rpc-Protocol"];///客户端使用的协议
            var task = new Task(() => {
                Stream inStream = null;
                try
                {
                    inStream = Request.Body;
                    Response.ContentType = "application/octet-stream";
                    Response.Headers.Add("Rpc-Protocol", "protobuf");

                    var sModel = RpcProcessHelper.Process(inStream, Request.ContentLength.Value, protocol);
                    long intLength = sModel.Length;
                    Response.ContentLength = intLength;
                    Response.Body.WriteAsync(sModel, 0, (int)intLength).Wait();
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 500;
                    Response.ContentType = "text/html;charset=utf-8";
                    var exMsg = ex.ToString();
                    var rsBytes = System.Text.Encoding.UTF8.GetBytes(exMsg);
                    Response.ContentLength = rsBytes.Length;
                    Response.Body.WriteAsync(rsBytes, 0, (int)rsBytes.Length).Wait();///500错误，直接返回异常信息
                }
                finally
                {
                    if (inStream != null)
                    {
                        inStream.Dispose();///释放流资源
                    }
                    if (Response.Body != null)
                    {
                        Response.Body.Dispose();
                    }
                }
            });

            task.Start();
            return task;
        }

        string FilterWebRequest(string url)
        {
            if (url.EndsWith("/info"))
            {
                var info = RpcInfoHelper.GetServiceInfo();
                return info; ;///非RPC请求，终止
            }
            else if (url.Contains("/DetailInfo/"))
            {
                var typeName = url.Substring(url.LastIndexOf('/') + 1);
                var info = RpcInfoHelper.GetDetailInfo(typeName);
                return info; ;///非RPC请求，终止
            }
            else if (url == "/")
            {
                var simpInfo = RpcInfoHelper.GetSimpleInfo();
                return simpInfo; ;///非RPC请求，终止
            }

            return "非正常的Rpc请求，请求路径 = " + url;
        }
    }
}
