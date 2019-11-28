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
        /// <summary>
        /// Handle Login request from user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password) {
            Models.User usr = SuperDAL.GetUser(username, password);
            if (usr != null) {
                // success
                Response.Cookies.Append("user", SuperDAL.GetCookie(usr));
                TempData["SuccessMessage"] = "Successful Login";
            } else {
                // failed
                TempData["ErrorMessage"] = "Login Failed";
            }
            return  RedirectToAction("Index", "Home");
        }

        public ActionResult Logout() {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register() {

            //return View("Create",new { });
            ViewBag.Action = "Register";
            return RedirectToAction("Create");
        }

    }
}