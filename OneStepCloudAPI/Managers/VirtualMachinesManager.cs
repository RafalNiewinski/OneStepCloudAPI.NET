﻿using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneStepCloudAPI.Exceptions;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using OneStepCloudAPI.REST;

namespace OneStepCloudAPI.Managers
{
    public class VirtualMachinesManager
    {
        readonly IOSCRequestManager rm;

        public VirtualMachinesManager(IOSCRequestManager rm)
        {
            this.rm = rm;
        }

        public Task<List<Product>> GetTemplates()
        {
            return rm.SendRequest<List<Product>>("virtual_machines/templates");
        }

        public async Task<List<ProductCategory>> GetProducts()
        {
            return (await rm.SendRequest<ProductCategoriesWrapper>("virtual_machines/products")).ProductCategories;
        }

        public Task<List<VirtualMachineSummary>> GetAll()
        {
            return rm.SendRequest<List<VirtualMachineSummary>>("virtual_machines");
        }

        public async Task<List<VirtualMachine>> GetAllDetailed()
        {
            var summaries = await GetAll();
            var detailed = new List<VirtualMachine>();

            foreach (var vm in summaries)
                detailed.Add(await Get(vm));

            return detailed;
        }

        public Task<VirtualMachine> Get(int id)
        {
            return rm.SendRequest<VirtualMachine>($"virtual_machines/{id}");
        }

        public async Task<int> Create(VirtualMachinePrototype proto)
        {
            return await rm.SendRequest<OSCID>("virtual_machines", Method.POST, proto);
        }

        public async Task<JSchema> GetConfigurationSchema(int id)
        {
            var data = await rm.SendRequest<string>($"virtual_machines/{id}/configure");

            try
            {
                JToken obj = JToken.Parse(data);
                obj = obj["configuration_options"];
                JSchema schema = JSchema.Parse(obj.ToString());
                return schema;
            }
            catch { throw new ServerErrorException(0, "Invalid Server Reply"); }
        }

        public async Task<VirtualMachine> Configure(int id, dynamic proto, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/configure", Method.POST, new { configuration_options = proto });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> Edit(VirtualMachine vm, bool wait = true)
        {
            List<object> disks = new List<object>();
            vm.VirtualMachineDisks.Where(d => d.Id != 0).ToList().ForEach(x => disks.Add(x));
            vm.VirtualMachineDisks.Where(d => d.Id == 0).ToList().ForEach(x => disks.Add(new { storage_gb = x.StorageGb, storage_type = x.StorageType.ToString() }));

            object editvars = new
            {
                cpu = vm.Cpu,
                memory_mb = vm.MemoryMb,
                virtual_machine_disks = disks,
                disk_remove_requirements_performed = true
            };

            await rm.SendRequest($"virtual_machines/{vm.Id}", Method.PATCH, editvars);

            if (wait)
                return await WaitForState(vm.Id, VirtualMachineStatus.idle);
            else
                return await Get(vm.Id);
        }

        public async Task Delete(int id)
        {
            await rm.SendRequest($"virtual_machines/{id}", Method.DELETE, new { id });

            List<VirtualMachineSummary> vms = await GetAll();
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
            {
                if (vms.Where(x => x.Id == id).ToList().Count == 0)
                    return;

                await Task.Delay(OneStepClient.PoolingInterval);
                vms = await GetAll();
            }

            throw new TimeoutException();
        }

        public async Task<VirtualMachine> PowerOn(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/power_on", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> Suspend(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/suspend", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> PowerOff(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/shutdown", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> PowerOffHard(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/hard_poweroff", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> Reboot(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/reboot", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> RebootHard(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/hard_reset", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> SnapshotCreate(int id, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/snapshots", Method.POST, new { id = id });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public async Task<VirtualMachine> SnapshotRevert(int id, int snapid, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/snapshots/{snapid}", Method.PATCH, new { id = snapid });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public Task<VirtualMachine> SnapshotRevert(VirtualMachine vm)
        {
            return SnapshotRevert(vm.Id, vm.VirtualMachineSnapshots.First().Id);
        }

        public async Task<VirtualMachine> SnapshotDelete(int id, int snapid, bool wait = true)
        {
            await rm.SendRequest($"virtual_machines/{id}/snapshots/{snapid}", Method.DELETE, new { id = snapid });

            if (wait)
                return await WaitForState(id, VirtualMachineStatus.idle);
            else
                return await Get(id);
        }

        public Task<VirtualMachine> SnapshotDelete(VirtualMachine vm)
        {
            return SnapshotDelete(vm.Id, vm.VirtualMachineSnapshots.First().Id);
        }

        public async Task<VirtualMachine> Rename(int vmid, string name)
        {
            await rm.SendRequest($"virtual_machines/{vmid}/rename", Method.POST, new { name_tag = name });
            return await Get(vmid);
        }

        public async Task<VirtualMachine> UpdateDescription(int vmid, string description)
        {
            await rm.SendRequest($"virtual_machines/{vmid}/description", Method.PATCH, new { description = description });
            return await Get(vmid);
        }


        #region Permissions Management

        public async Task<List<VirtualMachinePermission>> GetPermissionsForUser(int vm, int user)
        {
            string res = await rm.SendRequest($"virtual_machines/{vm}/permissions/{user}");
            return Newtonsoft.Json.Linq.JObject.Parse(res).SelectToken("$.permissions").ToObject<VirtualMachinePermission[]>().ToList();
        }

        public async Task<List<VirtualMachinePermission>> SetPermissionsForUser(int vm, int user, List<VirtualMachinePermission> perms)
        {
            await rm.SendRequest($"virtual_machines/{vm}/permissions/{user}", Method.PATCH, new { permissions = perms });

            return await GetPermissionsForUser(vm, user);
        }

        public async Task<List<VirtualMachinePermission>> AddPermissionForUser(int vm, int user, VirtualMachinePermission perm)
        {
            List<VirtualMachinePermission> perms = await GetPermissionsForUser(vm, user);
            if (!perms.Contains(perm))
            {
                perms.Add(perm);
                return await SetPermissionsForUser(vm, user, perms);
            }
            return perms;
        }

        public async Task<List<VirtualMachinePermission>> RemovePermissionForUser(int vm, int user, VirtualMachinePermission perm)
        {
            List<VirtualMachinePermission> perms = await GetPermissionsForUser(vm, user);
            if (perms.Contains(perm))
            {
                perms.Remove(perm);
                return await SetPermissionsForUser(vm, user, perms);
            }
            return perms;
        }

        #endregion

        #region Utils

        public async Task<VirtualMachine> WaitForState(int vmid, VirtualMachineStatus state)
        {
            VirtualMachine vm;

            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(OneStepClient.TaskTimeout))
            {
                vm = await Get(vmid);

                if (vm.Status == state)
                    return vm;

                if (vm.Status == VirtualMachineStatus.error)
                    throw new VirtualMachineTaskException("Virtual machine state changed to error");

                await Task.Delay(OneStepClient.PoolingInterval);
            }

            throw new TimeoutException();
        }

        private class ProductCategoriesWrapper { public List<ProductCategory> ProductCategories { get; set; } }
        #endregion
    }
}
