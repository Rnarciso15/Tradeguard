using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Linq;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class MensagensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public MensagensController(ApplicationDbContext context, IHubContext<ChatHub> hubContext, UserManager<ApplicationUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        public IActionResult Index(string idDestinatario, int? primeiramensagem)
            {
            var user = _userManager.GetUserAsync(User).Result;
            if (user != null)
            {
                if (user.Id != idDestinatario)
                {
                    var id = user.Id;
                    var user1 = _userManager.Users.FirstOrDefault(u => u.Id == id);
                    var mensagens = _context.Mensagens
                        .Where(m => m.Utilizador_1 == id || m.Utilizador_2 == id)
                        .ToList();
                    if (mensagens.Count > 0 || primeiramensagem == 1)
                    {
                        ViewBag.User = user1;
                        if (idDestinatario != null)
                        {
                            ViewData["IdDestinatario"] = idDestinatario;
                            _hubContext.Clients.All.SendAsync("ReceiveMessage", user1.Nome).Wait();
                        }
                        return View("Mensagens", mensagens);
                    }
                    else
                    {
                        _toastNotification.AddInfoToastMessage("Ainda não tem mensagens com um utilizador!");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Não pode enviar mensagens a si mesmo!");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para enviar mensagens.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpGet]
        public IActionResult CarregarMensagens(string idUsuario1, string idUsuario2)
        {
            var mensagens = _context.Mensagens
                .Where(m =>
                    (m.Utilizador_1 == idUsuario1 && m.Utilizador_2 == idUsuario2) ||
                    (m.Utilizador_1 == idUsuario2 && m.Utilizador_2 == idUsuario1))
                .ToList();
            var userId = _userManager.GetUserAsync(User).Result.Id;

            foreach (var mensagem in mensagens)
            {
                if (userId != mensagem.Utilizador_1)
                {
                    mensagem.Mensagem_Vista = true;
                }
            }

            _context.SaveChangesAsync().Wait();

            var mensagensViewModel = mensagens.Select(m => new
            {
                Usuario = ObterNomeUsuario(m.Utilizador_1).Result,
                Mensagem = m.texto,
                Data = m.Data,
                mensagemVista = m.Mensagem_Vista,
                userId = ObterIDUsuario(m.Utilizador_1).Result
            });

            return Json(mensagensViewModel);
        }

        private async Task<string> ObterNomeUsuario(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Nome;
        }

        private async Task<string> ObterIDUsuario(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Id;
        }
    }
}
