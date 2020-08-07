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
        private const string STORAGE_LOCATION = "Data/storage-location.json";
        private const string CONTAINERS = "Data/containers.json";
        private const string SLOTS = "Data/slots.json";

        public IEnumerable<StorageLocation> GetStorageLocations()
        {
            var reader = new StreamReader(STORAGE_LOCATION);

            string storageLocationsString = reader.ReadToEnd();

            reader.Close();

            var storageLocations = JsonConvert.DeserializeObject<IEnumerable<StorageLocation>>(storageLocationsString);

            return storageLocations;
        }

        public StorageLocation GetStorageLocation(int id)
        {
            return GetStorageLocations().FirstOrDefault(sl => sl.Id == id);
        }

        public IEnumerable<Container> GetContainersForStorageLocation(int storageLocationId) {
            var reader = new StreamReader(CONTAINERS);

            string containersString = reader.ReadToEnd();

            reader.Close();

            var containers = JsonConvert.DeserializeObject<IEnumerable<Container>>(containersString);

            return containers.Where(c => c.StorageLocationId == storageLocationId);
        }

        public IEnumerable<ContainerDivisionSlot> GetSlotsForContainerAndStorageLocation(int storageLocationId, int containerId)
        {
            var reader = new StreamReader(SLOTS);

            string slotsString = reader.ReadToEnd();

            reader.Close();

            var slots = JsonConvert.DeserializeObject<IEnumerable<ContainerDivisionSlot>>(slotsString);

            return slots;
        }

        public StorageLocation AddStorageLocation(StorageLocation newStorageLocation)
        {
            var storageLocations = this.GetStorageLocations();

            newStorageLocation.Id = storageLocations.Max(sa => sa.Id) + 1;

            List<StorageLocation> storageLocationList = storageLocations.ToList();
            
            storageLocationList.Add(newStorageLocation);

            using (StreamWriter file = File.CreateText(STORAGE_LOCATION))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, storageLocationList);
                file.Close();
            }

            return newStorageLocation;
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

        public StorageLocation SaveStorageLocation(StorageLocation storageLocation)
        {
            var storageLocations = this.GetStorageLocations();

            List<StorageLocation> storageLocationList = storageLocations.ToList();

            var storageLocationToUpdate = storageLocationList.First(s => s.Id == storageLocation.Id);
            var indexOf = storageLocationList.IndexOf(storageLocationToUpdate);
            storageLocationList.RemoveAt(indexOf);

            storageLocationList.Insert(indexOf, storageLocation);
            using (StreamWriter file = File.CreateText(STORAGE_LOCATION))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, storageLocationList);
                file.Close();
            }

            return storageLocationToUpdate;
        }

        public void RemoveStorageLocation(int LocationId)
        {
            var Locations = GetStorageLocations().ToList();

            Locations.Remove(Locations.Find(a => a.Id == LocationId));

            using (StreamWriter file = File.CreateText(STORAGE_LOCATION))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Locations);
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
