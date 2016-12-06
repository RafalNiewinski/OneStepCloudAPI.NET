using OneStepCloudAPI.OneStepObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Interfaces
{
    interface IOSCRequestManager
    {
        Task<string> SendRequest(string resource, Method method, object body, bool authNeeded = true);
        Task<T> SendRequest<T>(string resource, Method method, object body, bool authNeeded = true);
        Task<string> SendRequest(string resource, Method method = Method.GET, string body = "", bool authNeeded = true);
        Task<T> SendRequest<T>(string resource, Method method = Method.GET, string body = "", bool authNeeded = true);
        Task<SessionSummary> GetSessionSummary();
        Task<string> GetMaintenanceMessage();
    }
}
