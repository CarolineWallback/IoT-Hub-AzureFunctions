using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Models.RegisterDevice
{
    internal class RegisterDeviceRequest
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
    }
}
