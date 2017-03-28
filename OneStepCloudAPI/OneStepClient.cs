using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneStepCloudAPI.OneStepObjects;
using RestSharp;
using Newtonsoft.Json;
using OneStepCloudAPI.Managers;

namespace OneStepCloudAPI
{
    public enum OneStepRegion
    {
        US,
        PL,
        UNKNOWN
    }

    public class OneStepClient
    {
        public static int PoolingInterval = 5000; //5 Sec
        public static int TaskTimeout = 600000; //10 Min

        public OSCLoginObjectPrototype LoginCredentials
        {
            get { return loginCredentials; }
            set { loginCredentials = value; Authorized = false; }
        }
        private OSCLoginObjectPrototype loginCredentials;

        public bool Authorized { get; private set; }
        public VirtualMachinesManager VirtualMachines { get; private set; }
        public NetworkManager Network { get; private set; }
        public BillingManager Billing { get; private set; }
        public AccountManager Account { get; private set; }
        public UsersManager Users { get; private set; }
        public VpnManager Vpn { get; private set; }


        OSCRequestManager requestManager;


        public OneStepClient(OneStepRegion region)
        {
            requestManager = new OSCRequestManager(region);
            VirtualMachines = new VirtualMachinesManager(requestManager);
            Network = new NetworkManager(requestManager);
            Billing = new BillingManager(requestManager);
            Account = new AccountManager(requestManager);
            Users = new UsersManager(requestManager);
            Vpn = new VpnManager(requestManager);
        }

        public OneStepRegion Region() { return requestManager.Region; }
        public OSCLoginObject AuthData() { return requestManager.AuthenticationData; }
        public async Task<SessionSummary> SessionSummary() { return await requestManager.GetSessionSummary(); }
        public async Task<string> MainenanceMessage() { return await requestManager.GetMaintenanceMessage(); }

        public Task SignIn(string username, string password)
        {
            LoginCredentials = new OSCLoginObjectPrototype { Username = username, Password = password };
            return SignIn();
        }

        public async Task SignIn()
        {
            if (LoginCredentials.Username.Length == 0 || LoginCredentials.Password.Length == 0)
                throw new InvalidOperationException("Username or password empty");

            requestManager.AuthenticationData = await requestManager.SendRequest<OSCLoginObject>("user/login", Method.POST, LoginCredentials, false);

            if (requestManager.AuthenticationData != null && requestManager.AuthenticationData != null && requestManager.AuthenticationData.AuthenticationToken != null)
                Authorized = true;
        }

        public Task<Dictionary<string, string>> GetOSCPrices()
        {
            return requestManager.SendRequest<Dictionary<string, string>>("prices");
        }

    }
}
