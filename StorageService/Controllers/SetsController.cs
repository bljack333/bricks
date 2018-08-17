using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public IEnumerable<MySet> GetAllOfMySets()
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

        [HttpGet("{setState}")]
        public IEnumerable<MySet> GetSetsByState(SetState setState)
        {
            var allMySets = GetAllOfMySets();

            return allMySets.Where(s => s.SetState == setState);
        }
    }
}
