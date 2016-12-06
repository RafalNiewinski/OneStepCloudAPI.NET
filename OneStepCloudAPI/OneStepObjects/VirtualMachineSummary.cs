﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum VirtualMachinePowerState
    {
        powered_off,
        suspended,
        powered_on
    }

    public enum VirtualMachineStatus
    {
        idle,
        error,
        busy,
        configuration,
        configuring,
        deleted
    }

    public enum VirtualMachinePermission
    {
        virtual_machine_configure,
        virtual_machine_view,
        virtual_machine_update,
        virtual_machine_power_on,
        virtual_machine_suspend,
        virtual_machine_reboot,
        virtual_machine_shutdown,
        virtual_machine_delete,
        virtual_machine_snapshot_create,
        virtual_machine_snapshot_revert,
        virtual_machine_snapshot_delete,
        nat_create,
        nat_delete,
        virtual_machine_permissions_create,
        virtual_machine_permissions_update,
        virtual_machine_permissions_delete,
        virtual_machine_console
    }

    public class VirtualMachineSummary
    {
        public int Id { get; set; }
        public string NameTag { get; set; }
        public VirtualMachinePowerState PowerState { get; set; }
        public int Cpu { get; set; }
        public int MemoryMb { get; set; }
        public List<VirtualMachineDisk> VirtualMachineDisks { get; set; }
        public VirtualMachineStatus Status { get; set; }
        public OperatingSystem OperatingSystem { get; set; }
        public List<VirtualMachinePermission> VirtualMachinePermissions { get; set; }


        public bool ChackPermission(VirtualMachinePermission perm)
        {
            if (!VirtualMachinePermissions.Contains(perm))
                return false;

            return true;
        }
    }
}
