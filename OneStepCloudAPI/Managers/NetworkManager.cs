using OneStepCloudAPI.Exceptions;
using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class NetworkManager
    {
        readonly IOSCRequestManager rm;

        public NetworkManager(IOSCRequestManager rm)
        {
            this.rm = rm;
        }

        #region Public Networks
        public Task<List<PublicNetworkSummary>> GetPublicNetworks()
        {
            return rm.SendRequest<List<PublicNetworkSummary>>("public_networks");
        }

        public async Task<List<PublicNetwork>> GetPublicNetworksDetailed()
        {
            var summaries = await GetPublicNetworks();
            var detailed = new List<PublicNetwork>();

            foreach (var net in summaries)
                detailed.Add(await GetPublicNetwork(net));

            return detailed;
        }

        public Task<PublicNetwork> GetPublicNetwork(int netid)
        {
            return rm.SendRequest<PublicNetwork>($"public_networks/{netid}");
        }

        public async Task<PublicNetwork> CreatePublicNetwork(bool wait = true)
        {
            int id = await rm.SendRequest<OSCID>("public_networks", Method.POST);

            if (wait)
                return await WaitForNetState(id, NetworkResourceStatus.idle);
            else
                return await GetPublicNetwork(id);
        }

        public async Task DeletePublicNetwork(int netid, bool wait = true)
        {
            await rm.SendRequest($"public_networks/{netid}", Method.DELETE);

            if (wait)
            {
                List<PublicNetworkSummary> nets = await GetPublicNetworks();
                var startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
                {
                    if (nets.Where(x => x.Id == netid).ToList().Count == 0)
                        return;

                    await Task.Delay(OneStepClient.PoolingInterval);
                    nets = await GetPublicNetworks();
                }

                throw new TimeoutException();
            }

            throw new TimeoutException();
        }
        #endregion

        #region NATs
        public async Task<Dictionary<int, List<VirtualMachineNAT>>> GetNats()
        {
            var data = await rm.SendRequest<List<object>>("nats");

            var dict = new Dictionary<int, List<VirtualMachineNAT>>();

            foreach (var o in data)
            {
                var vmdata = Newtonsoft.Json.Linq.JObject.Parse(o.ToString());
                dict.Add(vmdata.SelectToken("$.id").ToObject<int>(), vmdata.SelectToken("$.nats").ToObject<List<VirtualMachineNAT>>());
            }

            return dict;
        }

        public async Task<List<VirtualMachineNAT>> GetNatsFlatten()
        {
            return (await GetNats()).Values.SelectMany(x => x).ToList();
        }

        public async Task<NetworkNAT> CreateNat(int vmid, int publicnetworkid, int privatenetworkid, string sourceportrange, string destportrange, NetworkProtocol protocol, bool wait = true)
        {
            int id = await rm.SendRequest<OSCID>("nats/advanced", Method.POST,
                new
                {
                    virtual_machine_id = vmid,
                    public_network_id = publicnetworkid,
                    private_network_id = privatenetworkid,
                    destination_port_range = destportrange,
                    source_port_range = sourceportrange,
                    protocol
                }
            );

            if(wait)
                return await WaitForNatState(id, publicnetworkid, NetworkResourceStatus.idle);
            else
                return (await GetPublicNetwork(publicnetworkid)).Nats.Where(n => n.Id == id).First();
        }

        public async Task DeleteNat(int natid, bool wait = true)
        {
            await rm.SendRequest($"nats/{natid}", Method.DELETE, new { id = natid });

            if (wait)
            {
                var nats = await GetNatsFlatten();
                var startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
                {
                    if (nats.Where(x => x.Id == natid).ToList().Count == 0)
                        return;

                    await Task.Delay(OneStepClient.PoolingInterval);
                    nats = await GetNatsFlatten();
                }

                throw new TimeoutException();
            }
        }
        #endregion

        #region Utils

        public async Task<PublicNetwork> WaitForNetState(int netid, NetworkResourceStatus state)
        {
            PublicNetwork net;

            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
            {
                net = await GetPublicNetwork(netid);

                if (net.Status == state)
                    return net;

                if (net.Status == NetworkResourceStatus.error)
                    throw new PublicNetworkTaskException("Network state changed to error");

                await Task.Delay(OneStepClient.PoolingInterval);
            }

            throw new TimeoutException();
        }

        public async Task<NetworkNAT> WaitForNatState(int natid, int netid, NetworkResourceStatus state)
        {
            NetworkNAT nat;

            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
            {
                var nats = (await GetPublicNetwork(netid)).Nats;
                nat = nats.Where(n => n.Id == natid).First();

                if (nat.Status == state)
                    return nat;

                if (nat.Status == NetworkResourceStatus.error)
                    throw new PublicNetworkTaskException("NAT state changed to error");

                await Task.Delay(OneStepClient.PoolingInterval);
            }

            throw new TimeoutException();
        }
        #endregion
    }
}
