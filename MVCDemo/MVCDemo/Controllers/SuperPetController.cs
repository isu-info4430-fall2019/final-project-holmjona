﻿using System;
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
    public class SuperPetController : Controller
    {
       

        public SuperPetController(DELETEMEContext context)
        {
            
        }

        // GET: SuperPet
        public ActionResult Index()
        {
            //    ViewBag.PetTypes = SuperDAL.GetPetTypes();
            List<SuperPet> lst = SuperDAL.GetSuperPets();
            //foreach (SuperPet sPut  in lst) {
            //    sPut.PetType = SuperDAL.GetPetType(sPut.PetTypeID);
            //    sPut.SuperHero = SuperDAL.GetSuperHero(sPut.SuperHeroID);
            //}
            return View(lst);
        }

        // GET: SuperPet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperPet superPet = SuperDAL.GetSuperPet((int)id);
            if (superPet == null)
            {
                return NotFound();
            }

            return View(superPet);
        }

        // GET: SuperPet/Create
        public IActionResult Create()
        {
            //ViewData["PetTypeID"] = new SelectList(_context.Set<PetType>(), "ID", "ID");
            //ViewData["SuperHeroID"] = new SelectList(_context.SuperHero, "ID", "FirstName");
            return View();
        }

        // POST: SuperPet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PetTypeID,SuperHeroID,ID")] SuperPet superPet)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(superPet);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PetTypeID"] = new SelectList(_context.Set<PetType>(), "ID", "ID", superPet.PetTypeID);
            //ViewData["SuperHeroID"] = new SelectList(_context.SuperHero, "ID", "FirstName", superPet.SuperHeroID);
            return View(superPet);
        }

        // GET: SuperPet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SuperPet superPet = SuperDAL.GetSuperPet((int)id); //= await _context.SuperPet.FindAsync(id);
            if (superPet == null)
            {
                return NotFound();
            }
            //ViewData["PetTypeID"] = new SelectList(_context.Set<PetType>(), "ID", "ID", superPet.PetTypeID);
            //ViewData["SuperHeroID"] = new SelectList(_context.SuperHero, "ID", "FirstName", superPet.SuperHeroID);
            return View(superPet);
        }

        // POST: SuperPet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PetTypeID,SuperHeroID,ID")] SuperPet superPet)
        {
            if (id != superPet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(superPet);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
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
            //ViewData["PetTypeID"] = new SelectList(_context.Set<PetType>(), "ID", "ID", superPet.PetTypeID);
            //ViewData["SuperHeroID"] = new SelectList(_context.SuperHero, "ID", "FirstName", superPet.SuperHeroID);
            return View(superPet);
        }

        // GET: SuperPet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var superPet = await _context.SuperPet
            //    .Include(s => s.PetType)
            //    .Include(s => s.SuperHero)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            SuperPet superPet = SuperDAL.GetSuperPet((int)id);
            if (superPet == null)
            {
                return NotFound();
            }

            return View(superPet);
        }

        // POST: SuperPet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var superPet = await _context.SuperPet.FindAsync(id);
            //_context.SuperPet.Remove(superPet);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool SuperPetExists(int id)
        //{
        //    return _context.SuperPet.Any(e => e.ID == id);
        //}
    }
}
