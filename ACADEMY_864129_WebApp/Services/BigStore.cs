using ACADEMY_864129_WebApp.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebApp.Services
{
    public class BigStore : IBigStore
    {
        private readonly AppSettings appSettings;
        public BigStore(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        public async Task<IList<DeviceData>> GetAlertData()
        {
            var deviceDataList = new List<DeviceData>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(appSettings.BigStoreAPI + "GetAlertDataTable"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deviceDataList = JsonConvert.DeserializeObject<List<DeviceData>>(apiResponse);
                    deviceDataList = deviceDataList.GroupBy(o => new DeviceData
                    {
                        Month = Convert.ToDateTime(o.RowKey).Month,
                        Temperature = o.Temperature,
                        DoorStatus = o.DoorStatus,
                        Humidity = o.Humidity
                    })
         .Select(g => new DeviceData
         {
             Month = g.Key.Month,
             Temperature = g.Key.Temperature,
             DoorStatus = g.Key.DoorStatus,
             Humidity = g.Key.Humidity
         }).OrderByDescending(a => a.Month).ToList();
                }
            }
            return deviceDataList;
        }
        public async Task<IList<DeviceInfo>> GetConnectedDevices()
        {
            var deviceDataList = new List<DeviceInfo>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(appSettings.BigStoreAPI + "GetConfigInfo"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deviceDataList = JsonConvert.DeserializeObject<List<DeviceInfo>>(apiResponse);
                }
            }
            return deviceDataList;
        }
        public async Task<IList<DeviceData>> GetCosmosTelemetryData()
        {
            var deviceDataList = new List<DeviceData>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(appSettings.BigStoreAPI + "GetTelemetryDataCosmos"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deviceDataList = JsonConvert.DeserializeObject<List<DeviceData>>(apiResponse);
                    deviceDataList = deviceDataList.GroupBy(o => new DeviceData
                    {
                        Month = Convert.ToDateTime(o.RowKey).Month,
                        Temperature = o.Temperature,
                        DoorStatus = o.DoorStatus,
                        Humidity = o.Humidity
                    })
        .Select(g => new DeviceData
        {
            Month = g.Key.Month,
            Temperature = g.Key.Temperature,
            DoorStatus = g.Key.DoorStatus,
            Humidity = g.Key.Humidity
        }).OrderByDescending(a => a.Month).ToList();
                }
            }
            return deviceDataList;
        }
        public async Task<IList<DeviceData>> GetTelemetryData()
        {
            var deviceDataList = new List<DeviceData>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(appSettings.BigStoreAPI + "GetNormalDataTable"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deviceDataList = JsonConvert.DeserializeObject<List<DeviceData>>(apiResponse);
                    deviceDataList = deviceDataList.GroupBy(o => new DeviceData
                    {
                        Month = Convert.ToDateTime(o.RowKey).Month,
                        Temperature = o.Temperature,
                        DoorStatus = o.DoorStatus,
                        Humidity = o.Humidity
                    })
         .Select(g => new DeviceData
         {
             Month = g.Key.Month,
             Temperature = g.Key.Temperature,
             DoorStatus = g.Key.DoorStatus,
             Humidity = g.Key.Humidity
         }).OrderByDescending(a => a.Month).ToList();
                }
            }
            return deviceDataList;
        }

        public async Task PostMessageToIoTHub(DeviceData deviceData)
        {
            using (var httpClient = new HttpClient())
            {
                var deviceJSON = JsonConvert.SerializeObject(deviceData);
                var data = new StringContent(deviceJSON, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(appSettings.BigStoreAPI + "MessageToDevice", data))
                {
                    string result = await response.Content.ReadAsStringAsync();
                }
            }            
        }
    }
}
