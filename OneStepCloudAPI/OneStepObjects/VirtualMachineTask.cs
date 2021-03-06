﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum VirtualMachineTaskStatus
    {
        unknown,
        finished,
        error,
        running
    }

    public class VirtualMachineTask
    {
        public int Id { get; set; }
        public VirtualMachineTaskStatus Status { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public static implicit operator int(VirtualMachineTask task) { return task.Id; }
    }
}
