using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using StorageServices.Entities;
using StorageServices.Entities.Rebrickable;
using Set = StorageServices.Entities.Rebrickable.Set;

namespace StorageServices.Services
{
    public class ReferenceRepository : IReferenceRepository
    {
        private const string AUTH_KEY = "key 733a4b6440f6cb7107829bcaaecbd7fb";
        private const string BASE_REBRICKABLE_API = "https://rebrickable.com/api/v3";
        private const string LEGO_API = "/lego/";
        private const string USER_API = "/users/";

        public Part GetPart(string partNumber)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("parts/{0}/", partNumber));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Part";

            var response = client.ExecuteGetTaskAsync<Part>(request);

            return response.Result.Data;
        }

        public Set GetSet(string setNumber)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("sets/{0}/", setNumber));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Set";

            var response = client.ExecuteGetTaskAsync<Set>(request);

            return response.Result.Data;
        }

        public MySet GetMySet(string setNumber)
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("/{0}/sets/{1}/",token.UserToken, setNumber));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "MySet";

            var response = client.ExecuteGetTaskAsync<MySet>(request);

            return response.Result.Data;
        }

        private Token GetUserToken()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("_token"));
            var request = new RestRequest();
            request.AddParameter("username", "spragum");
            request.AddParameter("password", "L3g0I@n)");
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Set";

            var response = client.ExecuteAsPost<Token>(request,"POST");

            return response.Data;
        }
    }
}
