using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class SuperHeroController : Controller
    {
        public IActionResult Index()
        {
            //SuperHero sup = new SuperHero("Bob");
            Villian sup = new Villian("Steve") { LastName = "Tia" };
            return View(sup);
        }
        public IActionResult List() {
            List<SuperHero> supes = DAL.SuperHeroesGet();
            return View(supes);
        }
    }
}