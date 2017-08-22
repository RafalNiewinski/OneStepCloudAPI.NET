using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.REST
{
    public interface IRESTResponse
    {
        HttpResponseMessage HttpResponse { get; }

        HttpStatusCode GetStatusCode();
        Task<string> ReadResponseAsStringAsync();
        Task<byte[]> ReadResponseAsByteArrayAsync();
    }

    class RESTResponse : IRESTResponse, IDisposable
    {
        public HttpResponseMessage HttpResponse { get; }

        public RESTResponse(HttpResponseMessage res) => HttpResponse = res;

        public HttpStatusCode GetStatusCode() => HttpResponse.StatusCode;

        public async Task<byte[]> ReadResponseAsByteArrayAsync() => HttpResponse.Content != null ? await HttpResponse.Content.ReadAsByteArrayAsync() : null;

        public async Task<string> ReadResponseAsStringAsync() => HttpResponse.Content != null ? await HttpResponse.Content.ReadAsStringAsync() : null;

        public void Dispose()
        {
            if (HttpResponse != null) HttpResponse.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
