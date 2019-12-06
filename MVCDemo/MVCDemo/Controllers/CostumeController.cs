using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MVCDemo;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class CostumeController : Controller {


        public CostumeController() {

        }

        // GET: Costume
        public async Task<IActionResult> Index(int? page, int? count,string color) {
            List<Costume> lst;
            if (color != null) {
                 lst = SuperDAL.GetCostumes(color);
            } else {
                 lst = SuperDAL.GetCostumes();
            }
            Pager pg = new Pager(page, count, lst.Count);
            ViewBag.Pager = pg;
            return View(lst.Skip(pg.Start).Take(pg.CountPerPage));
        }

        // GET: Costume/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var costume = SuperDAL.GetCostume((int)id);
            if (costume == null) {
                return NotFound();
            }

            return View(costume);
        }

        // GET: Costume/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Costume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask,ID")] Costume costume) {
            if (ModelState.IsValid) {
                costume.dbAdd();

                return RedirectToAction(nameof(Index));
            }
            return View(costume);
        }

        // GET: Costume/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var costume = SuperDAL.GetCostume((int)id);
            if (costume == null) {
                return NotFound();
            }
            return View(costume);
        }

        // POST: Costume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorMain,ColorSecondary,ColorTertiary,HasCape,HasMask,ID")] Costume costume) {
            if (id != costume.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    costume.dbUpdate();

                } catch (DbUpdateConcurrencyException) {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(costume);
        }

        // GET: Costume/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var costume = SuperDAL.GetCostume((int)id);
            if (costume == null) {
                return NotFound();
            }

            return View(costume);
        }

        // POST: Costume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var costume = SuperDAL.GetCostume((int)id);
            costume.dbRemove();

            return RedirectToAction(nameof(Index));
        }

        #region Extra Actions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colorString"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        /// <remarks> 
        /// To do dynamic images, needed to add the:
        /// System.Drawing.Common.4.6.1 NuGet Package
        /// https://stackoverflow.com/questions/47937591/asp-net-core-generate-image-file-dynamically 
        /// </remarks>
        public IActionResult ImageFor(string colors, int? height, int? width) {

            // default to 50 x 50 image
            height = height == null ? 50 : height;
            width = width == null ? 50 : width;
            Bitmap bm = new Bitmap((int)width, (int)height);
            Graphics gr = Graphics.FromImage(bm);
            String[] colorArr = colors.Split("|");

            if (colorArr.Length != 3) colorArr = new string[] { "0", "0", "0" };
            Costume fkCostume = new Costume() {
                ColorMainAsHexString = colorArr[0],
                ColorSecondaryAsHexString = colorArr[1],
                ColorTertiaryAsHexString = colorArr[2]
            };

            DrawCircle(gr, fkCostume.ColorMain, 0, 0, (int)(height * .9), (int)(width * .9));
            DrawCircle(gr, fkCostume.ColorSecondary, (int)(height * .3), (int)(width * .3), (int)(height * .7), (int)(width * .7));
            DrawCircle(gr, fkCostume.ColorTertiary, (int)(height * .5), (int)(width * .1), (int)(height * .5), (int)(width * .5));

            MemoryStream ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
           
            FileContentResult fcr = new FileContentResult(ms.ToArray(),"image/png");
            ms.Dispose();
            gr.Dispose();
            bm.Dispose();
            return fcr;

        }

        private void DrawCircle(Graphics gr, int colorValue, int top, int left, int height, int width) {
            gr.FillEllipse(new SolidBrush(Color.FromArgb(colorValue + Costume.ConvertFromHex("FF000000"))),
                new Rectangle(left, top, width, height));
        }

        #endregion


    }
}
