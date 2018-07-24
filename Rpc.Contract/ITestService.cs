using Hsf.NetCore.Rpc.Common.Attributes;
using Hsf.NetCore.Rpc.Common.Base;
using Hsf.NetCore.Rpc.Common.Models;
using Rpc.Contract.Models;
using System;
using System.Collections.Generic;

namespace Rpc.Contract
{
    [HsfContract]
    public interface ITestService : IBaseHsfRpc
    {

        RpcResponse<string> GetData(string value);

        RpcResponse<List<TestModelDto>> GetDataList(int pageIndex, int pageSize);


        RpcResponse<bool> UpdateData(TestModelDto model);

        RpcResponse<bool> DeleteData(int id);

    }
}
