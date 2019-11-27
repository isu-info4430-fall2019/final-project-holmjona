using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class PetTypeController : Controller
    {
        

        public PetTypeController()
        {
            
        }

        // GET: PetType
        public async Task<IActionResult> Index(int? page, int? count) {
            List<PetType> lst = SuperDAL.GetPetTypes();
            Pager pg = new Pager(page, count, lst.Count);
            ViewBag.Pager = pg;
            return View(lst.Skip(pg.Start).Take(pg.CountPerPage));
        }

        // GET: PetType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType= SuperDAL.GetPetType((int)id);
            if (petType == null)
            {
                return NotFound();
            }

            return View(petType);
        }

        // GET: PetType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ID")] PetType petType)
        {
            if (ModelState.IsValid)
            {
                petType.dbAdd();
                return RedirectToAction(nameof(Index));
            }
            return View(petType);
        }

        // GET: PetType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType= SuperDAL.GetPetType((int)id);
            if (petType == null)
            {
                return NotFound();
            }
            return View(petType);
        }

        // POST: PetType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID")] PetType petType)
        {
            if (id != petType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    petType.dbUpdate();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(petType);
        }

        // GET: PetType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petType= SuperDAL.GetPetType((int)id);
            if (petType == null)
            {
                return NotFound();
            }

            return View(petType);
        }

        // POST: PetType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petType= SuperDAL.GetPetType((int)id);
            petType.dbRemove();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
