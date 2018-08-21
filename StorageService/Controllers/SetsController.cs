using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StorageServices.Domain;
using StorageServices.Entities.Rebrickable;
using StorageServices.Services;

namespace StorageServices.Controllers
{
    [Route("/api/sets")]
    public class SetsController : Controller
    {
        private ISetRepository _setRepository;
        private IReferenceRepository _referenceRepository;

        public SetsController(ISetRepository setRepository, IReferenceRepository referenceRepository)
        {
            _setRepository = setRepository;
            _referenceRepository = referenceRepository;
        }

        [HttpGet()]
        public IActionResult GetAllOfMySets()
        {
            return new JsonResult(GetAllSets());
        }

        [HttpGet("{setState}")]
        public IActionResult GetSetsByState(SetState setState)
        {
            var allMySets = GetAllSets();

            return new JsonResult(allMySets.Where(s => s.SetState == setState));
        }

        private IEnumerable<MySet> GetAllSets()
        {
            var mySets = _setRepository.GetMySets();
            var myRebrickableSets = _referenceRepository.GetMySets();
            var domainSets = new List<MySet>();

            foreach (var set in mySets)
            {
                var rebrickableSet = myRebrickableSets.FirstOrDefault(s => s.Set.SetNumber == set.SetNumber);

                var domainSet = new MySet();

                domainSet = Mapper.Map<MySet>(set);

                if (rebrickableSet != null)
                {
                    domainSet = Mapper.Map<MySet>(rebrickableSet);
                }

                domainSets.Add(domainSet);
            }

            return domainSets;
        }

    }
}
