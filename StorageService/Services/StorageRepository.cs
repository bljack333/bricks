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

        public IEnumerable<Container> GetContainers()
        {
            var reader = new StreamReader(CONTAINERS);

            string containersString = reader.ReadToEnd();

            reader.Close();

            var containers = JsonConvert.DeserializeObject<IEnumerable<Container>>(containersString);

            return containers;
        }

        public StorageArea SaveStorageArea(StorageArea storageArea)
        {
            var storageAreas = this.GetStorageAreas();

            List<StorageArea> storageAreaList = storageAreas.ToList();

            var storageAreaToUpdate = storageAreaList.First(s => s.Id == storageArea.Id);
            var indexOf = storageAreaList.IndexOf(storageAreaToUpdate);
            storageAreaList.RemoveAt(indexOf);

            storageAreaList.Insert(indexOf, storageArea);
            using (StreamWriter file = File.CreateText(STORAGE_AREAS))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, storageAreaList);
                file.Close();
            }

            return storageAreaToUpdate;
        }

        public void RemoveStorageArea(int areaId)
        {
            var areas = GetStorageAreas().ToList();

            areas.Remove(areas.Find(a => a.Id == areaId));

            using (StreamWriter file = File.CreateText(STORAGE_AREAS))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, areas);
                file.Close();
            }
        }

        public Container GetContainer(int containerId)
        {
            var reader = new StreamReader(CONTAINERS);

            string containersString = reader.ReadToEnd();

            reader.Close();

            var containers = JsonConvert.DeserializeObject<IEnumerable<Container>>(containersString);

            return containers.FirstOrDefault(c => c.Id == containerId);
        }

        public Container AddContainer(Container container)
        {
            var containers = this.GetContainers();

            container.Id = containers.Max(c => c.Id) + 1;

            List<Container> containerList = containers.ToList();

            containerList.Add(container);

            using (StreamWriter file = File.CreateText(CONTAINERS))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, containerList);
                file.Close();
            }

            return container;
        }

        public Container SaveContainer(Container container)
        {
            var containers = this.GetContainers();

            List<Container> containerList = containers.ToList();

            var containerToUpdate = containerList.First(c => c.Id == container.Id);

            var indexOf = containerList.IndexOf(containerToUpdate);

            containerList.RemoveAt(indexOf);

            containerList.Insert(indexOf, container);

            using (StreamWriter file = File.CreateText(CONTAINERS))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, containerList);
                file.Close();
            }

            return container;
        }
    }
}
