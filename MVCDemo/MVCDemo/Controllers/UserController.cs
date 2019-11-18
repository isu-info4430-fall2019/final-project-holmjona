using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password) {

            return View();
        }

        public ActionResult Register() {

            //return View("Create",new { });
            ViewBag.Action = "Register";
            return RedirectToAction("Create");
        }

    }
}