using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("parts/{0}/", partNumber)));
            var client = new RestClient(options);
            var request = new RestRequest("Part");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.GetAsync<Part>(request);

            return response.Result;
        }

        public Set GetSet(string setNumber)
        {
            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("sets/{0}/", setNumber)));
            var client = new RestClient(options);
            var request = new RestRequest("Set");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.GetAsync<Set>(request);

            return response.Result;
        }

        public SetInstance GetMySet(string setNumber)
        {
            var token = GetUserToken();

            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("/{0}/sets/{1}/", token.UserToken, setNumber)));
            var client = new RestClient(options);
            var request = new RestRequest("MySet");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.GetAsync<SetInstance>(request);

            return response.Result;
        }

        public IEnumerable<SetInstance> GetMySets()
        {
            var token = GetUserToken();

            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("/{0}/sets", token.UserToken)));
            var client = new RestClient(options);
            var request = new RestRequest("MySet");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.GetAsync<IEnumerable<SetInstance>>(request);

            return response.Result;
        }

        public Theme GetTheme(int themeId)
        {
            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("themes/{0}/", themeId)));
            var client = new RestClient(options);
            var request = new RestRequest("Theme");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.GetAsync<Theme>(request);

            return response.Result;
        }

        private Token GetUserToken()
        {
            var options = new RestClientOptions(new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("_token")));
            var client = new RestClient(options);
            var request = new RestRequest("Set");
            request.AddParameter("username", "spragum");
            request.AddParameter("password", "L3g0I@n)");
            request.AddHeader("Authorization", AUTH_KEY);

            var response = client.Post<Token>(request);

            return response;
        }
    }
}
