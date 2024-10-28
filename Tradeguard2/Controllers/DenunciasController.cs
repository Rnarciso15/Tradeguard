using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class DenunciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public DenunciasController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userCC = user.CC;
                return View(await _context.Denuncias.Where(m => m.CC_denunciador == userCC || m.CC_anunciador == userCC).ToListAsync());
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var denuncias = await _context.Denuncias
                    .FirstOrDefaultAsync(m => m.Id_Denuncia == id);
                if (denuncias == null)
                {
                    return NotFound();
                }

                return View(denuncias);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Denuncia,Tipo,Id_Anuncio,CC_denunciador,CC_anunciador,Data,SubTipo")] Denuncias denuncias, string? idAnuncio)
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                denuncias.Data = DateTime.Now;
                var anuncios = await _context.Anuncios
                    .Where(f => f.UserId == user.Id && f.Id_anuncio == denuncias.Id_Anuncio).ToListAsync();
                if (anuncios.Count <= 0)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(denuncias);
                        await _context.SaveChangesAsync();
                        _toastNotification.AddSuccessToastMessage("Denúnica Efetuada!");
                        return RedirectToAction(nameof(Index));
                    }
                    if (idAnuncio != null)
                    {
                        ViewData["idAnuncio"] = idAnuncio;
                    }
                    return View(denuncias);
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Não pode denunciar os seus anúncios!");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var denuncias = await _context.Denuncias
                    .FirstOrDefaultAsync(m => m.Id_Denuncia == id);
                if (denuncias == null)
                {
                    return NotFound();
                }

                return View(denuncias);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var denuncias = await _context.Denuncias.FindAsync(id);
                if (denuncias != null)
                {
                    _context.Denuncias.Remove(denuncias);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }


        public async Task<IActionResult> GetanunciosByInput(string texto)
        {
            List<Denuncias> Denuncias = new List<Denuncias>();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    Denuncias = await _context.Denuncias.Where(p => (EF.Functions.Like(p.Id_Denuncia.ToString(), "%" + texto + "%")) && p.CC_anunciador == user.CC).ToListAsync();

                }
                else
                {
                    Denuncias = await _context.Denuncias.Where(p => p.CC_anunciador == user.CC).ToListAsync();

                }
            }

            return Json(Denuncias);
        }
    }
}

