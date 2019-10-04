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
        public IEnumerable<MyPart> GetMyParts(int page, int pageSize)
        {
            var referenceParts = _referenceRepository.GetMyParts(page, pageSize);

            var partsList = new List<MyPart>();

            var myParts = _partsRepository.GetMyParts();

            foreach (var referencePart in referenceParts.Result.Results)
            {
                var newPart = partsList.FirstOrDefault(l => l.PartNumber == referencePart.Part.PartNumber);

                if (newPart == null)
                {
                    newPart = new MyPart();

                    newPart.PartNumber = referencePart.Part.PartNumber;
                    newPart.PartCategoryId = referencePart.Part.PartCategoryId;
                    newPart.PartImageUrl = referencePart.Part.PartImageUrl;
                    newPart.PartUrl = referencePart.Part.PartUrl;
                    newPart.Name = referencePart.Part.Name;

                    partsList.Add(newPart);
                }

                if (newPart.Colors == null)
                {
                    newPart.Colors = new List<Color>();
                }

                var color = newPart.Colors.FirstOrDefault(c => c.Name == referencePart.Color.Name);

                if (color != null)
                {
                    newPart.Quantity += referencePart.Quantity;
                }
                else
                {
                    var newColor = new Color();

                    newColor.Quantity = referencePart.Quantity;
                    newColor.Name = referencePart.Color.Name;
                    newColor.IsTransparent = referencePart.Color.IsTransparent;
                    newColor.RGB = referencePart.Color.RGB;

                    newPart.Colors.ToList().Add(newColor);
                }

                newPart.Containers = _storageRepository.GetContainers(newPart.Containers.Select(c => c.Id).ToArray());
            }

            return partsList;
        }
    }
}