using Hsf.NetCore.Rpc.Common;
using Hsf.NetCore.Rpc.Common.Models;
using Rpc.Contract;
using Rpc.Contract.Models;
using RpcService.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpcService.Services
{
    public class TestService : ITestService
    {
        public RpcResponse<string> GetData(string value)
        {
            return RpcCommHelper.GetReponse<string>(() =>
            {
                return "success" + value;
            });
        }


        public RpcResponse<bool> DeleteData(int id)
        {
            return RpcCommHelper.GetReponse<bool>(() =>
            {
                return TestDateHelper.DeleteData();
            });
        }


        public RpcResponse<bool> UpdateData(TestModelDto model)
        {
            return RpcCommHelper.GetReponse<bool>(() =>
            {
                return TestDateHelper.UpdateData();
            });
        }

        public RpcResponse<bool> AddData(TestModelDto model)
        {
            return RpcCommHelper.GetReponse<bool>(() =>
            {
                return TestDateHelper.AddDataByModel(model).Id > 0;
            });
        }


        public RpcResponse<List<TestModelDto>> GetDataList(int pageIndex, int pageSize)
        {
            return RpcCommHelper.GetReponse<List<TestModelDto>>(() => {
                return TestDateHelper.GetListByPage(pageIndex, pageSize);
            });
        }
    }
}
