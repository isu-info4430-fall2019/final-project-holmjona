using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class SuperHeroController : Controller {


        public SuperHeroController() {//DELETEMEContext context) {
            //
        }

        // GET: SuperHero
        public async Task<IActionResult> Index(int? page, int? count) {
            //List<SuperHero> sups = DAL.SuperHeroesGet();
            List<SuperHero> sups = SuperDAL.GetSuperHeroes();
            Pager pg = new Pager(page, count, sups.Count);
            ViewBag.Pager = pg;
            return View(sups.Skip(pg.Start).Take(pg.CountPerPage));
            //foreach (SuperHero sup in sups) {
            //    List<SuperPet> pets = sup.SuperPets;
            //}
            //return View(sups);
        }

        // GET: SuperHero/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            List<int> ints = new List<int>() {
                1,2,3,4,4,5
            };

            //var superHero= SuperDAL.GetSuperHero
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //SuperHero superHero = DAL.SuperHeroesGet((int)id);
            SuperHero superHero = SuperDAL.GetSuperHero((int)id);
            if (superHero == null) {
                return NotFound();
            }
            ViewData["Title"] = "My Awesome Page";
            ViewData["numbers"] = ints;
            ViewBag.number = ints;

            Response.Cookies.Append("magic", "code");
            string value = Request.Cookies["magic"];


            return View(superHero);
        }

        // GET: SuperHero/Create
        public IActionResult Create() {
            //IClassName -- Interface - Contract - Required set of Methods.
            //ClassName --- Class - Creates Objects (models)

            SuperHero sup = new SuperHero();
            //sup.cal
            ISideKickable<Villian> canHaveSideKick;
            //canHaveSideKick = sup;
            canHaveSideKick = new Villian();

            canHaveSideKick.callForHelp(23);

            //ActionResult;
            //Json();
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ColorsString");
            ViewData["EyeColor"] = GetEnumSelectList(typeof(Person.Color));


            return View();
        }

        //private SelectList GetEnumSelectList(Type type) {
        //    Array values = Enum.GetValues(type);
        //    List<SelectListItem> sList = new List<SelectListItem>();
        //    foreach (var val in values) {
        //        sList.Add(new SelectListItem(Enum.GetName(type,val), ((int)val).ToString()));
        //    }
        //    return new SelectList(sList);
        //}

        private SelectList GetEnumSelectList(Type type) {
            List<object> lst = new List<object>();
            foreach (var val in Enum.GetValues(type)) {
                object li = new { Text = Enum.GetName(type, val), Value = ((int)val).ToString() };
                lst.Add(li);
            }
            return new SelectList(lst, "Value", "Text");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(SuperHero sup) {
        //    string fName = Request.Form["FirstName"];

        //    return View();
        //}

        // POST: SuperHero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Height,FirstName,LastName,BirthDate,ShoeSize,EyeColor")] SuperHero superHero) {
            if (ModelState.IsValid) {
                //superHero);
                //
                //     SuperDAL.AddSuperHero(superHero);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ColorsString", superHero.CostumeID);
            ViewData["EyeColor"] = GetEnumSelectList(typeof(Person.Color));
            return View(superHero);
        }

        // GET: SuperHero/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            //SuperHero superHero= SuperDAL.GetSuperHero((int)id);
            //SuperHero sup = DAL.SuperHeroesGet((int)id);
            SuperHero sup = SuperDAL.GetSuperHero((int)id);
            if (sup == null) {
                return NotFound();
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ColorsString", sup.CostumeID);
            ViewData["EyeColor"] = GetEnumSelectList(typeof(Person.Color));

            return View(sup);
        }

        // POST: SuperHero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Height,FirstName,LastName,BirthDate,ShoeSize,EyeColor,ID")] SuperHero superHero) {
            if (id != superHero.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    superHero.dbUpdate();

                } catch (DbUpdateConcurrencyException) {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ColorsString", superHero.CostumeID);
            ViewData["EyeColor"] = GetEnumSelectList(typeof(Person.Color));
            return View(superHero);
        }

        // GET: SuperHero/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var superHero = SuperDAL.GetSuperHero((int)id);
            if (superHero == null) {
                return NotFound();
            }

            return View(superHero);
        }

        // POST: SuperHero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var superHero = SuperDAL.GetSuperHero((int)id);
            superHero.dbRemove();

            return RedirectToAction(nameof(Index));
        }

        #region AJAX calls

        [HttpPost]
        public ActionResult Get(string searchtext) {
            List<SuperHero> sups = SuperDAL.GetSuperHeroes();
            List<object> answers = new List<object>();
            if (!string.IsNullOrEmpty(searchtext)) {
                foreach (SuperHero sup in sups) {
                    if (sup.FirstName.ToUpper().Contains(searchtext.ToUpper())
                        || sup.LastName.ToUpper().Contains(searchtext.ToUpper())) {
                        answers.Add(new {
                            text = sup.FullName,
                            id = sup.ID
                        });
                    }
                }
            }
            object answer = new {
                success = true,
                message = "Search results for: " + searchtext,
                data = answers
            };
            return Json(answer);
        }

        #endregion

    }
}
