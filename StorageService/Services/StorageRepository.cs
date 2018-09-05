using Newtonsoft.Json;
using StorageServices.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Services
{
    public class StorageRepository : IStorageRepository
    {
        private const string STORAGE_AREAS = "Data/storage-areas.json";
        private const string CONTAINERS = "Data/containers.json";
        private const string SLOTS = "Data/slots.json";

        public IEnumerable<StorageArea> GetStorageAreas()
        {
            var reader = new StreamReader(STORAGE_AREAS);

            string storageAreasString = reader.ReadToEnd();

            reader.Close();

            var storageAreas = JsonConvert.DeserializeObject<IEnumerable<StorageArea>>(storageAreasString);

            return storageAreas;
        }

        public StorageArea GetStorageArea(int id)
        {
            return GetStorageAreas().FirstOrDefault(sa => sa.Id == id);
        }

        public IEnumerable<Container> GetContainersForStorageArea(int storageAreaId) {
            var reader = new StreamReader(CONTAINERS);

            string containersString = reader.ReadToEnd();

            reader.Close();

            var containers = JsonConvert.DeserializeObject<IEnumerable<Container>>(containersString);

            return containers.Where(c => c.StorageAreaId == storageAreaId);
        }

        public IEnumerable<ContainerDivisionSlot> GetSlotsForContainerAndStorageArea(int storageAreaId, int containerId)
        {
            var reader = new StreamReader(SLOTS);

            string slotsString = reader.ReadToEnd();

            reader.Close();

            var slots = JsonConvert.DeserializeObject<IEnumerable<ContainerDivisionSlot>>(slotsString);

            return slots;
        }

        public StorageArea AddStorageArea(StorageArea newStorageArea)
        {
            var storageAreas = this.GetStorageAreas();

            newStorageArea.Id = storageAreas.Max(sa => sa.Id) + 1;

            List<StorageArea> storageAreaList = storageAreas.ToList();
            
            storageAreaList.Add(newStorageArea);

            using (StreamWriter file = File.CreateText(STORAGE_AREAS))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, storageAreaList);
                file.Close();
            }

            return newStorageArea;
        }

        public IEnumerable<Container> GetContainers(IEnumerable<int> containerIds)
        {
            var reader = new StreamReader(CONTAINERS);

            string containersString = reader.ReadToEnd();

            reader.Close();

            var containers = JsonConvert.DeserializeObject<IEnumerable<Container>>(containersString);

            return containers.Where(c => containerIds.ToList().Exists(i => i == c.Id));
        }
    }
}
