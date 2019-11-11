using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
            SuperHero sup = new SuperHero("TheBat");

        }

        public IActionResult Index() {
            return View();
        }

        public ActionResult Cool(int? number) {
            string num = Request.Query["number"];
            return View("CoolName");
        }

        [HttpPost]
        public ActionResult Cool() {
            return View("CoolName");
        }



        public IActionResult Jump() {
            ViewData["Title"] = "Awesommes!!";
            ViewBag.FuzzyAnimals = "are cute";
            
            return View("PinkPanther");
        }


        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
