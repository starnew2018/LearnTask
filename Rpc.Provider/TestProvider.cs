using Rpc.IProvider;
using Rpc.IProvider.Models;
using Rpc.Provider.Base;
using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;

namespace Rpc.Provider
{
    public class TestProvider : BaseService, ITestProvider
    {
        public DaoTestModel AddData(DaoTestModel model)
        {
            model.Id = (int)db.Insert<DaoTestModel>(model, true);
            return model;
        }

        public bool DeleteData(int id)
        {
            return db.Delete<DaoTestModel>(t => t.Id == id) > 0;
        }

        public void Dispose()
        {
            base.Dispose();
        }

        public DaoTestModel GetData(int id)
        {
            var find = db.Single<DaoTestModel>(t => t.Id == id);
            return find;
        }

        public List<DaoTestModel> GetList(int page, int size)
        {
            var query = db.From<DaoTestModel>().OrderByDescending(t => t.Id);
            var data = db.Select<DaoTestModel>(query.Limit((page - 1) * size, size));
            //var count = (int)db.Count(query);
            return data;
        }

        public bool UpdateData(DaoTestModel model)
        {
            return db.Update<DaoTestModel>(new { Name = model.Name, Psw = model.Psw }, t => t.Id == model.Id) > 0;
        }
    }
}
