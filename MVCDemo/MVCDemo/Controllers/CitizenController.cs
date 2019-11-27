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
    public class CitizenController : Controller {


        public CitizenController() {
            //
        }

        // GET: Citizen
        public async Task<IActionResult> Index(int? page, int? count) {
            List<Citizen> cits = new List<Citizen>();
            Pager pg = new Pager(page, count, cits.Count);
            ViewBag.Pager = pg;
            return View(cits.Skip(pg.Start).Take(pg.CountPerPage));
        }

        // GET: Citizen/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var citizen = SuperDAL.GetCitizen((int)id);
            if (citizen == null) {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizen/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Citizen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,BirthDate,ShoeSize,EyeColor,Height,ID")] Citizen citizen) {
            if (ModelState.IsValid) {
                citizen.dbAdd();

                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizen/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var citizen = SuperDAL.GetCitizen((int)id);
            if (citizen == null) {
                return NotFound();
            }
            return View(citizen);
        }

        // POST: Citizen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,BirthDate,ShoeSize,EyeColor,Height,ID")] Citizen citizen) {
            if (id != citizen.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    citizen.dbUpdate();

                } catch (DbUpdateConcurrencyException) {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(citizen);
        }

        // GET: Citizen/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var citizen = SuperDAL.GetCitizen((int)id);
            if (citizen == null) {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var citizen = SuperDAL.GetCitizen((int)id);
            citizen.dbRemove();

            return RedirectToAction(nameof(Index));
        }

    }
}
