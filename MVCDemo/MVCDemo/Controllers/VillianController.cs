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
    public class VillianController : Controller
    {
        

        public VillianController()
        {
            
        }

        // GET: Villian
        public IActionResult Index(int? page, int? count) {
            List<Villian> vills = SuperDAL.GetVillians();
            Pager pg = new Pager(page, count, vills.Count);
            ViewBag.Pager = pg;
            return View(vills.Skip(pg.Start).Take(pg.CountPerPage));
        }

        // GET: Villian/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Villian villian = SuperDAL.GetVillian((int)id);
            if (villian == null)
            {
                return NotFound();
            }

            return View(villian);
        }

        // GET: Villian/Create
        public IActionResult Create()
        {
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ID");
            return View();
        }

        // POST: Villian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostumeID,FirstName,LastName,BirthDate,ShoeSize,EyeColor,Height,ID")] Villian villian)
        {
            if (ModelState.IsValid)
            {
                villian.dbAdd();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ID", villian.CostumeID);
            return View(villian);
        }

        // GET: Villian/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Villian villian = SuperDAL.GetVillian((int)id);
            if (villian == null)
            {
                return NotFound();
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ID", villian.CostumeID);
            return View(villian);
        }

        // POST: Villian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CostumeID,FirstName,LastName,BirthDate,ShoeSize,EyeColor,Height,ID")] Villian villian)
        {
            if (id != villian.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    villian.dbUpdate();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostumeID"] = new SelectList(SuperDAL.GetCostumes(), "ID", "ID", villian.CostumeID);
            return View(villian);
        }

        // GET: Villian/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Villian villian = SuperDAL.GetVillian((int)id);
            if (villian == null)
            {
                return NotFound();
            }

            return View(villian);
        }

        // POST: Villian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var villian= SuperDAL.GetVillian((int)id);
            villian.dbRemove();
            return RedirectToAction(nameof(Index));
        }
    }
}
