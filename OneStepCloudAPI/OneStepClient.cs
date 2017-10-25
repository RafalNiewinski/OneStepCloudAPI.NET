using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.Managers;
using OneStepCloudAPI.REST;

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

        public VirtualMachinesManager VirtualMachines { get; private set; }
        public NetworkManager Network { get; private set; }
        public BillingManager Billing { get; private set; }
        public MarketplaceManager Marketplace { get; private set; }
        public AccountManager Account { get; private set; }
        public UsersManager Users { get; private set; }
        public VpnManager Vpn { get; private set; }


        OSCRequestManager requestManager;


        public OneStepClient(OneStepRegion region)
        {
            requestManager = new OSCRequestManager(region);
            Initialize();
        }

        public OneStepClient(string apiurl)
        {
            requestManager = new OSCRequestManager(apiurl);
            Initialize();
        }

        private void Initialize()
        {
            VirtualMachines = new VirtualMachinesManager(requestManager);
            Network = new NetworkManager(requestManager);
            Billing = new BillingManager(requestManager);
            Marketplace = new MarketplaceManager(requestManager);
            Account = new AccountManager(requestManager);
            Users = new UsersManager(requestManager);
            Vpn = new VpnManager(requestManager);
        }

        public OneStepRegion Region() { return requestManager.Region; }
        public OSCLoginObject AuthData() { return requestManager.AuthenticationData; }
        public async Task<SessionSummary> SessionSummary() { return await requestManager.GetSessionSummary(); }
        public async Task<string> MainenanceMessage() { return await requestManager.GetMaintenanceMessage(); }

        public async Task PasswordSignIn(string username, string password)
        {
            if (username.Length == 0 || password.Length == 0)
                throw new InvalidOperationException("Username or password empty");

            requestManager.AuthenticationData = await requestManager.SendRequest<OSCLoginObject>("user/login", Method.POST, new { Username = username, Password = password }, false);
        }

        public async Task TokenSignIn(string email, string token)
        {
            if (email.Length == 0 || token.Length == 0)
                throw new InvalidOperationException("Email or token empty");

            requestManager.AuthenticationData = new OSCLoginObject { Email = email, AuthenticationToken = token };
            requestManager.AuthenticationData.Primary = (await SessionSummary()).Primary;
        }

        public async Task Register(string email, string username, string password)
        {
            if (email.Length == 0 || username.Length == 0 || password.Length == 0)
                throw new InvalidOperationException("Username or password empty");

            requestManager.AuthenticationData = await requestManager.SendRequest<OSCLoginObject>("users", Method.POST, new
            {
                User = new
                {
                    AcceptedTermsAndConditions = true,
                    Email = email,
                    Username = username,
                    Password = password
                }
            }, false);
        }

        public async Task ConfirmAccount(string code)
        {
            await requestManager.SendRequest($"users/confirm?confirmation_token={code}", Method.GET, new { }, false);
        }

        public Task<Dictionary<string, string>> GetOSCPrices()
        {
            return requestManager.SendRequest<Dictionary<string, string>>("prices");
        }

    }
}
