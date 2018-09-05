using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StorageServices.Domain;
using StorageServices.Entities.Rebrickable;
using StorageServices.Services;
using Color = StorageServices.Domain.Color;

namespace StorageServices.Controllers
{
    [Route("/api/parts")]
    public class PartsController : Controller
    {
        private readonly IPartsRepository _partsRepository;
        private readonly IReferenceRepository _referenceRepository;
        private readonly IStorageRepository _storageRepository;

        public PartsController(IPartsRepository partsRepository, IReferenceRepository referenceRepository, IStorageRepository storageRepository)
        {
            _partsRepository = partsRepository;
            _referenceRepository = referenceRepository;
            _storageRepository = storageRepository;
        }

        [HttpGet()]
        public IEnumerable<MyPart> GetMyParts()
        {
            var partsList = new List<MyPart>();

            var myParts = _partsRepository.GetMyParts();

            foreach (var myPart in myParts)
            {
                var newPart = partsList.FirstOrDefault(l => l.PartNumber == myPart.PartNumber);

                var response = _referenceRepository.GetPart(myPart.PartNumber);

                if (newPart == null)
                {
                    newPart = new MyPart();
                    var refPart = response.Results.First().Part;

                    newPart.PartNumber = refPart.PartNumber;
                    newPart.PartCategoryId = refPart.PartCategoryId;
                    newPart.PartImageUrl = refPart.PartImageUrl;
                    newPart.PartUrl = refPart.PartUrl;
                    newPart.Name = refPart.Name;
                }
            
                // map refPart to newPart
                foreach (var partInstance in response.Results)
                {
                    newPart.Quantity += partInstance.Quantity;

                    if (newPart.Colors == null)
                    {
                        newPart.Colors = new List<Color>();
                    }
                    var newColor = new Color();

                    newColor.Quantity = partInstance.Quantity;
                    newColor.Name = partInstance.Color.Name;
                    newColor.IsTransparent = partInstance.Color.IsTransparent;
                    newColor.RGB = partInstance.Color.RGB;

                    newPart.Colors.ToList().Add(newColor);
                }

                newPart.Containers = _storageRepository.GetContainers(myPart.ContainerIds);

                partsList.Add(newPart);
            }

            return partsList;
        }
    }
}