using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StorageServices.Entities.Rebrickable;
using StorageServices.Services;

namespace StorageServices.Controllers
{
    [Route("/api/themes")]
    public class ThemeController : Controller
    {
        private IReferenceRepository _referenceRepository;

        public ThemeController(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }

        [HttpGet("/{themeId}")]
        public Theme GetTheme(int themeId)
        {
            return _referenceRepository.GetTheme(themeId);
        }
    }
}