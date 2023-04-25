using StorageServices.Entities;
using StorageServices.Entities.Rebrickable;

using System.Collections.Generic;

using Set = StorageServices.Entities.Rebrickable.Set;

namespace StorageServices.Services
{
    public interface IReferenceRepository
    {
        Set GetSet(string setNumber);
        Part GetPart(string partNumber);
        SetInstance GetMySet(string setNumber);
        IEnumerable<SetInstance> GetMySets();
        Theme GetTheme(int themeId);
    }
}
