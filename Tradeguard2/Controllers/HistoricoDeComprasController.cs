using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class HistoricoDeComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricoDeComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.HistoricoDeCompra.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoDeCompra = await _context.HistoricoDeCompra.FirstOrDefaultAsync(m => m.Id_historico == id);
            if (historicoDeCompra == null)
            {
                return NotFound();
            }

            return View(historicoDeCompra);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_historico,Tipo,Id_Anuncio,CC_vendedor,CC_comprador,Preco,vendido,Data")] HistoricoDeCompra historicoDeCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoDeCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historicoDeCompra);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoDeCompra = await _context.HistoricoDeCompra.FindAsync(id);
            if (historicoDeCompra == null)
            {
                return NotFound();
            }
            return View(historicoDeCompra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_historico,Tipo,Id_Anuncio,CC_vendedor,CC_comprador,Preco,vendido,Data")] HistoricoDeCompra historicoDeCompra)
        {
            if (id != historicoDeCompra.Id_historico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoDeCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoDeCompraExists(historicoDeCompra.Id_historico))
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
            return View(historicoDeCompra);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoDeCompra = await _context.HistoricoDeCompra.FirstOrDefaultAsync(m => m.Id_historico == id);
            if (historicoDeCompra == null)
            {
                return NotFound();
            }

            return View(historicoDeCompra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoDeCompra = await _context.HistoricoDeCompra.FindAsync(id);
            if (historicoDeCompra != null)
            {
                _context.HistoricoDeCompra.Remove(historicoDeCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoDeCompraExists(int id)
        {
            return _context.HistoricoDeCompra.Any(e => e.Id_historico == id);
        }
    }
}
