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
    public class CostumeController : Controller
    {
        

        public CostumeController()
        {
            
        }

        // GET: Costume
        public async Task<IActionResult> Index()
        {
            return View(SuperDAL.GetCostumes());
        }

        // GET: Costume/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costume= SuperDAL.GetCostume((int)id);
            if (costume == null)
            {
                return NotFound();
            }

            return View(costume);
        }

        // GET: Costume/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask,ID")] Costume costume)
        {
            if (ModelState.IsValid)
            {
                costume.dbAdd();
                
                return RedirectToAction(nameof(Index));
            }
            return View(costume);
        }

        // GET: Costume/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costume= SuperDAL.GetCostume((int)id);
            if (costume == null)
            {
                return NotFound();
            }
            return View(costume);
        }

        // POST: Costume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask,ID")] Costume costume)
        {
            if (id != costume.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    costume.dbUpdate();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(costume);
        }

        // GET: Costume/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costume= SuperDAL.GetCostume((int)id);
            if (costume == null)
            {
                return NotFound();
            }

            return View(costume);
        }

        // POST: Costume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costume= SuperDAL.GetCostume((int)id);
            costume.dbRemove();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
