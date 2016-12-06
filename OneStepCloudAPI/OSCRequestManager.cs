using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OneStepCloudAPI.Exceptions;
using OneStepCloudAPI.Interfaces;
using OneStepCloudAPI.OneStepObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI
{

    public class OSCRequestManager : IOSCRequestManager
    {
        public OSCLoginObject AuthenticationData { get; set; }
        public OneStepRegion Region { get; private set; }

        readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> { new StringEnumConverter(true) },
            ContractResolver = new UnderscorePropertyNamesContractResolver()
        };

        readonly IRestClient restClient;

        public OSCRequestManager(OneStepRegion region)
        {
            Region = region;

            switch (Region)
            {
                case OneStepRegion.US:
                    restClient = new RestClient("https://onestepcloud.com/api");
                    break;
                case OneStepRegion.PL:
                    restClient = new RestClient("https://beta.onestepcloud.pl/api");
                    break;
            }
        }

        public OSCRequestManager(IRestClient restClient)
        {
            Region = OneStepRegion.UNKNOWN;

            this.restClient = restClient;
        }

        public Task<string> SendRequest(string resource, Method method, object body, bool authNeeded = true)
        {
            return SendRequest<string>(resource, method, body, authNeeded);
        }

        public Task<T> SendRequest<T>(string resource, Method method, object body, bool authNeeded = true)
        {
            return SendRequest<T>(resource, method, JsonConvert.SerializeObject(body, jsonSettings), authNeeded);
        }

        public Task<string> SendRequest(string resource, Method method = Method.GET, string body = "", bool authNeeded = true)
        {
            return SendRequest<string>(resource, method, body, authNeeded);
        }

        public async Task<T> SendRequest<T>(string resource, Method method = Method.GET, string body = "", bool authNeeded = true)
        {
            if (authNeeded && (AuthenticationData == null || AuthenticationData.Email == null || AuthenticationData.AuthenticationToken == null || AuthenticationData.Email.Length == 0 || AuthenticationData.AuthenticationToken.Length == 0))
                throw new NotAuthorizedException();

            var req = new RestRequest(resource, method);
            req.RequestFormat = DataFormat.Json;

            if (authNeeded)
            {
                req.AddHeader("X-User-Email", AuthenticationData.Email);
                req.AddHeader("X-User-Token", AuthenticationData.AuthenticationToken);
            }

            if (body.Length > 0)
                req.AddParameter("application/json;charset=utf-8", body, ParameterType.RequestBody);

            IRestResponse response = await restClient.ExecuteTaskAsync(req);

            //IF requested type is byte array do not check content and return binary data
            if (typeof(T) == typeof(byte[]))
                return (T)(object)response.RawBytes;

            JToken responseContent;
            try { responseContent = JToken.Parse(response.Content); }
            catch(JsonException) { throw ExceptionHelper.DetermineByHttpCode(response.StatusCode, "Server response doesn't have valid JSON"); }

            if (responseContent.Type == JTokenType.Object && responseContent["error"] != null)
                throw ExceptionHelper.DetermineByHttpCode(response.StatusCode, responseContent["error"].ToString());

            if (response.StatusCode != HttpStatusCode.OK)
                throw ExceptionHelper.DetermineByHttpCode(response.StatusCode);

            if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            {
                return (T)(object)response.Content;
            }
            else
            {
                try { return JsonConvert.DeserializeObject<T>(response.Content, jsonSettings); }
                catch (JsonException e) { throw new ServerErrorException(response.StatusCode, e.Message, e); }
            }
        }

        public Task<SessionSummary> GetSessionSummary()
        {
            return SendRequest<SessionSummary>("user");
        }

        public async Task<string> GetMaintenanceMessage()
        {
            string res = await SendRequest("maintenance", Method.GET, "", false);

            if (res.Length == 0 || res == "{}" || res == "{ }")
                return "";

            var message = Newtonsoft.Json.Linq.JToken.Parse(res);
            if (message.Type == Newtonsoft.Json.Linq.JTokenType.Object && message["message"] != null)
                return message["message"].ToString();

            throw new ServerErrorException(HttpStatusCode.OK, "Maintenance query response does not match pattern.");
        }
    }
}
