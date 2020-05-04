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

        public async Task<IActionResult> Index()
        {            
            MasterData deviceDataList = new MasterData
            {
                AzureTableAlertData = await bigStoreService.GetAlertData(),
                AzureTableTelemetryData = await bigStoreService.GetTelemetryData(),
                AzureCosmosDBTelemetryData = await bigStoreService.GetCosmosTelemetryData()
            };
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
