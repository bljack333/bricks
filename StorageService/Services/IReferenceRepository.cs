using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using StorageServices.Entities;
using StorageServices.Entities.Rebrickable;
using Set = StorageServices.Entities.Rebrickable.Set;
using RestSharp;

namespace StorageServices.Services
{
    public interface IReferenceRepository
    {
        Set GetSet(string setNumber);
        Task<Response<PartInstance>> GetPart(string partNumber);
        SetInstance GetMySet(string setNumber);
        Task<IRestResponse<Response<SetInstance>>> GetMySets();
        Theme GetTheme(int themeId);
        Response<PartInstance> GetMyParts();
        Task<Response<PartInstance>> GetMyParts(int page, int size);
    }
}
