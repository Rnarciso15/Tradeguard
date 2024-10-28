using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IToastNotification toastNotification, ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> VerificarPropVista()
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
                var countPropostasNaoAceites = await _context.PropostasDeCompra
                   .Where(p => p.CC_vendedor == user.CC && !p.Proposta_Aceite)
                   .CountAsync();
                ViewData["CountPropostasNaoVistas"] = countPropostasNaoAceites;

                var proposta = await _context.PropostasDeCompra
                    .Where(m => !m.Proposta_vista && m.CC_vendedor == user.CC && !m.Proposta_Aceite)
                    .ToListAsync();

                var userLoginId = await _userManager.GetUserAsync(User);
                var anuncios = await _context.Anuncios
                    .Where(a => a.UserId != userLoginId.Id)
                    .ToListAsync();

                var propostaAnuncio = await _context.PropostasDeCompra
                    .Where(a => a.CC_comprador == userLoginId.CC && a.Proposta_Aceite && !a.Produto_recebido)
                    .ToListAsync();

                var resultado = anuncios
                    .Join(propostaAnuncio,
                    anuncio => anuncio.Id_anuncio,
                    proposta => proposta.Id_Anuncio,
                    (anuncio, proposta) => new PropostaAnuncioViewModel
                    {
                        Proposta = proposta,
                        Anuncio = anuncio
                    })
                    .Count();

                ViewData["countPropostasNaoValidada"] = resultado;

                if (countPropostasNaoAceites > 0 || proposta.Count > 0 || resultado > 0)
                {
                    string mensagem = "";

                    if (proposta.Count > 1)
                    {
                        mensagem += "Têm " + proposta.Count + " propostas por visualizar<br />";
                    }
                    else if (proposta.Count == 1)
                    {
                        mensagem += "Têm " + proposta.Count + " proposta por visualizar<br />";
                    }

                    if (countPropostasNaoAceites > 1)
                    {
                        mensagem += "Têm " + countPropostasNaoAceites + " propostas por aceitar <br />";
                    }
                    else if (countPropostasNaoAceites == 1)
                    {
                        mensagem += "Têm " + countPropostasNaoAceites + " proposta por aceitar <br />";
                    }

                    if (resultado > 1)
                    {
                        mensagem += "Têm " + resultado + " propostas por validar <br />";
                    }
                    else if (resultado == 1)
                    {
                        mensagem += "Têm " + resultado + " proposta por validar <br />";
                    }

                    _toastNotification.AddInfoToastMessage(mensagem);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index()
        {
            try
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
                if (user != null && (User.IsInRole("Administrador") || User.IsInRole("Moderador")))
                {
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                else
                {
                    if (user!= null){
                        var anuncios = await _context.Anuncios
                        .Where(m => m.UserId != user.Id && !m.Venda_Concluida)
                        .ToListAsync();

                        return View(anuncios);
                    }
                    else
                    {
                        var anuncios = await _context.Anuncios
                        .Where(m => !m.Venda_Concluida)
                        .ToListAsync();

                        return View(anuncios);
                    }
                    
                }
            }
            catch
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && (User.IsInRole("Administrador") || User.IsInRole("Moderador")))
                {
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                else
                {
                    return View();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
