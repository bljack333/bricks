using Microsoft.AspNetCore.Mvc;
using StorageServices.Entities;
using StorageServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Controllers
{
    [Route("/api/storageLocations")]
    public class StorageLocationsController : Controller
    {
        private IStorageRepository _storageRepository;

        public StorageLocationsController(IStorageRepository storageRepostiory)
        {
            _storageRepository = storageRepostiory;
        }

        [HttpGet()]
        public IActionResult GetStorageLocations()
        {
            var LocationsFromRepo = _storageRepository.GetStorageLocations();
            return new JsonResult(LocationsFromRepo);
        }

        [HttpGet("{id}")]
        public IActionResult GetStorageLocation(int id)
        {
            var LocationFromRepo = _storageRepository.GetStorageLocations().FirstOrDefault(s => s.Id == id);

            return new JsonResult(LocationFromRepo);
        }

        [HttpPost()]
        public IActionResult CreateStorageLocation([FromBody]StorageLocation newStorageLocation)
        {
            _storageRepository.AddStorageLocation(newStorageLocation);

            return GetStorageLocations();
        }

        [HttpPost()]
        [Route("{id}")]
        public IActionResult UpdateStorageLocation([FromBody]StorageLocation storageLocation)
        {
            _storageRepository.SaveStorageLocation(storageLocation);

            return GetStorageLocations();
        }

        [HttpDelete()]
        [Route("{id}")]
        public IActionResult RemoveStorageLocation(int id)
        {
            _storageRepository.RemoveStorageLocation(id);

            return new JsonResult("");
        }
    }
}
