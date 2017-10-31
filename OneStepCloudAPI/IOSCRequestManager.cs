using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI
{
    public interface IOSCRequestManager
    {
        OSCLoginObject AuthenticationData { get; set; }

        OneStepRegion GetRegion();
        Task<string> SendRequest(string resource, Method method, object body, bool authNeeded = true);
        Task<T> SendRequest<T>(string resource, Method method, object body, bool authNeeded = true);
        Task<string> SendRequest(string resource, Method method = Method.GET, string body = "", bool authNeeded = true);
        Task<T> SendRequest<T>(string resource, Method method = Method.GET, string body = "", bool authNeeded = true);
        Task<SessionSummary> GetSessionSummary();
        Task<string> GetMaintenanceMessage();
    }
}
