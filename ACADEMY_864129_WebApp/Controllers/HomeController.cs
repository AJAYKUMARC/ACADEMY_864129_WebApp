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

namespace ACADEMY_864129_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<DeviceData> deviceDataList = new List<DeviceData>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44383/api/Device/GetTelemetryDataTable"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    deviceDataList = JsonConvert.DeserializeObject<List<DeviceData>>(apiResponse);
                }
            }
            return View(deviceDataList);
        }

        public IActionResult ViewDevices()
        {
            return View("DevicesConnected");
        }

        public IActionResult ViewConfigurations()
        {
            return View("ViewConfigurations");
        }
    }
}
