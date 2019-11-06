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
    public class SuperHeroController : Controller
    {
        private readonly DELETEMEContext _context;

        public SuperHeroController(DELETEMEContext context)
        {
            _context = context;
        }

        // GET: SuperHero
        public async Task<IActionResult> Index()
        {
            List<SuperHero> sups = DAL.SuperHeroesGet();
            return View(sups);
        }

        // GET: SuperHero/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superHero = await _context.SuperHero
                .FirstOrDefaultAsync(m => m.ID == id);
            if (superHero == null)
            {
                return NotFound();
            }

            return View(superHero);
        }

        // GET: SuperHero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuperHero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Height,FirstName,LastName,BirthDate,ShoeSize,EyeColor,ID")] SuperHero superHero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(superHero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(superHero);
        }

        // GET: SuperHero/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //SuperHero superHero = await _context.SuperHero.FindAsync(id);
            SuperHero sup = DAL.SuperHeroesGet((int)id);
            if (sup == null)
            {
                return NotFound();
            }
            return View(sup);
        }

        // POST: SuperHero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Height,FirstName,LastName,BirthDate,ShoeSize,EyeColor,ID")] SuperHero superHero)
        {
            if (id != superHero.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(superHero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuperHeroExists(superHero.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(superHero);
        }

        // GET: SuperHero/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superHero = await _context.SuperHero
                .FirstOrDefaultAsync(m => m.ID == id);
            if (superHero == null)
            {
                return NotFound();
            }

            return View(superHero);
        }

        // POST: SuperHero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var superHero = await _context.SuperHero.FindAsync(id);
            _context.SuperHero.Remove(superHero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuperHeroExists(int id)
        {
            return _context.SuperHero.Any(e => e.ID == id);
        }
    }
}
