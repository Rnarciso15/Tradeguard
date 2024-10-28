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
    public class FavoritosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public FavoritosController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        public async Task<IActionResult> RemoverTodosAnunciosFavoritos()
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
                var userLogin = user.CC;
                var favoritos = await _context.Favoritos
                    .Where(f => f.CC == userLogin)
                    .ToListAsync();

                _context.Favoritos.RemoveRange(favoritos);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para remover anúncios da Lista de favoritos.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
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
                var CC = user.CC;
                var viewModel = await _context.Anuncios
                  .Join(
                      _context.Favoritos,
                      anuncio => anuncio.Id_anuncio,
                      favorito => favorito.Id_Anuncio,
                      (anuncio, favorito) => new AnunciosFavoritosViewModel
                      {
                          Anuncios = anuncio,
                          Favoritos = favorito
                      }
                  )
                  .Where(af => !af.Anuncios.Venda_Concluida && af.Favoritos.CC == user.CC) // Adicionando filtro para anúncios onde a venda não foi concluída
                  .ToListAsync();
                return View(viewModel);

            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver a Lista de favoritos.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Favorito,Id_Anuncio")] Favoritos favoritos)
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
                favoritos.CC = user.CC;
                var existingFavorite = await _context.Favoritos.FirstOrDefaultAsync(f => f.CC == favoritos.CC && f.Id_Anuncio == favoritos.Id_Anuncio);
                var anuncios = await _context.Anuncios.Where(f => f.UserId == user.Id && f.Id_anuncio == favoritos.Id_Anuncio).ToListAsync();
                if (anuncios.Count < 1)
                {
                    if (existingFavorite != null)
                    {
                        _toastNotification.AddInfoToastMessage("Anúncio já está na lista de Favoritos!");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(favoritos.CC) && favoritos.Id_Anuncio != null)
                        {
                            _context.Add(favoritos);
                            await _context.SaveChangesAsync();
                            _toastNotification.AddSuccessToastMessage("Anúncio adicionada á lista de Favoritos!");
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Não pode adicionar os seus anúncios á lista de favoritos!");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para adicionar á Lista de favoritos.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Id_Favorito")] int Id_Favorito)
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
                var favoritos = await _context.Favoritos.FirstOrDefaultAsync(a=> a.Id_Anuncio == Id_Favorito);
                if (favoritos != null)
                {
                    _context.Favoritos.Remove(favoritos);
                    _toastNotification.AddInfoToastMessage("Anúncio removido da lista de favoritos");
                }
                await _context.SaveChangesAsync();
                return View("Index");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para adicionar á Lista de favoritos.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
    }
}
