using StorageServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Services
{
    public interface IStorageRepository
    {
        IEnumerable<StorageArea> GetStorageAreas();
        IEnumerable<Container> GetContainersForStorageArea(int storageAreaId);
        IEnumerable<ContainerDivisionSlot> GetSlotsForContainerAndStorageArea(int storageAreaId, int containerId);

        StorageArea AddStorageArea(StorageArea newStorageArea);
    }
}
