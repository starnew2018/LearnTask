using Hsf.NetCore.Rpc.CommUtil;
using Hsf.NetCore.Rpc.CommUtil.IOC;
using Rpc.Contract.Models;
using Rpc.IProvider;
using Rpc.IProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpcService.Biz
{
    public static class TestDateHelper
    {
        public static void RegisterMaps()
        {
            Hsf.NetCore.Rpc.CommUtil.MapperHelper.RegisterMap(typeof(MapperProfile));///注册全局的映射关系
        }


        public static TestModelDto GetData()
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rsDao = provider.GetData(1);
                var rsDto = MapperHelper.AutoMapTo<TestModelDto>(rsDao);
                return rsDto;
            }
        }

        public static List<TestModelDto> GetList()
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rsDao = provider.GetList(1, 10);
                var rsDto = MapperHelper.AutoMapTo<List<TestModelDto>>(rsDao);
                return rsDto;
            }
        }
        public static List<TestModelDto> GetListByPage(int pageIndex, int pageSize)
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rsDao = provider.GetList(pageIndex, pageSize);
                var rsDto = MapperHelper.AutoMapTo<List<TestModelDto>>(rsDao);
                return rsDto;
            }
        }


        public static TestModelDto AddData()
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rsDao = provider.AddData(new DaoTestModel() { Name = "AccountName", Psw = "888888" });
                var rsDto = MapperHelper.AutoMapTo<TestModelDto>(rsDao);
                return rsDto;
            }
        }


        public static TestModelDto AddDataByModel(TestModelDto model)
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var daoModel = MapperHelper.AutoMapTo<DaoTestModel>(model);
                var rsDao = provider.AddData(daoModel);
                var rsDto = MapperHelper.AutoMapTo<TestModelDto>(rsDao);
                return rsDto;
            }
        }


        public static bool DeleteData()
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rsDao = provider.DeleteData(1);
                return rsDao;
            }
        }

        public static bool UpdateData()
        {
            using (var provider = ProvidersHelper.Resolve<ITestProvider>())
            {
                var rs = provider.UpdateData(new DaoTestModel() { Id = 2, Name = "UpdateAccount", Psw = "666666" });
                return rs;
            }
        }

    }

}
