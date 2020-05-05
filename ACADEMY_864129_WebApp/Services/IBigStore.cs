using ACADEMY_864129_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebApp.Services
{
    public interface IBigStore
    {
        Task<IList<DeviceData>> GetAlertData();
        Task<IList<DeviceData>> GetTelemetryData();
        Task<IList<DeviceData>> GetCosmosTelemetryData();
        Task<IList<DeviceInfo>> GetConnectedDevices();
        Task PostMessageToIoTHub(DeviceData deviceData);
    }
}
