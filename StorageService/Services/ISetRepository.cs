using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageServices.Entities;

namespace StorageServices.Services
{
    public interface ISetRepository
    {
        Set GetSet(int Id);
    }
}
