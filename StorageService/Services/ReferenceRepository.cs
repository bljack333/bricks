using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using StorageServices.Entities;
using StorageServices.Entities.Rebrickable;
using Set = StorageServices.Entities.Rebrickable.Set;
using Newtonsoft.Json;

namespace StorageServices.Services
{
    public class ReferenceRepository : IReferenceRepository
    {
        private const string AUTH_KEY = "key 733a4b6440f6cb7107829bcaaecbd7fb";
        private const string BASE_REBRICKABLE_API = "https://rebrickable.com/api/v3";
        private const string LEGO_API = "/lego/";
        private const string USER_API = "/users/";

        public  async Task<Response<PartInstance>> GetPart(string partNumber)
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("{0}/allparts/?part_num={1}", token.UserToken, partNumber));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Response";

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));

            RestResponse response = (RestResponse)(await taskCompletion.Task);
            return JsonConvert.DeserializeObject<Response<PartInstance>>(response.Content);
        }

        public Response<PartInstance> GetMyParts()
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("{0}/allparts/", token.UserToken));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Response<PartInstance>>";

            var response = client.ExecuteGetTaskAsync<Response<PartInstance>>(request);

            return response.Result.Data;
        }

        public async Task<Response<PartInstance>> GetMyParts(int page, int size)
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("{0}/allparts/", token.UserToken));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Response<PartInstance>>";
            request.AddParameter("page", page);
            request.AddParameter("page_size", size);

            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = client.ExecuteAsync(request, r => taskCompletion.SetResult(r));

            RestResponse response = (RestResponse)(await taskCompletion.Task);
            return JsonConvert.DeserializeObject<Response<PartInstance>>(response.Content);
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

        public SetInstance GetMySet(string setNumber)
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("{0}/sets/{1}/",token.UserToken, setNumber));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "MySet";

            var response = client.ExecuteGetTaskAsync<SetInstance>(request);

            return response.Result.Data;
        }

        public async Task<IRestResponse<Response<SetInstance>>> GetMySets()
        {
            var token = GetUserToken();

            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("{0}/sets/", token.UserToken));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "MySet";

            var response = client.ExecuteGetTaskAsync<Response<SetInstance>>(request);

            var waited = await response;
            return waited;
        }

        public Theme GetTheme(int themeId)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + LEGO_API + String.Format("themes/{0}/", themeId));
            var request = new RestRequest();
            request.AddHeader("Authorization", AUTH_KEY);
            request.RootElement = "Theme";

            var response = client.ExecuteGetTaskAsync<Theme>(request);

            return response.Result.Data;
        }

        private Token GetUserToken()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BASE_REBRICKABLE_API + USER_API + String.Format("_token/"));
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
