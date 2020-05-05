using Microsoft.WindowsAzure.Storage.Table;

namespace ACADEMY_864129_WebApp.Models
{
    public class DeviceData : TableEntity
    {
        public string DeviceId { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public bool DoorStatus { get; set; }
        public int Month { get; set; }
    }
}
