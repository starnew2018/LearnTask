using AutoMapper;
using Rpc.Contract.Models;
using Rpc.IProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpcService.Biz
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            RegisterMap<TestModelDto, DaoTestModel>();///注册Dto与Dao的映射关系
        }



        private void RegisterMap<T, S>()
        {
            CreateMap<T, S>();
            CreateMap<S, T>();
        }
    }
}
