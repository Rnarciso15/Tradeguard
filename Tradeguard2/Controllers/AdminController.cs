
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Immutable;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using Tradeguard2.Data;
using Tradeguard2.Models;
using System.Configuration;
using Microsoft.CodeAnalysis.Operations;
using iText.Commons.Actions.Contexts;



namespace Tradeguard2.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly SmtpClient _smtpClient;

        private readonly IWebHostEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IWebHostEnvironment appEnvironment ,IToastNotification toastNotification, ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _toastNotification = toastNotification;
          _appEnvironment = appEnvironment;
        }
        public IActionResult PaginaRegistro()
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }

        public async Task EnviarEmail(string emailrecebe, string mensagem, string assunto)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(emailrecebe);
            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.From = new MailAddress(emailrecebe);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("tradeguardsuporte@gmail.com", "ojlh jgel shpj pbtr");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch
            {

            }
        }

        public async Task<IActionResult> IndexAdminAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {

                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    return View();
                }
                else
                {
                    
                    return RedirectToAction("Index", "Home");
                }
            }
           

            
        }
        public async Task<IActionResult> ValidarAnuncioAdminAsync(int Id_anuncio, string UserID)
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
            if (user1 == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    var anuncios = await _context.Anuncios
                     .Where(m => m.Id_anuncio == Id_anuncio && m.UserId == UserID)
                     .ToListAsync();


                    var imagensAnuncio = new List<Imagens_Anuncios>();
                    foreach (var anuncio in anuncios)
                    {
                        var imagens = await _context.Imagens_Anuncios
                            .Where(m => m.Id_Anuncio == anuncio.Id_anuncio)
                            .ToListAsync();
                        imagensAnuncio.AddRange(imagens);
                    }


                    var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == UserID);


                    if (user != null)
                    {

                        var anunciosvalidados = await _context.AnuncioValidado
                            .Where(p => p.CC_Vendedor == user.CC && p.Id_anuncio == Id_anuncio)
                            .ToListAsync();




                        // Passar os dados para a visualizaçã
                        var viewModel = new AnunciosImagensViewModel
                        {
                            Anuncios = anuncios,
                            ImagensAnuncio = imagensAnuncio,
                            AnuncioValidado = anunciosvalidados // Adicione isso ao modelo de visualização
                        };

                        return View(viewModel); // Passar o modelo de visualização para a view
                    }

                    // Se o usuário não for encontrado, retorne uma visualização vazia ou outra ação adequada
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }

            }

        }

        public async Task<IActionResult> ValidarDenunciaAdmin(int Id_anuncio, string UserID)
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
            if (user1 == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    var anuncios = await _context.Anuncios
                     .Where(m => m.Id_anuncio == Id_anuncio && m.UserId == UserID)
                     .ToListAsync();


                    var imagensAnuncio = new List<Imagens_Anuncios>();
                    foreach (var anuncio in anuncios)
                    {
                        var imagens = await _context.Imagens_Anuncios
                            .Where(m => m.Id_Anuncio == anuncio.Id_anuncio)
                            .ToListAsync();
                        imagensAnuncio.AddRange(imagens);
                    }


                    var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == UserID);


                    if (user != null)
                    {

                        var anunciosvalidados = await _context.AnuncioValidado
                            .Where(p => p.CC_Vendedor == user.CC && p.Id_anuncio == Id_anuncio)
                            .ToListAsync();

                        var denuncias = await _context.Denuncias.Where(p => p.Id_Anuncio == Id_anuncio).ToListAsync();


                        // Passar os dados para a visualizaçã
                        var viewModel = new DenunciasAnunciosImagensViewModel
                        {
                            Denuncias = denuncias,
                            Anuncios = anuncios,
                            ImagensAnuncio = imagensAnuncio,
                            AnuncioValidado = anunciosvalidados // Adicione isso ao modelo de visualização
                        };

                        return View(viewModel); // Passar o modelo de visualização para a view
                    }

                    // Se o usuário não for encontrado, retorne uma visualização vazia ou outra ação adequada
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            
            }
             
        }
        [HttpPost]
        private async Task AddToMovimentosDaCarteira(PropostasDeCompra proposta, string tipo, int ID_proposta)
        {
            try
            {

                var user = await _userManager.GetUserAsync(User);
                if (tipo == "Dinheiro Retirado")
                {
                    var movimento = await _context.MovimentosDaCarteira.FirstOrDefaultAsync(p => p.Id_proposta == ID_proposta);
                    var proposta1 = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == ID_proposta);
                    if (proposta1 != null)
                    {
                        var user123 = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta1.CC_vendedor);
                        if (user123 != null)
                        {
                            user123.Saldo += Convert.ToDecimal(proposta.Preco_proposta);
                            var movimentoAdicionado = new MovimentosDaCarteira
                            {
                                Tipo = "Dinheiro Adicionado",
                                Quantia = Convert.ToInt32(proposta.Preco_proposta),
                                Data = DateTime.Now,
                                UserId = user123.Id,
                                Id_proposta = ID_proposta
                            };
                            _context.Add(movimentoAdicionado);
                            await _context.SaveChangesAsync();
                        }

                    }
                    if (movimento != null)
                    {
                        movimento.Tipo = "Dinheiro Retirado";
                        await _context.SaveChangesAsync();
                    }
                }
                else if (tipo == "Dinheiro Pendente")
                {
                    var userpropsota = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == ID_proposta);
                    var user123 = await _context.Users.FirstOrDefaultAsync(p => p.CC == userpropsota.CC_comprador);
                    var movimento = new MovimentosDaCarteira
                    {
                        Tipo = tipo,
                        Quantia = Convert.ToInt32(proposta.Preco_proposta),
                        Data = DateTime.Now,
                        UserId = user123.Id,
                        Id_proposta = ID_proposta
                    };
                    var userCCCopmprador = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_comprador);
                    var userCCvendedor = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_vendedor);
                    userCCCopmprador.Saldo -= Convert.ToDecimal(proposta.Preco_proposta);
                    _context.Add(movimento);
                    await _context.SaveChangesAsync();
                }
                else if(tipo== "Dinheiro Devolvido")
                {
                    var userpropsota = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == ID_proposta);
                    var user123 = await _context.Users.FirstOrDefaultAsync(p => p.CC == userpropsota.CC_comprador);
                    var movimento = new MovimentosDaCarteira
                    {
                        Tipo = tipo,
                        Quantia = Convert.ToInt32(proposta.Preco_proposta),
                        Data = DateTime.Now,
                        UserId = user123.Id,
                        Id_proposta = ID_proposta
                    };
                    var userCCCopmprador = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_comprador);
                    var userCCvendedor = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_vendedor);
                    userCCCopmprador.Saldo += Convert.ToDecimal(proposta.Preco_proposta);
                    _context.Add(movimento);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                // Handle exceptions if necessary
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidarDenuncia(Denuncias denuncias, string SubmitButton, string id_anuncio)
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
            if (user1 == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {

                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    if (SubmitButton == "nao remover")
                    {
                      
                        var denuncia = await _context.Denuncias.FirstOrDefaultAsync(p => p.Id_Anuncio == Convert.ToInt32(id_anuncio));

                        if (denuncia != null)
                        {

                            _context.Denuncias.Remove(denuncia);
                            await _context.SaveChangesAsync();
                            var user = await _context.Users.FirstOrDefaultAsync(p => p.CC == denuncia.CC_anunciador);
                            var mensagemDenunciaNaoAceite = "Caro " + user.Nome + ",\r\n\r\nGostaríamos de informar que a denúncia feita em relação ao seu anúncio no nosso site TradeGuard não foi aceita. Isso significa que o seu anúncio continua ativo e visível para os nossos utiizadores.\r\n\r\nAgradecemos por escolher o TradeGuard para suas transações e desejamos-lhe sucesso contínuo em suas atividades de venda.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                            var assuntoDenunciaNaoAceite = "Denúncia não Aceita - Seu Anúncio Permanece Ativo";


                            await EnviarEmail(user.Email, mensagemDenunciaNaoAceite, assuntoDenunciaNaoAceite);
                        }
                        user1.N_Validados += 1;
                        await _context.SaveChangesAsync();
                    }
                    else if (SubmitButton == "remover")
                    {
                        var denuncia = await _context.Denuncias.FirstOrDefaultAsync(p => p.Id_Anuncio == Convert.ToInt32(id_anuncio));

                        if (denuncia != null)
                        {

                            _context.Denuncias.Remove(denuncia);
                            await _context.SaveChangesAsync();
                        }

                        var anuncios = await _context.Anuncios.Where(i => i.Id_anuncio == Convert.ToInt32(id_anuncio)).ToListAsync();
                        if (anuncios != null && anuncios.Any())
                        {
                            foreach (var anuncio in anuncios)
                            {
                                var imagePath = Path.Combine(_appEnvironment.WebRootPath, "Imagens/AnunciosEnviado", anuncio.Imagem_destaque);

                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }

                            var propostas = await _context.PropostasDeCompra.Where(p => p.Id_Anuncio == Convert.ToInt32(id_anuncio)).ToListAsync();
                            var favoritos = await _context.Favoritos.Where(f => f.Id_Anuncio == Convert.ToInt32(id_anuncio)).ToListAsync();
                            var imagens = await _context.Imagens_Anuncios.Where(i => i.Id_Anuncio == Convert.ToInt32(id_anuncio)).ToListAsync();

                            foreach (var imagem in imagens)
                            {
                                var imagePath = Path.Combine(_appEnvironment.WebRootPath, "Imagens/Anuncios", imagem.Imagem);

                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                            }

                            if (favoritos != null && favoritos.Any())
                            {
                                _context.Favoritos.RemoveRange(favoritos);
                            }

                            if (imagens != null && imagens.Any())
                            {
                                _context.Imagens_Anuncios.RemoveRange(imagens);
                            }

                            if (propostas != null && propostas.Any())
                            {
                                _context.PropostasDeCompra.RemoveRange(propostas);
                            }

                            if (anuncios != null)
                            {
                                _context.Anuncios.RemoveRange(anuncios);
                                _toastNotification.AddInfoToastMessage("Anúncio removido");
                                await _context.SaveChangesAsync();
                            }

                            user1.N_Validados += 1;
                            await _context.SaveChangesAsync();
                            var user = await _context.Users.FirstOrDefaultAsync(p => p.CC == denuncia.CC_anunciador);
                            var mensagemDenunciaAceite = "Caro " + user.Nome + ",\r\n\r\nLamentamos informar que o seu anúncio no nosso site TradeGuard foi removido devido a uma denúncia que foi aceita pela nossa equipe de moderação. Entendemos que isso possa ser decepcionante, mas é importante manter a integridade e a segurança da nossa plataforma para todos os utilizadores.\r\n\r\nAgradecemos por sua compreensão e cooperação.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                            var assuntoDenunciaAceite = "Anúncio Removido devido a Denúncia Aceita";


                            await EnviarEmail(user.Email, mensagemDenunciaAceite, assuntoDenunciaAceite);



                        }
                        return RedirectToAction("IndexDenuncias", "Admin");
                    }
                  

                }
               
                    return RedirectToAction("Index", "Home");
                


            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidarValidacao(PropostasDeCompra propostasDeCompra, Denuncias denuncias, string SubmitButton, string propostaId)
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
            if (user1 == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {

                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    if (SubmitButton == "aceitar")
                    {
                        var proposta = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == Convert.ToUInt32(propostaId));
                        var userComprador = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_comprador);
                        var uservendedor = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_vendedor);
                        var anuncioVendido = await _context.Anuncios.FirstOrDefaultAsync(p => p.Id_anuncio == proposta.Id_Anuncio);

                        var mensagemVenda = "Caro " + uservendedor.Nome + ",\r\n\r\nGostaríamos de informar que a sua venda foi concluída com sucesso no nosso site TradeGuard. Parabéns pela venda bem-sucedida!\r\n\r\nDetalhes da venda:\r\n- Produto Vendido: " + anuncioVendido.Titulo + "\r\n- Valor da Venda: " + proposta.Preco_proposta + "\r\n- Comprador: " + userComprador.Nome + "\r\n\r\n Obrigado por usar o nosso site e desejamos-lhe sucesso contínuo nas suas futuras vendas.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                        var assuntoVenda = "Confirmação de Venda Efetuada";

                        await EnviarEmail(uservendedor.Email, mensagemVenda, assuntoVenda);


                        var mensagemVendaComprador = "Caro " + userComprador.Nome + ",\r\n\r\nGostaríamos de informar que a sua compra foi concluída com sucesso no nosso site TradeGuard. Parabéns pela compra bem-sucedida!\r\n\r\nDetalhes da Compra:\r\n- Produto Comprado: " + anuncioVendido.Titulo + "\r\n- Valor da Compra: " + proposta.Preco_proposta + "\r\n- Vendedor: " + uservendedor.Nome + "\r\n\r\n Obrigado por usar o nosso site e desejamos-lhe sucesso contínuo nas suas futuras compras.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                        var assuntoVendaComprador = "Confirmação de Compra Efetuada";

                        await EnviarEmail(userComprador.Email, mensagemVendaComprador, assuntoVendaComprador);


                        anuncioVendido.Venda_Concluida = true;
                        proposta.Venda_Concluida = true;
                        proposta.Venda_Validada = true;
                        await _context.SaveChangesAsync();
                        await AddToMovimentosDaCarteira(proposta, "Dinheiro Retirado", proposta.Id_Proposta);
                        _toastNotification.AddSuccessToastMessage("Encomenda Validada!");
                        return RedirectToAction("IndexPropostasValidar", "Admin");
                    }
                    else if(SubmitButton == "Recusar")
                    {
                        var proposta = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == Convert.ToUInt32(propostaId));
                        var userComprador = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_comprador);
                        var uservendedor = await _context.Users.FirstOrDefaultAsync(p => p.CC == proposta.CC_vendedor);
                        var anuncioVendido = await _context.Anuncios.FirstOrDefaultAsync(p => p.Id_anuncio == proposta.Id_Anuncio);
                        var mensagemVendaCancelada = "Caro " + uservendedor.Nome + ",\r\n\r\nGostaríamos de informar que a sua venda do produto '" + anuncioVendido.Titulo + "' no nosso site TradeGuard não pôde ser concluída e o valor da venda foi reembolsado ao comprador.\r\n\r\nPedimos desculpas pelo inconveniente.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                        var assuntoVendaCancelada = "Venda Cancelada - Reembolso Efetuado";
                        var imagens = await _context.AnuncioValidado.FirstOrDefaultAsync(p => p.Id_proposta == Convert.ToUInt32(propostaId));
                        await EnviarEmail(uservendedor.Email, mensagemVendaCancelada, assuntoVendaCancelada);



                        var mensagemCompraCancelada = "Caro " + userComprador.Nome + ",\r\n\r\nGostaríamos de informar que a sua compra do produto '" + anuncioVendido.Titulo + "' no nosso site TradeGuard foi cancelada e o valor da compra foi reembolsado.\r\n\r\nPedimos desculpas pelo inconveniente.\r\n\r\nAtenciosamente,\r\nTradeGuard";
                        var assuntoCompraCancelada = "Compra Cancelada - Reembolso Efetuado";

                        await EnviarEmail(userComprador.Email, mensagemCompraCancelada, assuntoCompraCancelada);
                        await AddToMovimentosDaCarteira(proposta, "Dinheiro Devolvido", proposta.Id_Proposta);
                        if(imagens != null)
                        {
                            _context.Remove(imagens);
                        }
                        if(proposta != null)
                        {
                            _context.Remove(proposta);
                        }
                        await _context.SaveChangesAsync();
                        _toastNotification.AddSuccessToastMessage("Encomenda Validada!");

                    }
                    return RedirectToAction("IndexPropostasValidar","Admin");
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
               
            }
              
           
            
        }
        public async Task<IActionResult> Lista_Moderadores()
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if ((User.IsInRole("Administrador") || User.IsInRole("Moderador")))
                {
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }


        }
        public async Task<IActionResult> IndexDenuncias()
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
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    var userLoginId = await _userManager.GetUserAsync(User);
                    var userIdsInRole = await _userManager.GetUsersInRoleAsync("Utilizador");
                    var userIds = userIdsInRole.Select(u => u.Id).ToList();

                    var anuncios = await _context.Anuncios
                        .Where(a => userIds.Contains(a.UserId) && a.Venda_Concluida == false)
                        .ToListAsync();

                    var ccList = await _context.Users
                        .Where(u => userIds.Contains(u.Id))
                        .Select(u => u.CC)
                        .ToListAsync();

                    var denuncias = await _context.Denuncias
                        .Where(d => ccList.Contains(d.CC_anunciador) && ccList.Contains(d.CC_denunciador))
                        .ToListAsync();

                    var resultado =  anuncios
                        .Join(denuncias,
                            anuncio => anuncio.Id_anuncio,
                            denuncia => denuncia.Id_Anuncio,
                            (anuncio, denuncia) => new DenunciasAnuncioViewModel
                            {
                                Denuncias = denuncia,
                                Anuncio = anuncio
                            })
                        .ToList();


                    return View(resultado);
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }


        }
        public async Task<IActionResult> Lista_UsersAsync()
        {
            var user1 = await _userManager.GetUserAsync(User);

            if (user1 != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    return View();
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }
              

        }
        public async Task<IActionResult> IndexPropostasValidar()
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
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    var userLoginId = await _userManager.GetUserAsync(User);

                    var anuncios = await _context.Anuncios
                        .Where(a => a.UserId != userLoginId.Id)
                        .ToListAsync();

                    var propostaAnuncio = await _context.PropostasDeCompra
                        .Where(a => a.CC_comprador != userLoginId.CC && a.CC_vendedor != userLoginId.CC && a.Proposta_Aceite == true && a.Produto_recebido == true && a.Venda_Validada == true && a.Venda_Concluida == false)
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
                        .ToList();

                    return View(resultado);
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }

           
        }

        public async Task<IActionResult> GetPropostasByEstadoAsync(string estado)
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
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    List<PropostasDeCompra> propostas;

                    if (estado == "Todos")
                    {
                        propostas = _context.PropostasDeCompra.ToList();
                    }
                    else if (estado == "Concluídas")
                    {
                        propostas = _context.PropostasDeCompra.Where(p => p.Venda_Concluida).ToList();
                    }
                    else if (estado == "Validadas")
                    {
                        propostas = _context.PropostasDeCompra.Where(p => p.Venda_Validada && !p.Venda_Concluida).ToList();
                    }
                    else if (estado == "Por Validar")
                    {
                        propostas = _context.PropostasDeCompra.Where(p => p.Produto_recebido && !p.Venda_Validada && !p.Venda_Concluida).ToList();
                    }
                    else if (estado == "Dinheiro Pendente")
                    {
                        propostas = _context.PropostasDeCompra.Where(p => p.Proposta_Aceite && !p.Produto_recebido && !p.Venda_Validada && !p.Venda_Concluida).ToList();
                    }
                    else
                    {
                        propostas = new List<PropostasDeCompra>();
                    }

                    if (propostas.Count == 0)
                    {
                        ViewData["Mensagem"] = "Não há propostas de anúncios disponíveis.";
                    }

                    return Json(propostas);
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }
          

        }
        [HttpGet]
        public async Task<IActionResult> GetPropostasByInputAsync(string searchTerm, string estado)
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
            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    List<PropostasDeCompra> propostas;

                    if (estado == "Todos")
                    {
                        propostas = await _context.PropostasDeCompra
                            .Where(p =>
                                EF.Functions.Like(p.Id_Proposta.ToString(), "%" + searchTerm + "%") ||
                                EF.Functions.Like(p.Id_Anuncio.ToString(), "%" + searchTerm + "%"))
                            .ToListAsync();
                    }
                    else if (estado == "Concluídas")
                    {
                        propostas = await _context.PropostasDeCompra
                            .Where(p =>
                                (EF.Functions.Like(p.Id_Proposta.ToString(), "%" + searchTerm + "%") ||
                                EF.Functions.Like(p.Id_Anuncio.ToString(), "%" + searchTerm + "%")) &&
                                p.Venda_Concluida)
                            .ToListAsync();
                    }
                    else if (estado == "Validadas")
                    {
                        propostas = await _context.PropostasDeCompra
                            .Where(p =>
                                (EF.Functions.Like(p.Id_Proposta.ToString(), "%" + searchTerm + "%") ||
                                EF.Functions.Like(p.Id_Anuncio.ToString(), "%" + searchTerm + "%")) && p.Venda_Validada && !p.Venda_Concluida)
                            .ToListAsync();

                    }
                    else if (estado == "Por Validar")
                    {

                        propostas = await _context.PropostasDeCompra
             .Where(p =>
                 (EF.Functions.Like(p.Id_Proposta.ToString(), "%" + searchTerm + "%") ||
                 EF.Functions.Like(p.Id_Anuncio.ToString(), "%" + searchTerm + "%")) && p.Produto_recebido && !p.Venda_Validada && !p.Venda_Concluida)
             .ToListAsync();
                    }
                    else if (estado == "Dinheiro Pendente")
                    {


                        propostas = await _context.PropostasDeCompra
                      .Where(p =>
                          (EF.Functions.Like(p.Id_Proposta.ToString(), "%" + searchTerm + "%") ||
                          EF.Functions.Like(p.Id_Anuncio.ToString(), "%" + searchTerm + "%")) && p.Proposta_Aceite && !p.Produto_recebido && !p.Venda_Validada && !p.Venda_Concluida)
                      .ToListAsync();
                    }
                    else
                    {
                        propostas = new List<PropostasDeCompra>();
                    }

                    return Json(propostas);
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }

         
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers(string searchTerm, string Tipo)
        {
            try
            {
                var user1 = await _userManager.GetUserAsync(User);

                if (user1 == null)
                {
                    _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }

                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    IQueryable<ApplicationUser> usersQuery = _context.Users;

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        usersQuery = usersQuery.Where(u =>
                            EF.Functions.Like(u.CC, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Nome, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Telemovel, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Email, "%" + searchTerm + "%"));
                    }

                    if (Tipo == "Melhores Avaliados")
                    {
                        usersQuery = usersQuery.OrderByDescending(u => u.Avaliacao);
                    }
                    else if (Tipo == "Piores Avaliados")
                    {
                        usersQuery = usersQuery.OrderBy(u => u.Avaliacao);
                    }
                    else if (Tipo == "Mais Vendas" || Tipo == "Menos Vendas")
                    {
                        var orderedUsers = await GetOrderedUsersBySales(_context, Tipo);
                        return Json(orderedUsers);
                    }
                    else
                    {
                        usersQuery = _context.Users;
                    }
                    var userIdsInRole = await _userManager.GetUsersInRoleAsync("Utilizador");
                    var userIds = userIdsInRole.Select(u => u.Id).ToList();

                    // Obter os usuários mapeados
                    var mappedUsers = await usersQuery
                     .Where(u => userIds.Contains(u.Id))
                     .Select(u => new UserDto
                     {
                         Id = u.Id,
                         CC = u.CC,
                         Nome = u.Nome,
                         Telemovel = u.Telemovel,
                         Email = u.Email,
                         Avaliacao = u.Avaliacao,
                         Saldo = u.Saldo
                     })
                     .ToListAsync();


                    // Aguardar a conclusão de todas as tarefas de obtenção de URL do avatar antes de retornar
                    foreach (var user in mappedUsers)
                    {
                        // Atribua o valor da URL do avatar apenas dentro do loop
                        user.AvatarUrl = await GetUserAvatarUrlAsync(_context, user.Id);
                    }


                    return Json(mappedUsers);


                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetModeradores(string searchTerm, string Tipo)
        {
            try
            {
                var user1 = await _userManager.GetUserAsync(User);

                if (user1 == null)
                {
                    _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }

                if (User.IsInRole("Administrador") || User.IsInRole("Moderador"))
                {
                    IQueryable<ApplicationUser> usersQuery = _context.Users;

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        usersQuery = usersQuery.Where(u =>
                            EF.Functions.Like(u.CC, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Nome, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Telemovel, "%" + searchTerm + "%") ||
                            EF.Functions.Like(u.Email, "%" + searchTerm + "%"));
                    }

                    if (Tipo == "Mais validações")
                    {
                        usersQuery = usersQuery.OrderByDescending(u => u.N_Validados);
                    }
                    else if (Tipo == "Menos validações")
                    {
                        usersQuery = usersQuery.OrderBy(u => u.N_Validados);
                    }
                    else
                    {
                        usersQuery = _context.Users;
                    }
                    var userIdsInRole = await _userManager.GetUsersInRoleAsync("Moderador");
                    var userIds = userIdsInRole.Select(u => u.Id).ToList();

                    // Obter os usuários mapeados
                    var mappedUsers = await usersQuery
                     .Where(u => userIds.Contains(u.Id))
                     .Select(u => new UserDto
                     {
                         Id = u.Id,
                         CC = u.CC,
                         Nome = u.Nome,
                         Telemovel = u.Telemovel,
                         Email = u.Email,
                         Avaliacao = u.Avaliacao,
                         Saldo = u.Saldo,
                         N_validados = u.N_Validados
                     })
                     .ToListAsync();


                    // Aguardar a conclusão de todas as tarefas de obtenção de URL do avatar antes de retornar
                    foreach (var user in mappedUsers)
                    {
                        // Atribua o valor da URL do avatar apenas dentro do loop
                        user.AvatarUrl = await GetUserAvatarUrlAsync(_context, user.Id);
                    }


                    return Json(mappedUsers);


                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json("");
            }
        }

        private async Task<List<object>> GetOrderedUsersBySales(ApplicationDbContext context, string tipo)
        {
            var propostas = await context.PropostasDeCompra.Where(p => p.Venda_Concluida).ToListAsync();
            var users = await context.Users.ToListAsync();
            var utilizadoresComPropostasConcluidas = new List<string>();
            var utilizadoresComN_Vendas = new Dictionary<string, int>();

            foreach (var utilizador in users)
            {
                var userCC = utilizador.CC;
                var eVendedor = propostas.Any(p => p.CC_vendedor == userCC);
                if (eVendedor)
                {
                    utilizadoresComPropostasConcluidas.Add(utilizador.CC);
                }
            }

            foreach (var utilizador1 in utilizadoresComPropostasConcluidas)
            {
                if (!utilizadoresComN_Vendas.ContainsKey(utilizador1))
                {
                    var N_Vendas = propostas.Count(p => p.CC_vendedor == utilizador1);
                    utilizadoresComN_Vendas.Add(utilizador1, N_Vendas);
                }
            }

            if (tipo == "Mais Vendas")
            {
                utilizadoresComN_Vendas = utilizadoresComN_Vendas.OrderByDescending(p => p.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
            }
            else // Menos Vendas
            {
                utilizadoresComN_Vendas = utilizadoresComN_Vendas.OrderBy(p => p.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
            }

            var orderedUsers = new List<object>();
            foreach (var utilizadorFinal in utilizadoresComN_Vendas)
            {
                var user1234 = await context.Users.FirstOrDefaultAsync(p => p.CC == utilizadorFinal.Key);
                if (user1234 != null)
                {
                    orderedUsers.Add(new
                    {
                        Id = user1234.Id,
                        CC = user1234.CC,
                        Nome = user1234.Nome,
                        Telemovel = user1234.Telemovel,
                        Email = user1234.Email,
                        Avaliacao = user1234.Avaliacao,
                        AvatarUrl = await GetUserAvatarUrlAsync(context, user1234.Id),
                        Saldo = user1234.Saldo
                    });
                }
            }

            return orderedUsers;
        }

        public static async Task<string> GetUserAvatarUrlAsync(ApplicationDbContext context, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(p => p.Id == userId);
            if (user != null && user.ProfilePicture != null && user.ProfilePicture.Length > 0)
            {
                var base64String = Convert.ToBase64String(user.ProfilePicture);
                return $"data:image/jpeg;base64,{base64String}";
            }

            return "/path/to/default/avatar.png";
        }



    }
}