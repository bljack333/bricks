using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageServices.Entities;

namespace StorageServices.Services
{
    public interface ISetRepository
    {
        MySet GetSet(int Id);
        IEnumerable<MySet> GetMySets();
        MySet GetSetBySetNumber(string setNumber);
    }
}
