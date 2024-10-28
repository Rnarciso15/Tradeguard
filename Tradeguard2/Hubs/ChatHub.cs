using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Linq;
using Tradeguard2.Models;
using Tradeguard2.Data;
using Microsoft.AspNetCore.Hosting;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IToastNotification _toastNotification;

    public ChatHub(IToastNotification toastNotification, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment appEnvironment)
    {
        _userManager = userManager;
        _context = context;
        _appEnvironment = appEnvironment;
        _toastNotification = toastNotification;
    }

    public void SendMessage(string user, string message, string dateTime)
    {
        try
        {
            var dataHora = DateTime.Parse(dateTime);

            var userId = _userManager.GetUserAsync(Context.User).Result.Id;
            if (userId != null)
            {
                if (!string.IsNullOrWhiteSpace(user) && user.Trim() != "")
                {
                    var newMessage = new Mensagens
                    {
                        Utilizador_1 = userId,
                        Utilizador_2 = user,
                        texto = message,
                        Data = dataHora,
                        Mensagem_Vista = false,
                        Mensagem_Apagada = false
                    };

                    _context.Mensagens.Add(newMessage);
                    _context.SaveChangesAsync().Wait();

                    // Marcar mensagens anteriores como vistas quando uma nova mensagem é enviada
                    MarkMessagesAsViewed(userId, user);

                    var userNome = _userManager.FindByIdAsync(userId).Result.Nome;

                    // Enviar a mensagem para o cliente
                    Clients.All.SendAsync("ReceiveMessage", userId, userNome, message, dataHora, false).Wait();
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Precisa selecionar um utilizador na lista lateral.");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login para adicionar à Lista de favoritos.");
            }
        }
        catch (Exception ex)
        {
            // Registrar informações detalhadas sobre a exceção
            Console.WriteLine($"Erro no método SendMessage: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            throw; // Re-lançar a exceção para lidar com ela no cliente
        }
    }

    public void MarkMessagesAsViewed(string senderId, string receiverId)
    {
        // Marcar todas as mensagens do remetente para o destinatário como vistas
        var messagesToUpdate = _context.Mensagens
            .Where(m => (m.Utilizador_1 == senderId && m.Utilizador_2 == receiverId) ||
                        (m.Utilizador_1 == receiverId && m.Utilizador_2 == senderId) &&
                        !m.Mensagem_Vista)
            .ToList();

        var userId = _userManager.GetUserAsync(Context.User).Result.Id;

        foreach (var message in messagesToUpdate)
        {
            if (userId != message.Utilizador_1)
            {
                message.Mensagem_Vista = true;
            }
        }

        _context.SaveChangesAsync().Wait();
    }

    public async void SendProposal(string receiverCC, string message)
    {
        var senderUser = _userManager.GetUserAsync(Context.User).Result;
        var receiverUser = _context.Users.FirstOrDefault(m => m.CC == receiverCC);
        SendNotifyProposal(receiverUser.Id, "Você recebeu uma nova proposta de compra.");

    }

    public void SendNotifyProposal(string userId, string message)
    {
        Clients.User(userId).SendAsync("ReceiveProposalNotification", message).Wait();
    }

    public void MarkPropostaAsViewed(string receiverCC)
    {
        var propostasNotViewed = _context.PropostasDeCompra
            .Where(p => (p.CC_vendedor == receiverCC && !p.Proposta_vista))
            .ToList();

        foreach (var proposta in propostasNotViewed)
        {
            proposta.Proposta_vista = true;
        }

        _context.SaveChangesAsync().Wait();
    }
}
