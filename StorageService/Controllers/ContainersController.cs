﻿using Microsoft.AspNetCore.Mvc;
using StorageServices.Entities;
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

        [HttpGet("api/storageLocations/{storageLocationId}/containers")]
        public IActionResult GetContainersForStorageLocation(int storageLocationId)
        {
            var containers = _storageRepository.GetContainersForStorageLocation(storageLocationId);

            return new JsonResult(containers);
        }

        [HttpGet("api/containers/{id}")]
        public IActionResult GetContainer(int id)
        {
            var container = _storageRepository.GetContainer(id);

            return new JsonResult(container);
        }

        [HttpPost("api/containers")]
        public IActionResult CreateContainer([FromBody]Container newContainer)
        {
            _storageRepository.AddContainer(newContainer);

            return GetContainersForStorageLocation(newContainer.StorageLocationId);
        }

        [HttpPost("api/containers/{id}")]
        public IActionResult UpdateContainer(int id, [FromBody]Container container)
        {
            _storageRepository.SaveContainer(container);

            return GetContainersForStorageLocation(container.StorageLocationId);
        }


    }
}
