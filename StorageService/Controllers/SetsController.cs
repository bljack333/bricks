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
            var myRebrickableSets = _referenceRepository.GetMySets().Result.Data;
            var domainSets = new List<MySet>();

            foreach (var rebrickableSet in myRebrickableSets.Results)
            {
                var set = mySets.FirstOrDefault(s => s.SetNumber == rebrickableSet.Set.SetNumber);

                var domainSet = new MySet();

                domainSet = Mapper.Map<MySet>(rebrickableSet.Set);

                if (set != null)
                {
                    domainSet = Mapper.Map<MySet>(set);
                }
                else
                {
                    domainSet.SetState = SetState.Unclassified;
                }

                domainSets.Add(domainSet);
            }

            return domainSets;
        }

        private MySet GetSetBySetNumber(string setNumber)
        {
            var myRebrickableSet = _referenceRepository.GetMySet(setNumber);

            var domainSet = _setRepository.GetSetBySetNumber(setNumber);

            var resultSet = Mapper.Map<MySet>(myRebrickableSet.Set);

            if (domainSet != null)
            {
                resultSet = Mapper.Map<MySet>(domainSet);
            }

            return resultSet;
        }

    }
}
