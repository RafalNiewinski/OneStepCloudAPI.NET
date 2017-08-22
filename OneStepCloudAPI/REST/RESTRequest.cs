using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.REST
{
    public interface IRESTRequest
    {
        Dictionary<string,string> Headers { get; set; }
        HttpContent Body { get; set; }
        string Url { get; set; }
        Method Method { get; set; }
    }

    class RESTRequest : IRESTRequest
    {
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public HttpContent Body { get; set; }
        public string Url { get; set; }
        public Method Method { get; set; } = Method.GET;
    }
}
