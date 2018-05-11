using Microsoft.AspNetCore.Mvc;
using StorageServices.Entities;
using StorageServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Controllers
{
    [Route("/api/storageAreas")]
    public class StorageAreasController : Controller
    {
        private IStorageRepository _storageRepository;

        public StorageAreasController(IStorageRepository storageRepostiory)
        {
            _storageRepository = storageRepostiory;
        }

        [HttpGet()]
        public IActionResult GetStorageAreas()
        {
            var areasFromRepo = _storageRepository.GetStorageAreas();
            return new JsonResult(areasFromRepo);
        }

        [HttpGet("{id}")]
        public IActionResult GetStorageArea(int id)
        {
            var areaFromRepo = _storageRepository.GetStorageAreas().FirstOrDefault(s => s.Id == id);

            return new JsonResult(areaFromRepo);
        }

        [HttpPost()]
        public IActionResult CreateStorageArea([FromBody]StorageArea newStorageArea)
        {
            _storageRepository.AddStorageArea(newStorageArea);

            return GetStorageAreas();
        }
    }
}
