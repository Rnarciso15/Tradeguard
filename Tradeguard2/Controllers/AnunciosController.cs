    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NToastNotify;
    using Tradeguard2.Data;
    using Tradeguard2.Models;
    using Tradeguard2.Data;
    using Tradeguard2.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Text.RegularExpressions;
    using X.PagedList;
    using Microsoft.AspNetCore.Components.Web;
using Tradeguard2.Helpers;
using System.Net.Mail;
using System.Net;


    namespace Tradeguard2.Controllers
    {
        public class AnunciosController : Controller 
        {
            private readonly ApplicationDbContext _context;
            private readonly IWebHostEnvironment _appEnvironment;
            private readonly IToastNotification _toastNotification;
            private readonly string _imageFolder;
            private readonly string _imageFolderDestaque;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<ApplicationUser> _userManager;
           bool result = false;
            bool resultDestaque = false;    
            public AnunciosController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IWebHostEnvironment appEnvironment, IToastNotification toastNotification, IHttpContextAccessor httpContextAccessor)
            {
                _userManager = userManager;
                _context = context;
                _appEnvironment = appEnvironment;
                _toastNotification = toastNotification;
                _imageFolderDestaque = Path.Combine(_appEnvironment.WebRootPath, "Imagens\\AnunciosEnviado");
                _imageFolder = Path.Combine(_appEnvironment.WebRootPath, "Imagens\\Anuncios");
                _httpContextAccessor = httpContextAccessor;
            }
            private List<string> UploadedFiles(Anuncios anuncios)
            {
                List<string> uniqueFileNames = new List<string>();

                if (anuncios.FicheiroImagem != null && anuncios.FicheiroImagem.Count > 0)
                {
                    System.IO.Directory.CreateDirectory(_imageFolder);

                    foreach (var file in anuncios.FicheiroImagem)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString()
                            + "_"
                            + System.IO.Path.GetFileName(file.FileName);

                        string filePath = Path.Combine(_imageFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        uniqueFileNames.Add(uniqueFileName);
                    }
                }

                return uniqueFileNames;
            }
            private List<string> UploadedFilesDestaque(Anuncios anuncios)
            {
                List<string> uniqueFileNames = new List<string>();

                if (anuncios.FicheiroImagemDestaque != null && anuncios.FicheiroImagemDestaque.Count > 0)
                {
                    System.IO.Directory.CreateDirectory(_imageFolderDestaque);

                    foreach (var file in anuncios.FicheiroImagemDestaque)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString()
                            + "_"
                            + System.IO.Path.GetFileName(file.FileName);

                        string filePath = Path.Combine(_imageFolderDestaque, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        uniqueFileNames.Add(uniqueFileName);
                    }
                }

                return uniqueFileNames;
            }
            private async Task<bool> SaveAnuncios(Anuncios anuncios)
            {
                try
                {
               
                    List<string> uniqueFileNames = UploadedFiles(anuncios);

                    if (uniqueFileNames.Count > 0 || anuncios.RemoverImagem)
                    {
                        // Remover imagens antigas
                        if (anuncios.Imagens_Anuncios != null && anuncios.Imagens_Anuncios.Any())
                        {
                            foreach (var imagem in anuncios.Imagens_Anuncios)
                            {
                                string filePath = Path.Combine(_imageFolder, imagem.Imagem);
                                System.IO.File.Delete(filePath);
                            }
                        }

                   
                        anuncios.Imagens_Anuncios = uniqueFileNames.Select(fileName => new Imagens_Anuncios { Imagem = fileName }).ToList();
                    }

              
                    if (anuncios.Id_anuncio == 0)
                    {
                        _context.Add(anuncios);
                    }
                    else
                    {
                        _context.Update(anuncios);
                    }
                    resultDestaque = await SaveAnunciosEnviado(anuncios);
                    foreach (var fileName in uniqueFileNames)
                    {
                        var imagemAnuncio = new Imagens_Anuncios
                        {
                            Imagem = fileName,
                            Id_Anuncio = anuncios.Id_anuncio
                        };

                        _context.Add(imagemAnuncio);
                    }

                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {

                    return false;

                }
            }


            private async Task<bool> SaveAnunciosEnviado(Anuncios anuncios)
            {
                try
                {

                    List<string> uniqueFileNames = UploadedFilesDestaque(anuncios);

                    if (uniqueFileNames.Count > 0 || anuncios.RemoverImagemDestaque)
                    {
                        // Remover imagens antigas
                       if (anuncios.Imagens_Anuncios != null && anuncios.Imagens_Anuncios.Any())
                        {
                            foreach (var imagem in anuncios.Imagens_Anuncios)
                            {
                                string filePath = Path.Combine(_imageFolderDestaque, imagem.Imagem);
                                System.IO.File.Delete(filePath);
                            }
                        }


                        anuncios.Imagem_destaque = uniqueFileNames.FirstOrDefault();
                    }


                    if (anuncios.Id_anuncio == 0)
                    {
                        _context.Add(anuncios);
                    }
                    else
                    {
                        _context.Update(anuncios);
                    }
                    await _context.SaveChangesAsync();
                    foreach (var fileName in uniqueFileNames)
                    {
                        var imagemAnuncio = new Imagens_Anuncios
                        {
                            Imagem = fileName,
                            Id_Anuncio = anuncios.Id_anuncio
                        };

                        _context.Add(imagemAnuncio);
                    }

                    await _context.SaveChangesAsync();

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }



            public async Task<IActionResult> MostrarTipo(string Tipo)
            {
            var userLogin = await _userManager.GetUserAsync(User);
            if (userLogin != null)
            {
                var mensagem = "Tipo da Denúncia: " + Tipo;
                return Json(new { mensagem });
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
            public async Task<IActionResult> MostrarTipoESubTipo(string Subtipo)
            {
            var userLogin = await _userManager.GetUserAsync(User);
            if (userLogin != null)
            {
                var mensagem = "Subtipo da Denúncia: " + Subtipo ;
                return Json(new { mensagem });
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }



        public async Task<IActionResult> Index(string Categoria, string Subcategoria, int? page)
        {
            List<Anuncios> anuncios = null;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
                if (avaliacoes != null)
                {
                    return RedirectToAction("Create", "Avaliacaos");
                }
            }

            if (Subcategoria == "" || Subcategoria == null)
            {
                if (Categoria == "Todas as Vendas")
                {
                    if (user != null)
                    {
                        anuncios = await _context.Anuncios
                            .Join(
                                _context.Users,
                                anuncio => anuncio.UserId,
                                usuario => usuario.Id,
                                (anuncio, usuario) => new { Anuncio = anuncio, Usuario = usuario }
                            )
                            .Where(m => m.Anuncio.UserId != user.Id && m.Anuncio.Venda_Concluida == false)
                            .OrderByDescending(m => m.Usuario.Avaliacao)
                            .Select(m => m.Anuncio)
                            .ToListAsync();
                    }
                    else
                    {
                        anuncios = await _context.Anuncios
                            .Where(m => m.Venda_Concluida == false)
                            .ToListAsync();
                    }
                }
                else
                {
                    if (user != null)
                    {
                        anuncios = await _context.Anuncios
                            .Join(
                                _context.Users,
                                anuncio => anuncio.UserId,
                                usuario => usuario.Id,
                                (anuncio, usuario) => new { Anuncio = anuncio, Usuario = usuario }
                            )
                            .Where(m => m.Anuncio.Categoria == Categoria && m.Anuncio.UserId != user.Id && m.Anuncio.Venda_Concluida == false)
                            .OrderByDescending(m => m.Usuario.Avaliacao)
                            .Select(m => m.Anuncio)
                            .ToListAsync();
                    }
                    else
                    {
                        anuncios = await _context.Anuncios
                            .Where(m => m.Categoria == Categoria && m.Venda_Concluida == false)
                            .ToListAsync();
                    }
                }
            }
            else
            {
                if (user != null)
                {
                    anuncios = await _context.Anuncios
                        .Join(
                            _context.Users,
                            anuncio => anuncio.UserId,
                            usuario => usuario.Id,
                            (anuncio, usuario) => new { Anuncio = anuncio, Usuario = usuario }
                        )
                        .Where(m => m.Anuncio.Categoria == Categoria && m.Anuncio.SubCategoria == Subcategoria && m.Anuncio.UserId != user.Id && m.Anuncio.Venda_Concluida == false)
                        .OrderByDescending(m => m.Usuario.Avaliacao)
                        .Select(m => m.Anuncio)
                        .ToListAsync();
                }
                else
                {
                    anuncios = await _context.Anuncios
                        .Where(m => m.Venda_Concluida == false)
                        .ToListAsync();
                }
            }

            return View(anuncios);
        }


        public async Task<IActionResult> Details(int? id)
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
            if (id == null)
                {
                    return NotFound();
                }

                var anuncios = await _context.Anuncios
                    .FirstOrDefaultAsync(m => m.Id_anuncio == id);
                if (anuncios == null)
                {
                    return NotFound();
                }

                return View(anuncios);
            }

            static bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
            public async Task<IActionResult> Create()
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

            if (user != null)
                {
                    return View();
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Precisa efetuar login para criar um anúncio.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AdicionarImagens([Bind("Id_Imagem,Imagem,Id_Anuncio")] Imagens_Anuncios imagens_Anuncios)
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
            var userLogin = await _userManager.GetUserAsync(User);
            if (userLogin != null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(imagens_Anuncios);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(imagens_Anuncios);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
           
            }
        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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


        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id_anuncio,Titulo,Preco,localizacao,CC_id,Data,descricao,Categoria,FicheiroImagem,FicheiroImagemDestaque,Email,Telemovel,SubCategoria,coordenadas")] Anuncios anuncios, string SubmitButton)
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
            if (user != null)
                {
                    if (SubmitButton == "publicar")
                    {
                        anuncios.Data = DateTime.Now;
                        anuncios.UserId = user.Id;

                        if(anuncios.Telemovel != null)
                        {
                            string texto = anuncios.Telemovel;
                            bool encontradoLetra = false;
                            for (int i = 0; i < texto.Length - 1; i++)
                            {
                                if (texto[i] == '0' || texto[i] == '1' || texto[i] == '2' || texto[i] == '3' || texto[i] == '4' || texto[i] == '5' || texto[i] == '6' || texto[i] == '7' || texto[i] == '8' || texto[i] == '9' || texto[i] == ' ')
                                {
                                    encontradoLetra = false;
                                }
                                else
                                {
                                    encontradoLetra = true;
                                    break;
                                }
                            }
                            if (encontradoLetra == false)
                            {
                                texto = texto.Replace(" ", "");
                                for (int j = 3; j < texto.Length; j += 4)
                                {
                                    texto = texto.Insert(j, " ");
                                }
                                anuncios.Telemovel = texto;
                            }
                            if (encontradoLetra == true)
                            {
                                _toastNotification.AddErrorToastMessage("Telemóvel Inválido!");
                                ModelState.AddModelError("Telemovel", "Nº de Telemóvel Inválido.");
                                return View(anuncios);
                            }
                            if(anuncios.Email != null)
                            {
                                string email = anuncios.Email;

                                if (IsValidEmail(email) == false)
                                {
                                    _toastNotification.AddErrorToastMessage("Email Inválido!");
                                    return View(anuncios);
                                }

                                if(anuncios.Preco != null)
                                {
                                    if(anuncios.Titulo != null)
                                    {
                                        if(anuncios.localizacao != null)
                                        {
                                            if(anuncios.Categoria != null)
                                            {
                                                if(anuncios.SubCategoria != null)
                                                {
                                                    List<string> uniqueFileNames = UploadedFiles(anuncios);

                                                    if (uniqueFileNames.Count > 0)
                                                    {
                                                        anuncios.Imagens_Anuncios = uniqueFileNames.Select(fileName => new Imagens_Anuncios { Imagem = fileName }).ToList();
                                                    }

                                                    result = await SaveAnuncios(anuncios);

                                                    if (result == true || resultDestaque == true)
                                                    {
                                                        _toastNotification.AddSuccessToastMessage("Anuncio Publicado!");
                                                    string mensagem = $"Prezado(a) {anuncios.Email},\n\nO anúncio foi criado com sucesso! Aqui estão os detalhes:\n\n" +
                                                     $"Título: {anuncios.Titulo}\n" +
                                                     $"Preço: {anuncios.Preco}\n" +
                                                     $"Localização: {anuncios.localizacao}\n" +
                                                     $"Categoria: {anuncios.Categoria}\n" +
                                                     $"Subcategoria: {anuncios.SubCategoria}\n" +
                                                     $"Descrição: {anuncios.descricao}\n" +
                                                     $"Data de publicação: {anuncios.Data}\n" +
                                                     $"\n\nAtenciosamente,\nEquipe Tradeguard";

                                                    // Assunto do email
                                                    string assunto = "Anúncio Criado com Sucesso - Tradeguard";

                                                    // Envie o email
                                                    await EnviarEmail(email, mensagem, assunto);
                                                    return RedirectToAction("VeerPublicador", "PropostasDeCompras");
                                                    }
                                                    else
                                                    {
                                                        return View(anuncios);
                                                    }
                                                }
                                                else
                                                {
                                                    _toastNotification.AddErrorToastMessage("Selecione uma Subcateogoria!");
                                                    return View(anuncios);
                                                }
                                            }
                                            else
                                            {
                                                _toastNotification.AddErrorToastMessage("Selecione uma categoria!");
                                                return View(anuncios);
                                            }



                                        }
                                        else
                                        {
                                            _toastNotification.AddErrorToastMessage("Insira e selecione uma localização!");
                                            return View(anuncios);
                                        }

                                    }
                                    else
                                    {
                                        _toastNotification.AddErrorToastMessage("Preencha o campo titulo!");
                                        return View(anuncios);
                                    }
                                }
                                else
                                {
                                    _toastNotification.AddErrorToastMessage("Preencha o campo preço!");
                                    return View(anuncios);
                                }

                            }
                            else
                            {
                                _toastNotification.AddErrorToastMessage("Preencha o campo email!");
                                return View(anuncios);
                            }

                        }
                        else
                        {
                            _toastNotification.AddErrorToastMessage("Preencha o campo telemóvel!");
                            return View(anuncios);
                        }



                    }
                    if (SubmitButton == "visualizar")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View(anuncios);
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Precisa efetuar login para criar um anúncio.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }


            }

           
            // POST: Anuncios/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          
            public async Task<IActionResult> Delete(int? id)
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
            if (user != null)
                {
                    if (id == null)
                {
                    return NotFound();
                }

                var anuncios = await _context.Anuncios
                    .FirstOrDefaultAsync(m => m.Id_anuncio == id);
                if (anuncios == null)
                {
                    return NotFound();
                }

                return View(anuncios);
                }
                else
                {
                    _toastNotification.AddInfoToastMessage("Precisa efetuar login para criar um anúncio.");
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
            }

            // POST: Anuncios/Delete/5
            [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Id_Anuncio")] int Id_Anuncio)
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
            var userLogin = await _userManager.GetUserAsync(User);
            if (userLogin != null)
            {
                var anuncios = await _context.Anuncios.Where(i => i.Id_anuncio == Id_Anuncio).ToListAsync();
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

                    var propostas = await _context.PropostasDeCompra.Where(p => p.Id_Anuncio == Id_Anuncio).ToListAsync();
                    var favoritos = await _context.Favoritos.Where(f => f.Id_Anuncio == Id_Anuncio).ToListAsync();
                    var imagens = await _context.Imagens_Anuncios.Where(i => i.Id_Anuncio == Id_Anuncio).ToListAsync();

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

                    _context.Anuncios.RemoveRange(anuncios);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddInfoToastMessage("Anúncio removido dos publicados");
                }

                return RedirectToAction("VeerPublicador", "PropostasDeCompras");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public IActionResult GetImageLink(string imagemDestaque)
        {
            string imageUrl = ImageHelper.GetImageLink(imagemDestaque, "AnunciosEnviado");
            if (!string.IsNullOrEmpty(imageUrl) && imageUrl.StartsWith("~"))
            {
                imageUrl = imageUrl.Substring(1); 
            }

            return Json(imageUrl);
        }

        public async Task<IActionResult> GetanunciosByInput(string categoria, string subcategoria, string de_Preco, string para_Preco)
        {
            List<Anuncios> anuncios = new List<Anuncios>();
            if (!string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(de_Preco) && string.IsNullOrEmpty(para_Preco))
            {
                if(categoria == "Todas as Vendas")
                {
                    anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false).ToListAsync();
                }
                else
                {
                    anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria).ToListAsync();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(categoria) && !string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(de_Preco) && string.IsNullOrEmpty(para_Preco))
                {
                    anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria && p.SubCategoria == subcategoria).ToListAsync();
                }
                else
                {
                    if (!string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                    {
                        decimal precoInicial = Convert.ToDecimal(de_Preco);
                        decimal precoFinal = Convert.ToDecimal(para_Preco);

                        anuncios = await _context.Anuncios.Where(p => !p.Venda_Concluida &&
                                                        p.Categoria == categoria &&
                                                        p.Preco >= precoInicial &&
                                                        p.Preco <= precoFinal)
                                            .ToListAsync();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(categoria) && !string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                        {
                            decimal precoInicial = Convert.ToDecimal(de_Preco);
                            decimal precoFinal = Convert.ToDecimal(para_Preco);

                            anuncios = await _context.Anuncios.Where(p => !p.Venda_Concluida &&
                                                            p.Categoria == categoria &&
                                                            p.SubCategoria == subcategoria &&
                                                            p.Preco >= precoInicial &&
                                                            p.Preco <= precoFinal)
                                                .ToListAsync();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                            {
                                decimal precoInicial = Convert.ToDecimal(de_Preco);
                                decimal precoFinal = Convert.ToDecimal(para_Preco);

                                anuncios = await _context.Anuncios.Where(p => !p.Venda_Concluida &&
                                                                p.Preco >= precoInicial &&
                                                                p.Preco <= precoFinal)
                                                    .ToListAsync();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(categoria) && !string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                                {
                                    anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria && p.SubCategoria == subcategoria).ToListAsync();
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(categoria) && !string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && string.IsNullOrEmpty(para_Preco))
                                    {
                                        anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria && p.SubCategoria == subcategoria).ToListAsync();
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && string.IsNullOrEmpty(para_Preco))
                                        {
                                            anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria).ToListAsync();
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                                            {
                                                anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false && p.Categoria == categoria).ToListAsync();
                                            }
                                            else
                                            {

                                                if (string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(de_Preco) && !string.IsNullOrEmpty(para_Preco))
                                                {
                                                    anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false).ToListAsync();
                                                }
                                                else
                                                {
                                                    if (string.IsNullOrEmpty(categoria) && string.IsNullOrEmpty(subcategoria) && !string.IsNullOrEmpty(de_Preco) && string.IsNullOrEmpty(para_Preco))
                                                    {
                                                        anuncios = await _context.Anuncios.Where(p => p.Venda_Concluida == false).ToListAsync();
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }


                        }




                    }

                }
            }
            return Json(anuncios);
        }
    }
}
