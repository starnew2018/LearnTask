using Rpc.IProvider.Models;
using System;
using System.Collections.Generic;

namespace Rpc.IProvider
{
    public interface ITestProvider : IDisposable
    {
        DaoTestModel GetData(int id);

        DaoTestModel AddData(DaoTestModel model);

        bool UpdateData(DaoTestModel model);

        bool DeleteData(int id);

        List<DaoTestModel> GetList(int page, int size);
    }
}
