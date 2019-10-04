using StorageServices.Domain;
using StorageServices.Entities.Rebrickable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.App_Start
{
    public static class StorageMapperConfiguration
    {
        public static void ConfigureMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Set, MySet>();
                cfg.CreateMap<Entities.MySet, MySet>();

            });
        }
    }
}
