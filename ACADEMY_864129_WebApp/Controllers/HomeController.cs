using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ACADEMY_864129_WebApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using ACADEMY_864129_WebApp.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace ACADEMY_864129_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBigStore bigStoreService;
        private readonly AppSettings appSettings;
        public HomeController(ILogger<HomeController> logger, IBigStore bigStoreService, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            this.bigStoreService = bigStoreService;
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAlertData()
        {
            var azureTableAlertData = await bigStoreService.GetAlertData();
            return Json(azureTableAlertData);
        }

        public async Task<JsonResult> GetTelemetryData()
        {
            var azureTableAlertData = await bigStoreService.GetTelemetryData();
            return Json(azureTableAlertData);
        }

        public async Task<JsonResult> GetCosmosTelemetryData()
        {
            var azureTableAlertData = await bigStoreService.GetCosmosTelemetryData();
            return Json(azureTableAlertData);
        }

        public async Task<IActionResult> ViewDevicesAsync()
        {
            IList<DeviceInfo> deviceData = await bigStoreService.GetConnectedDevices();
            return View("DevicesConnected", deviceData);
        }

        public async Task<IActionResult> ViewConfigurationsAsync()
        {
            IList<DeviceInfo> deviceData = await bigStoreService.GetConnectedDevices();
            var selectedListItems = new List<SelectListItem>();
            foreach (var item in deviceData)
            {
                selectedListItems.Add(new SelectListItem { Text = item.DeviceId, Value = item.DeviceId });
            }
            ViewBag.Data = new SelectList(selectedListItems, "Value", "Text");
            if (TempData.ContainsKey("AlertCheck"))
            {
                ViewBag.Alert = true;
                TempData.Remove("Alert");
            }
            else 
            {
                ViewBag.Alert = false;
            }

            return View("ViewConfigurations");
        }

        public async Task<IActionResult> SendData(DeviceData deviceData)
        {
            await bigStoreService.PostMessageToIoTHub(deviceData);
            TempData["AlertCheck"] = true;
            return RedirectToAction("ViewConfigurations");
        }

    }
}
