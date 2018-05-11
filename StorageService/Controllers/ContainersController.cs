using Microsoft.AspNetCore.Mvc;
using StorageServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageServices.Controllers
{
    public class ContainersController
    {
        private IStorageRepository _storageRepository;

        public ContainersController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpGet("api/storageAreas/{storageAreaId}/containers")]
        public IActionResult GetContainersForStorageArea(int storageAreaId)
        {
            var containers = _storageRepository.GetContainersForStorageArea(storageAreaId);

            return new JsonResult(containers);
        }
    }
}
