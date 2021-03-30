using FrontendReport.Models;
using FrontendReport.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReadData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FrontendReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReadData _readData;
      
        public ReportController(ILogger<ReportController> logger, IReadData readData)
        {
            _logger = logger;
            _readData = readData;
        }
        public IActionResult Index()
        {
            ReportViewModel reportViewModel = new ReportViewModel();
            var jsonData= _readData.ReadFromJsonFile("BDG_Output.json");
            reportViewModel.Reports = JsonConvert.DeserializeObject<List<Report>>(jsonData);
            return View(reportViewModel);
        }
    }
}
