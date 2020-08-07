using StorageServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Services
{
    public interface IStorageRepository
    {
        IEnumerable<StorageLocation> GetStorageLocations();
        IEnumerable<Container> GetContainersForStorageLocation(int storageLocationId);
        IEnumerable<ContainerDivisionSlot> GetSlotsForContainerAndStorageLocation(int storageLocationId, int containerId);

        StorageLocation AddStorageLocation(StorageLocation newStorageLocation);
        IEnumerable<Container> GetContainers(IEnumerable<int> containerIds);
        StorageLocation SaveStorageLocation(StorageLocation storageLocation);
        void RemoveStorageLocation(int LocationId);
        Container GetContainer(int containerId);
        Container AddContainer(Container container);
        Container SaveContainer(Container container);
    }
}
