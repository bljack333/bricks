using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageServices.Entities;
using StorageServices.Entities.Rebrickable;
using Set = StorageServices.Entities.Rebrickable.Set;

namespace StorageServices.Services
{
    public interface IReferenceRepository
    {
        Set GetSet(string setNumber);
        Part GetPart(string partNumber);

        MySet GetMySet(string setNumber);
    }
}
