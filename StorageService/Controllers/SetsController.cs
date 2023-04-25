using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using StorageServices.Domain;
using StorageServices.Services;

using System.Collections.Generic;
using System.Linq;

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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MySet, MySet>();
            });

            var mapper = config.CreateMapper();

            foreach (var set in mySets)
            {
                var rebrickableSet = myRebrickableSets.FirstOrDefault(s => s.Set.SetNumber == set.SetNumber);

                var domainSet = new MySet();

                domainSet = mapper.Map<MySet>(set);

                if (rebrickableSet != null)
                {
                    domainSet = mapper.Map<MySet>(rebrickableSet);
                }

                domainSets.Add(domainSet);
            }

            return domainSets;
        }

    }
}
