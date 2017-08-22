using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.REST
{
    public interface IRESTClient
    {
        Task<IRESTResponse> PerformRequestAsync(IRESTRequest request);
    }

    class RESTClient : IRESTClient
    {
        HttpClient httpClient = new HttpClient();

        public async Task<IRESTResponse> PerformRequestAsync(IRESTRequest request)
        {
            var req = new HttpRequestMessage()
            {
                Method = new HttpMethod(request.Method.ToString()),
                RequestUri = new Uri(request.Url),
                Content = request.Body
            };
            foreach (var header in request.Headers)
                req.Headers.Add(header.Key, header.Value);

            return new RESTResponse(await httpClient.SendAsync(req));
        }
    }
}
