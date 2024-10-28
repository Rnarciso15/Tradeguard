using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using NToastNotify;
using Tradeguard2.Data;
using Tradeguard2.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace Tradeguard2.Controllers
{
    public class AvaliacaosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        public AvaliacaosController( ApplicationDbContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userCC = user.CC;
                return View(await _context.Avaliacao.Where(p => p.CC_Vendedor == user.CC).ToListAsync());
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para ver o histórico de denúncias.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
       

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userTemAvaliacao = await _context.PropostasDeCompra.Where(p => p.CC_comprador == user.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true).ToListAsync();
                if (userTemAvaliacao.Count < 1)
                {
                    _toastNotification.AddInfoToastMessage("Não é possível avaliar.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Avaliacao,Avaliacao_Atribuida,CC_Vendedor,CC_Comprador,Data")] Avaliacao avaliacao)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var propostaNaoAvaliada = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Vendedor_Avaliado == false && p.CC_comprador == user.CC);
                avaliacao.CC_Vendedor = propostaNaoAvaliada.CC_vendedor;
                avaliacao.CC_Comprador = user.CC;
                avaliacao.Data = DateTime.Now;
                
                var avaliacoesVendedor = await _context.Avaliacao.Where(p => p.CC_Vendedor == avaliacao.CC_Vendedor).ToListAsync();
                double media = 0;
                var avaliacao_atribuida = avaliacao.Avaliacao_Atribuida;
                if (avaliacoesVendedor.Any())
                {
                    double mediaAtual = avaliacoesVendedor.Average(p => p.Avaliacao_Atribuida);                  
                    media = (mediaAtual + avaliacao_atribuida) / 2;
                }
                else
                {                    
                    media = avaliacao_atribuida;
                }
                media = Math.Max(0, Math.Min(5, media));
                int mediaFinal = (int)Math.Round(media);

                var usuario = await _context.Users.FirstOrDefaultAsync(u => u.CC == avaliacao.CC_Vendedor);

                if (usuario != null)
                {
                    usuario.Avaliacao = mediaFinal;
                    await _context.SaveChangesAsync();
                }

                propostaNaoAvaliada.Vendedor_Avaliado = true;
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> GetanunciosByInput(string texto)
            {
            List<Avaliacao> Avaliacao = new List<Avaliacao>();
            var user = await _userManager.GetUserAsync(User);
            if( user != null)
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    Avaliacao = await _context.Avaliacao.Where(p => ( EF.Functions.Like(p.Id_Avaliacao.ToString(), "%" + texto + "%") ) && p.CC_Vendedor == user.CC).ToListAsync();

                }
                else
                {
                    Avaliacao = await _context.Avaliacao.Where(p=> p.CC_Vendedor == user.CC).ToListAsync();

                }
            }
          
            return Json(Avaliacao);
        }


    

    }



}

