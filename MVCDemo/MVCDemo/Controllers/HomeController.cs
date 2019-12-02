using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            List<string> salts = new List<string>();
            for (int i = 0; i < 6; i++) {
                salts.Add("a");//Hasher.GenerateSalt());
            }

           //throw new Exception();

            ViewBag.Hash = new List<Tuple<string, string>>() {
                new Tuple<string,string>(Hasher.HashIt("tester",salts[0]),salts[0]),
                new Tuple<string,string>(Hasher.HashIt("tester",salts[1]),salts[1]),
                new Tuple<string,string>(Hasher.HashIt("tester",salts[2]),salts[2]),
                new Tuple<string,string>(Hasher.HashIt("tester",salts[3]),salts[3]),
                new Tuple<string,string>(Hasher.HashIt("tester",salts[4]),salts[4]),
                new Tuple<string,string>(Hasher.HashIt("tester",salts[5]),salts[5])
            };
            //if (HttpContext.Session.Keys.Contains("user")) {
            //    Response.WriteAsync(HttpContext.Session.GetString("user"));
            //}
            Models.User usr = SessionHelper.Get<User>(HttpContext.Session, "user");
            if (usr != null) Response.WriteAsync(usr.UserName);
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

        //public IActionResult Login(string UserName, string Password) {


        //    System.IO.StreamWriter fs = new System.IO.StreamWriter("secrets.psw");
        //    fs.Write(UserName + " | " + Password);
        //    fs.Close();

        //    return View();
        //}


        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
