using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class PropostasDeComprasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _imageFolder;
        private readonly string _imageFolder1;
        private readonly IWebHostEnvironment _appEnvironment;

        public PropostasDeComprasController(IWebHostEnvironment appEnvironment, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
            _imageFolder = Path.Combine(_appEnvironment.WebRootPath, "Imagens\\AnunciosPorValidar");
            _imageFolder1 = Path.Combine(_appEnvironment.WebRootPath, "Imagens\\AnunciosEnviado");
        }

        public async Task<IActionResult> IndexPorEnviar()
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
                var receiverCC = user.CC;
                var countPropostasNaoVistas = await _context.PropostasDeCompra
                    .Where(p => p.CC_vendedor == receiverCC && !p.Proposta_vista)
                    .CountAsync();
                var countPropostasNaoAceites = await _context.PropostasDeCompra
                    .Where(p => p.CC_vendedor == receiverCC && !p.Proposta_Aceite)
                    .CountAsync();
                ViewData["CountPropostasNaoVistas"] = countPropostasNaoAceites;
                return View(await _context.PropostasDeCompra.ToListAsync());
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AceitarPropostaValidar(int propostaId)
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
                var proposta = await _context.PropostasDeCompra.FindAsync(propostaId);

                if (proposta == null)
                {
                    return NotFound();
                }

                proposta.Venda_Concluida = true;

                await _context.SaveChangesAsync();

              
                _toastNotification.AddSuccessToastMessage("Encomenda Validada!");
                return RedirectToAction("IndexAdmin", "IndexPropostasValidar");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
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
                var receiverCC = user.CC;
                var countPropostasNaoVistas = await _context.PropostasDeCompra
                    .Where(p => p.CC_vendedor == receiverCC && !p.Proposta_vista)
                    .CountAsync();
                var countPropostasNaoAceites = await _context.PropostasDeCompra
                    .Where(p => p.CC_vendedor == receiverCC && !p.Proposta_Aceite)
                    .CountAsync();
                ViewData["CountPropostasNaoVistas"] = countPropostasNaoAceites;
                return View(await _context.PropostasDeCompra.ToListAsync());
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> EnviarValidacaoEncomenda(int id_anuncio)
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
                var anunciosPublicados = await _context.Anuncios
                    .Where(a => a.Id_anuncio == id_anuncio)
                    .ToListAsync();

                return View(anunciosPublicados);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> VeerPublicador()
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
                var anunciosPublicados = await _context.Anuncios
                    .Where(a => a.UserId == user.Id && a.Venda_Concluida == false)
                    .ToListAsync();

                var propostaAnuncioViewModels = anunciosPublicados.Select(anuncio => new PropostaAnuncioViewModel
                {
                    Anuncio = anuncio,
                    Proposta = null
                }).ToList();

                return View(propostaAnuncioViewModels);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> VerAnunciosPublicados(string userId)
        {

            var userlogin = await _userManager.GetUserAsync(User);
          
            if (userlogin != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == userlogin.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }
            if (userlogin != null)
            {
                var user1 = await _userManager.GetUserAsync(User);
                if (user1 != null)
                {
                    if (user1.Id == userId)
                    {
                        var propostasEAnuncios = await _context.PropostasDeCompra
                            .Join(
                                _context.Anuncios,
                                proposta => proposta.Id_Anuncio,
                                anuncio => anuncio.Id_anuncio,
                                (proposta, anuncio) => new PropostaAnuncioViewModel { Proposta = proposta, Anuncio = anuncio }
                            )
                            .ToListAsync();
                        return View("VeerPublicador", propostasEAnuncios);
                    }
                }

                var anunciosPublicados = await _context.Anuncios
                    .Where(a => a.UserId == userId && a.Venda_Concluida == false)
                    .ToListAsync();
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    ViewBag.AnunciadorNome = user.Nome;
                }
                return View(anunciosPublicados);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> EnviarValidacao(int id_anuncio)
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
            var userlogin = await _userManager.GetUserAsync(User);
            if (userlogin != null)
            {
                var anunciosPublicados = await _context.Anuncios
                    .Where(a => a.Id_anuncio == id_anuncio)
                    .ToListAsync();

                return View(anunciosPublicados);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        private string SaveCapturedImage(string base64ImageData)
        {
            var base64DataOnly = base64ImageData.Split(',')[1];

            byte[] bytes = Convert.FromBase64String(base64DataOnly);

            string uniqueFileName = Guid.NewGuid().ToString() + ".png";

            string filePath = Path.Combine(_imageFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            return uniqueFileName;
        }

        private string SaveCapturedImage1(string base64ImageData)
        {
            var base64DataOnly = base64ImageData.Split(',')[1];

            byte[] bytes = Convert.FromBase64String(base64DataOnly);

            string uniqueFileName = Guid.NewGuid().ToString() + ".png";

            string filePath = Path.Combine(_imageFolder1, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            return uniqueFileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateValidacao([Bind("Id_proposta,Id_anuncio, CC_Comrador, Imagem_destaque2, Imagem_destaque3")] AnuncioValidado AnuncioValidado)
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
                var anuncioValidadoUpdate = await _context.AnuncioValidado
                    .FirstOrDefaultAsync(p => p.Id_anuncio == AnuncioValidado.Id_anuncio && p.Id_proposta == AnuncioValidado.Id_proposta);

                if (AnuncioValidado.CC_Comrador == user.CC)
                {
                    var proposta = await _context.PropostasDeCompra
                        .FirstOrDefaultAsync(p => p.Id_Anuncio == AnuncioValidado.Id_anuncio);

                    if (proposta != null)
                    {
                        if (!string.IsNullOrEmpty(AnuncioValidado.Imagem_destaque2))
                        {
                            string imagePath = SaveCapturedImage1(AnuncioValidado.Imagem_destaque2);
                            anuncioValidadoUpdate.Imagem_destaque2 = imagePath;
                        }

                        if (!string.IsNullOrEmpty(AnuncioValidado.Imagem_destaque3))
                        {
                            string imagePath1 = SaveCapturedImage1(AnuncioValidado.Imagem_destaque3);
                            anuncioValidadoUpdate.Imagem_destaque3 = imagePath1;
                        }
                        proposta.Venda_Validada = true;
                        proposta.Produto_recebido = true;
                        anuncioValidadoUpdate.Recebido = true;
                        await _context.SaveChangesAsync();

                        _toastNotification.AddInfoToastMessage("Confirmação da entrega enviada!");
                    }

                    return RedirectToAction("VerificarPropVista", "Home");
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Não pode enviar a confirmação da entrega enviada!");
                    return RedirectToAction("VerificarPropVista", "Home");
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
        public async Task<IActionResult> CreateValidacaoEnviado([Bind("Id_proposta,Id_anuncio, CC_Comrador, Imagem_destaque, Imagem_destaque1")] AnuncioValidado AnuncioValidado)
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
                AnuncioValidado.CC_Vendedor = user.CC;

                if (AnuncioValidado.CC_Vendedor == user.CC)
                {
                    var proposta = await _context.PropostasDeCompra
                        .FirstOrDefaultAsync(p => p.Id_Anuncio == AnuncioValidado.Id_anuncio);

                    if (proposta != null)
                    {
                        if (!string.IsNullOrEmpty(AnuncioValidado.Imagem_destaque))
                        {
                            string imagePath = SaveCapturedImage(AnuncioValidado.Imagem_destaque);
                            AnuncioValidado.Imagem_destaque = imagePath;
                        }

                        if (!string.IsNullOrEmpty(AnuncioValidado.Imagem_destaque1))
                        {
                            string imagePath1 = SaveCapturedImage(AnuncioValidado.Imagem_destaque1);
                            AnuncioValidado.Imagem_destaque1 = imagePath1;
                        }

                        proposta.Produto_recebido = false;
                        proposta.Produto_enviado = true;
                        await _context.SaveChangesAsync();
                        AnuncioValidado.Recebido = false;
                        _context.Add(AnuncioValidado);
                        await _context.SaveChangesAsync();
                        var userComprador = await _context.Users.FirstOrDefaultAsync(p=> p.CC == proposta.CC_comprador);
                        _toastNotification.AddInfoToastMessage("Confirmação do envio da encomenda enviada!");
                        if(userComprador != null)
                        {
                            string mensagem = $"Prezado(a) {userComprador.Nome},\n\nO produto foi enviado com sucesso! Quando receber o produto deverá fazer o processo de validação da entrega:\n\n" +
                                                     $"\n\nAtenciosamente,\nEquipe Tradeguard";

                            // Assunto do email
                            string assunto = "O vendedor já enviou o Produto - Tradeguard";
                          await  EnviarEmail(userComprador.Email,mensagem,assunto);
                        }
                        
                    }

                    return RedirectToAction("VerificarPropVista", "Home");
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Não pode enviar a confirmação da entrega enviada!");
                    return RedirectToAction("VerificarPropVista", "Home");
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
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


        public async Task<IActionResult> ValidarEncomenda()
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
            var userlogin = await _userManager.GetUserAsync(User);
            if (userlogin != null)
            {
                return View();
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecusarProposta(int propostaId)
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
            var userlogin = await _userManager.GetUserAsync(User);
            if (userlogin != null)
            {
                var proposta = await _context.PropostasDeCompra.FindAsync(propostaId);

                if (proposta == null)
                {
                    return NotFound();
                }

                _context.PropostasDeCompra.Remove(proposta);
                await _context.SaveChangesAsync();
                _toastNotification.AddInfoToastMessage("Proposta Recusada");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost]
        private async Task AddToMovimentosDaCarteira(PropostasDeCompra proposta,string tipo,int ID_proposta)
        {
            try
            {
                
                var user = await _userManager.GetUserAsync(User);
                if(tipo == "Dinheiro Retirado")
                {
                    var movimento = await _context.MovimentosDaCarteira.FirstOrDefaultAsync(p=>p.Id_proposta == ID_proposta);
                    var proposta1 = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.Id_Proposta == ID_proposta);
                    if (proposta1 != null)
                    {
                        var user123 = await _context.Users.FirstOrDefaultAsync(p=>p.CC == proposta1.CC_vendedor);
                        if (user123 != null)
                        {
                            user123.Saldo += Convert.ToDecimal(proposta.Preco_proposta);
                            await _context.SaveChangesAsync();
                        }
                      
                    }
                    if (movimento != null)
                    {
                        movimento.Tipo = "Dinheiro Retirado";
                        await _context.SaveChangesAsync();
                    }
                }
                else if(tipo == "Dinheiro Pendente"){
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
              
            }
            catch (Exception)
            {
                // Handle exceptions if necessary
            }
        }

        [HttpPost]
        public async Task<IActionResult> AceitarProposta(int propostaId)
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
            var userlogin = await _userManager.GetUserAsync(User);
            if (userlogin != null)
            {
                var proposta = await _context.PropostasDeCompra.FindAsync(propostaId);

                if (proposta == null)
                {
                    return NotFound();
                }

                proposta.Proposta_Aceite = true;
                proposta.Data_aceite = DateTime.Now;
                proposta.Data_Final = proposta.Data_aceite.AddDays(5);
                proposta.Dinheiro_pendente = true;

                await _context.SaveChangesAsync();               
                var outrasPropostas = _context.PropostasDeCompra.Where(p => p.Id_Anuncio == proposta.Id_Anuncio && p.Id_Proposta != propostaId && p.Proposta_Aceite == false);
                _context.PropostasDeCompra.RemoveRange(outrasPropostas);

                await _context.SaveChangesAsync();

                await AddToMovimentosDaCarteira(proposta,"Dinheiro Pendente",proposta.Id_Proposta);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }


        static bool EstaEntre75e100Porcento(double valor, double valorX)
        {
            double limiteInferior = valorX * 0.75; // 75% de valorX
            double limiteSuperior = valorX; // 100% de valorX

            return valor >= limiteInferior && valor <= limiteSuperior;
        }
        // POST: PropostasDeCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proposta,Id_Anuncio,CC_vendedor,CC_comprador,Preco_proposta,Data_enviada")] PropostasDeCompra propostasDeCompra)
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
                var utilizador = _context.Users.FirstOrDefault(p => p.Id == user.Id);
                if (!double.TryParse(propostasDeCompra.Preco_proposta, out double result))
                {
                    _toastNotification.AddErrorToastMessage("O valor escrito Incorreto");
                    return RedirectToAction("index", "Home");
                }

                if (user.Saldo < Convert.ToInt32(propostasDeCompra.Preco_proposta))
                {
                    _toastNotification.AddInfoToastMessage("Não pode enviar a proposta pois não tem saldo suficiente");
                    return RedirectToAction("Gerir_Carteira", "User");
                }
                else
                {
                    var proposta = _context.Anuncios.FirstOrDefault(p=>p.Id_anuncio == propostasDeCompra.Id_Anuncio);
                    bool entre75e100 = EstaEntre75e100Porcento(Convert.ToDouble(propostasDeCompra.Preco_proposta), Convert.ToDouble(proposta.Preco));
                    if (entre75e100 == false)
                    {
                        _toastNotification.AddInfoToastMessage("O valor da proposta tem de estar entre 75% e 100% do valor do anuncio");
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        if (propostasDeCompra.CC_vendedor != user.CC)
                        {
                            propostasDeCompra.Data_enviada = DateTime.Now;
                            propostasDeCompra.Proposta_vista = false;
                            if (ModelState.IsValid)
                            {
                                if (!string.IsNullOrEmpty(propostasDeCompra.Preco_proposta))
                                {
                                    if (Convert.ToDecimal(propostasDeCompra.Preco_proposta) <= utilizador.Saldo)
                                    {

                                    }
                                    else
                                    {
                                        _toastNotification.AddWarningToastMessage("O seu saldo não permite enviar a proposta!");
                                        return RedirectToAction("Inserir_Dinheiro", "User");
                                    }

                                }
                                else
                                {

                                }
                                _context.Add(propostasDeCompra);
                                await _context.SaveChangesAsync();
                                _toastNotification.AddInfoToastMessage("Proposta enviada!");
                                return RedirectToAction("Index", "Home");
                            }

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            _toastNotification.AddInfoToastMessage("Não pode enviar Propostas ao seu anúncio!");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                   
                }

            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propostasDeCompra = await _context.PropostasDeCompra
                .FirstOrDefaultAsync(m => m.Id_Proposta == id);
            if (propostasDeCompra == null)
            {
                return NotFound();
            }

            return View(propostasDeCompra);
        }

        // POST: PropostasDeCompras/Delete/5
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
            var userlogin = await _userManager.GetUserAsync(User);
            if (userlogin != null)
            {
                var propostasDeCompra = await _context.PropostasDeCompra.FindAsync(id);
                if (propostasDeCompra != null)
                {
                    _context.PropostasDeCompra.Remove(propostasDeCompra);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

        }

        private bool PropostasDeCompraExists(int id)
        {
            return _context.PropostasDeCompra.Any(e => e.Id_Proposta == id);
        }
    }
}
