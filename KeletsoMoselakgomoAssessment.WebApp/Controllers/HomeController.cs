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

            if(System.IO.File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var text = sr.ReadToEnd();
                    employees = JsonConvert.DeserializeObject<List<Employee>>(text);
                }
            }

            return employees;
        }
    }
}
