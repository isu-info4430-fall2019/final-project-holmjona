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
    public class SuperPetController : Controller {


        public SuperPetController() {

        }

        // GET: SuperPet
        public ActionResult Index(int? page, int? count) {
            //    ViewBag.PetTypes = SuperDAL.GetPetTypes();
            List<SuperPet> lst = SuperDAL.GetSuperPets();
            //foreach (SuperPet sPut  in lst) {
            //    sPut.PetType = SuperDAL.GetPetType(sPut.PetTypeID);
            //    sPut.SuperHero = SuperDAL.GetSuperHero(sPut.SuperHeroID);
            //}
            if (page != null && count != null) {
                Pager pg = new Pager(page, count, lst.Count);
                ViewBag.Pager = pg;
                return View(lst.Skip(pg.Start).Take(pg.CountPerPage));
            } else {
                return View(lst);
            }
        }

        // GET: SuperPet/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            SuperPet superPet = SuperDAL.GetSuperPet((int)id);
            if (superPet == null) {
                return NotFound();
            }

            return View(superPet);
        }

        // GET: SuperPet/Create
        public IActionResult Create() {
            Models.User cUser = SuperDAL.GetUserForCookie(Request.Cookies["user"]);
            if (cUser == null) {
                // see Shared/_Layout for this error message use.
                // TempData is used to pass data through a Redirect.
                TempData["ErrorMessage"] = "You need to login to view add a Super Pet.";
                // ViewBag and ViewData will only pass down to a View returned
                // by this action. 
                return RedirectToAction("Index", "Home");
            } else if (cUser.Role.SuperPetAdd) {
                ViewData["PetTypeID"] = new SelectList(SuperDAL.GetPetTypes(), "ID", "Name");
                ViewData["SuperHeroID"] = new SelectList(SuperDAL.GetSuperHeroes(), "ID", "FullName");
                return View();
            } else {
                TempData["ErrorMessage"] = "You do not have permission to Add a SuperPet.";
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: SuperPet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PetTypeID,SuperHeroID,ID")] SuperPet superPet) {
            if (ModelState.IsValid) {
                //superPet);
                //
                //SuperDAL.AddSuperPet(superPet);
                superPet.dbAdd();
                if (false) {
                    //
                    ViewData["ErrorMessage"] = "Ooops";
                    ViewData["PetTypeID"] = new SelectList(SuperDAL.GetPetTypes(), "ID", "Name");
                    ViewData["SuperHeroID"] = new SelectList(SuperDAL.GetSuperHeroes(), "ID", "FullName");
                    return View(superPet);
                } else {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["PetTypeID"] = new SelectList(SuperDAL.GetPetTypes(), "ID", "Name");
            ViewData["SuperHeroID"] = new SelectList(SuperDAL.GetSuperHeroes(), "ID", "FullName");
            return View(superPet);
        }

        // GET: SuperPet/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            SuperPet superPet = SuperDAL.GetSuperPet((int)id); //= await SuperPet((int)id);
            if (superPet == null) {
                return NotFound();
            }
            ViewData["PetTypeID"] = new SelectList(SuperDAL.GetPetTypes(), "ID", "Name");
            ViewData["SuperHeroID"] = new SelectList(SuperDAL.GetSuperHeroes(), "ID", "FullName");
            return View(superPet);
        }

        // POST: SuperPet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PetTypeID,SuperHeroID,ID")] SuperPet superPet) {
            if (id != superPet.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    //superPet);
                    //
                    //SuperDAL.UpdateCitizen(superPet);
                    superPet.dbUpdate();
                } catch (DbUpdateConcurrencyException) {
                    //if (!SuperPetExists(superPet.ID))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetTypeID"] = new SelectList(SuperDAL.GetPetTypes(), "ID", "Name");
            ViewData["SuperHeroID"] = new SelectList(SuperDAL.GetSuperHeroes(), "ID", "FullName");
            return View(superPet);
        }

        // GET: SuperPet/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            //var superPet= SuperDAL.GetSuperPet
            //    .Include(s => s.PetType)
            //    .Include(s => s.SuperHero)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            SuperPet superPet = SuperDAL.GetSuperPet((int)id);
            if (superPet == null) {
                return NotFound();
            }

            return View(superPet);
        }

        // POST: SuperPet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            //var superPet= SuperDAL.GetSuperPet((int)id);
            //SuperPet.Remove(superPet);
            //
            return RedirectToAction(nameof(Index));
        }

        //private bool SuperPetExists(int id)
        //{
        //    return SuperPet.Any(e => e.ID == id);
        //}

        public IActionResult Cart() {
            ViewBag.InCart = GetPetsInCart(Request.Cookies["cartIds"]);
            return View(SuperDAL.GetSuperPets());
        }

        private List<SuperPet> GetPetsInCart(string idString) {
            List<SuperPet> retList = new List<SuperPet>();
            if (!string.IsNullOrEmpty(idString)) {
                string[] ids = idString.Split(",");
                foreach (string idSt in ids) {
                    SuperPet sp = SuperDAL.GetSuperPet(idSt, false);
                    if (sp != null)
                        retList.Add(sp);
                }
            }
            return retList;
        }


        #region AJAX calls

        [HttpPost]
        public ActionResult Get(string searchtext) {
            List<SuperPet> sups = SuperDAL.GetSuperPets();
            List<object> answers = new List<object>();
            if (!string.IsNullOrEmpty(searchtext)) {
                foreach (SuperPet sup in sups) {
                    if (sup.Name.ToUpper().Contains(searchtext.ToUpper())) {
                        answers.Add(new {
                            text = sup.Name,
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

        [HttpPost]
        public ActionResult GetForSuperHero(int? id) {
            if (id > 0) {
                SuperHero tempHero = new SuperHero() { ID = (int)id };
                List<SuperPet> sups = SuperDAL.GetSuperPets(tempHero);
                return PartialView("Parts/_TableData", sups);

                //List<SuperPet> answers = new List<SuperPet>();
                //foreach (SuperPet sup in sups) {
                //    if (sup.Name.ToUpper().Contains(searchtext.ToUpper())) {
                //        answers.Add(sup);
                //    }
                //}
                //return PartialView("Parts/_TableData", answers);

            }
            return NotFound();
        }

        public ActionResult AddToCart(int? petId) {
            string cook = Request.Cookies["cartIds"];
            cook = cook == null ? "" : cook;
            cook += "," + petId;
            Response.Cookies.Append("cartIds", cook);
            var a = new { success = true, derpy = "rabbits", loopy = 457687 };
            return Json(a);
            //SuperPet p = new SuperPet();
            //p.Name = "Fluffy";
            //p.ID = 23;
            //return Json(p);
        }


        public ActionResult GetCart() {
            List<SuperPet> inCart = new List<SuperPet>();
            inCart = GetPetsInCart(Request.Cookies["cartIds"]);
            List<object> retList = new List<object>();
            foreach (SuperPet petInCart in inCart) {
                retList.Add(new {
                    text = petInCart.Name,
                    id = petInCart.ID
                });
            }
            return Json(retList);
        }

        #endregion


    }
}
