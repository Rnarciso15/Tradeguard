using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class ElogiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElogiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Elogios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Elogios.ToListAsync());
        }

        // GET: Elogios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elogios = await _context.Elogios
                .FirstOrDefaultAsync(m => m.Id_Elogio == id);
            if (elogios == null)
            {
                return NotFound();
            }

            return View(elogios);
        }

        // GET: Elogios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Elogios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Elogio,Tipo,Id_Anuncio,CC,Data")] Elogios elogios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elogios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(elogios);
        }

        // GET: Elogios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elogios = await _context.Elogios.FindAsync(id);
            if (elogios == null)
            {
                return NotFound();
            }
            return View(elogios);
        }

        // POST: Elogios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Elogio,Tipo,Id_Anuncio,CC,Data")] Elogios elogios)
        {
            if (id != elogios.Id_Elogio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elogios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElogiosExists(elogios.Id_Elogio))
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
            return View(elogios);
        }

        // GET: Elogios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elogios = await _context.Elogios
                .FirstOrDefaultAsync(m => m.Id_Elogio == id);
            if (elogios == null)
            {
                return NotFound();
            }

            return View(elogios);
        }

        // POST: Elogios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elogios = await _context.Elogios.FindAsync(id);
            if (elogios != null)
            {
                _context.Elogios.Remove(elogios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElogiosExists(int id)
        {
            return _context.Elogios.Any(e => e.Id_Elogio == id);
        }
    }
}
