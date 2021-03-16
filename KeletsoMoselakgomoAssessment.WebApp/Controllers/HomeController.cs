using KeletsoMoselakgomoAssessment.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace KeletsoMoselakgomoAssessment.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetEmployees());
        }

        private List<Employee> GetEmployees()
        {
            string path = @"C:\Temp\BDG_Output.json";
            var employees = new List<Employee>();
            using (StreamReader sr = new StreamReader(path))
            {
                employees = JsonConvert.DeserializeObject<List<Employee>>(sr.ReadToEnd());
            }

            return employees;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
