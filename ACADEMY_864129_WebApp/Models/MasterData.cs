using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACADEMY_864129_WebApp.Models
{
    public class MasterData
    {
        public IList<DeviceData> AzureTableAlertData { get; set; }
        public IList<DeviceData> AzureTableTelemetryData { get; set; }
        public IList<DeviceData> AzureCosmosDBTelemetryData { get; set; }
    }
}
