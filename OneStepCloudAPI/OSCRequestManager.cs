using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneStepCloudAPI.Exceptions;
using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI
{

    class OSCRequestManager : IOSCRequestManager
    {
        public static readonly Dictionary<OneStepRegion, string> RegionsUrls = new Dictionary<OneStepRegion, string>()
        {
            [OneStepRegion.UNKNOWN] = "",
            [OneStepRegion.US] = "https://panel.onestepcloud.com/api",
            [OneStepRegion.PL] = "https://panel.onestepcloud.pl/api"
        };

        public string ApiUrl { get; set; }
        public OSCLoginObject AuthenticationData { get; set; }

        public OneStepRegion GetRegion() => region;
        readonly OneStepRegion region;

        readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter> { new TolerantEnumConverter() },
            ContractResolver = new UnderscorePropertyNamesContractResolver()
        };

        readonly IRESTClient restClient;

        public OSCRequestManager(OneStepRegion region)
        {
            this.region = region;
            ApiUrl = RegionsUrls[region];
            restClient = new RESTClient();
        }

        public OSCRequestManager(string apiurl)
        {
            region = OneStepRegion.UNKNOWN;
            ApiUrl = apiurl.TrimEnd('/');
            restClient = new RESTClient();
        }

        public OSCRequestManager(IRESTClient restClient)
        {
            region = OneStepRegion.UNKNOWN;

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

            var req = new RESTRequest()
            {
                Url = ApiUrl + "/" + resource,
                Method = method,
            };

            req.Headers.Add("X-User-Language", "en-US");
            if (authNeeded)
            {
                req.Headers.Add("X-User-Email", AuthenticationData.Email);
                req.Headers.Add("X-User-Token", AuthenticationData.AuthenticationToken);
            }

            if (body.Length > 0)
                req.Body = new StringContent(body, Encoding.UTF8, "application/json");

            using (RESTResponse response = (RESTResponse) await restClient.PerformRequestAsync(req))
            {
                //IF requested type is byte array do not check content and return binary data
                if (typeof(T) == typeof(byte[]))
                {
                    if (response.GetStatusCode() != HttpStatusCode.OK)
                        throw ExceptionHelper.DetermineByHttpCode(response.GetStatusCode());
                    return (T)(object)await response.ReadResponseAsByteArrayAsync();
                }

                string responseBody = await response.ReadResponseAsStringAsync();

                JToken responseContent;
                try { responseContent = JToken.Parse(responseBody); }
                catch (JsonException)
                {
                    if(response.GetStatusCode() != HttpStatusCode.OK)
                        throw ExceptionHelper.DetermineByHttpCode(response.GetStatusCode());
                    throw ExceptionHelper.DetermineByHttpCode(response.GetStatusCode(), "Server response doesn't have valid JSON");
                }

                if (responseContent.Type == JTokenType.Object && responseContent["error"] != null)
                    throw ExceptionHelper.DetermineByHttpCode(response.GetStatusCode(), responseContent["error"].ToString());

                if (response.GetStatusCode() != HttpStatusCode.OK)
                    throw ExceptionHelper.DetermineByHttpCode(response.GetStatusCode());

                if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
                {
                    return (T)(object)responseBody;
                }
                else
                {
                    try { return JsonConvert.DeserializeObject<T>(responseBody, jsonSettings); }
                    catch (JsonException e) { throw new ServerErrorException(response.GetStatusCode(), e.Message, e); }
                }
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
