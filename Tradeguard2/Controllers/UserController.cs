using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Tradeguard2.Data;
using Tradeguard2.Models;

namespace Tradeguard2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _toastNotification = toastNotification;
            _context = context;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> GetanunciosByInput(string texto)
        {
            List<MovimentosDaCarteira> MovimentosDaCarteira = new List<MovimentosDaCarteira>();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    MovimentosDaCarteira = await _context.MovimentosDaCarteira.Where(p => (EF.Functions.Like(p.Tipo.ToString(), "%" + texto + "%") || EF.Functions.Like(p.Quantia.ToString(), "%" + texto + "%")) && p.UserId == user.Id).ToListAsync();

                }
                else
                {
                    MovimentosDaCarteira = await _context.MovimentosDaCarteira.Where(p => p.UserId == user.Id).ToListAsync();

                }
            }

            return Json(MovimentosDaCarteira);
        }
        [HttpPost]
        private async Task AddToMovimentosDaCarteira(PagamentoViewModel pagamento, string tipo)
        {
            try
            {

                var user = await _userManager.GetUserAsync(User);
                if (tipo == "Dinheiro Adicionado")
                {
                    var movimento = new MovimentosDaCarteira
                    {
                        Tipo = tipo,
                        Quantia = Convert.ToInt32(pagamento.Quantia),
                        Data = DateTime.Now,
                        UserId = user.Id
                    };
                    _context.Add(movimento);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                // Handle exceptions if necessary
            }
        }
        public async Task<IActionResult> MudarPasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MudarPassword(AlterarSenhaViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            if (ModelState.IsValid)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.PasswordAntiga, model.PasswordNova);

                if (changePasswordResult.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Senha alterada com sucesso.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Senha alterada sem sucesso.");
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Se chegou aqui, significa que houve algum erro
            return View();
        }

        //public async Task<IActionResult> MudarPassword()
        //{
        //    var user1 = await _userManager.GetUserAsync(User);

        //    if (user1 != null)
        //    {
        //        var avaliacoes = await _context.PropostasDeCompra.FirstOrDefaultAsync(p => p.CC_comprador == user1.CC && p.Vendedor_Avaliado == false && p.Venda_Concluida == true);
        //        if (avaliacoes != null)
        //        {
        //            return RedirectToAction("Create", "Avaliacaos");
        //        }
        //    }

        //    var userlog = await _userManager.GetUserAsync(User);
        //    if (userlog != null)
        //    {                
        //        return View();
        //    }
        //    else
        //    {
        //        _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
        //        return RedirectToPage("/Account/Login", new { area = "Identity" });
        //    }

        //}
        public async Task<IActionResult> Gerir_Carteira(string id)
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
            var userlog = await _userManager.GetUserAsync(User);
            if (userlog != null)
            {
                var cc = userlog.CC;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CC == cc);
                return View(user);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> adicionar_Saldo([Bind("Nome,CVV,Validade,Quantia,N_Cartao")] PagamentoViewModel pagamento)
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
            var userlog = await _userManager.GetUserAsync(User);
            if (userlog != null)
            {
                var utilizador = await _context.Users.FirstOrDefaultAsync(p => p.Id == userlog.Id);
                if (!string.IsNullOrEmpty(pagamento.Nome)
                    && !string.IsNullOrEmpty(pagamento.CVV)
                    && !string.IsNullOrEmpty(pagamento.Validade)
                    && !string.IsNullOrEmpty(pagamento.N_Cartao)
                    && !string.IsNullOrEmpty(pagamento.Quantia))
                {

                    pagamento.N_Cartao = pagamento.N_Cartao.Replace(" ", "");
                    pagamento.Validade = pagamento.Validade.Replace("/", "");
                    // Verifica se a string após a remoção dos espaços só contém números
                    if (long.TryParse(pagamento.N_Cartao, out _) && long.TryParse(pagamento.CVV, out _)
                    && long.TryParse(pagamento.Validade, out _)
                    && decimal.TryParse(pagamento.Quantia, out _))
                    { 
                    
                    }
                        utilizador.Saldo += Convert.ToDecimal(pagamento.Quantia);
                   
                    await _context.SaveChangesAsync();
                    AddToMovimentosDaCarteira(pagamento, "Dinheiro Adicionado");
                    _toastNotification.AddSuccessToastMessage("Saldo adicionado.");
                }
                else
                {
                    _toastNotification.AddWarningToastMessage("Preencha todos os campos.");
                    return View("Inserir_Dinheiro", pagamento);
                }

                return RedirectToAction("Gerir_Carteira");
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> Inserir_Dinheiro()
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
            var userlog = await _userManager.GetUserAsync(User);
            if (userlog != null)
            {
                return View();
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public async Task<IActionResult> DetailsUser(string id)
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
            var userlog = await _userManager.GetUserAsync(User);
            if (userlog != null)
            {
                var cc = userlog.CC;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CC == cc);
                return View(user);
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser([Bind("CC,Email,Nome,Telemovel,ProfilePicture")] ApplicationUser model, IFormFile ProfilePicture, string submitButton)
        {
            var userlog = await _userManager.GetUserAsync(User);

            if (userlog == null)
            {
                _toastNotification.AddInfoToastMessage("Precisa efetuar login.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var cc1 = userlog.CC;
            var user1 = await _userManager.Users.FirstOrDefaultAsync(u => u.CC == cc1);

            if (user1 == null)
            {
                _toastNotification.AddInfoToastMessage("Utilizador não encontrado.");
                return RedirectToAction("Index", "Home");
            }

            if (model.Email == null || model.Nome == null || model.Telemovel == null || model.CC == null)
            {
                _toastNotification.AddInfoToastMessage("Campos obrigatórios não preenchidos.");
                return RedirectToAction("DetailsUser", new { id = user1.Id });
            }

            if (user1.Email != model.Email || user1.Telemovel != model.Telemovel || user1.Nome != model.Nome || user1.CC != model.CC )
            {
                user1.Email = model.Email;
                user1.Telemovel = model.Telemovel;
                user1.Nome = model.Nome;
                user1.CC = model.CC;

                if (model.Nome.Length <= 4)
                {
                    ModelState.AddModelError("Nome", "O Nome deve ter pelo menos 4 caracteres.");
                    _toastNotification.AddErrorToastMessage("O Nome deve ter pelo menos 4 caracteres.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                // Verificar se já existe um usuário com o mesmo e-mail
                var existingUserByEmail = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Id != user1.Id);
                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError("Email", "Já existe um utilizador com este Email.");
                    _toastNotification.AddErrorToastMessage("O Nome deve ter pelo menos 4 caracteres.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                // Verificar se já existe um usuário com o mesmo número de CC
                var existingUserByCC = await _userManager.Users.FirstOrDefaultAsync(u => u.CC == model.CC && u.Id != user1.Id);
                if (existingUserByCC != null)
                {
                    ModelState.AddModelError("CC", "Já existe um utilizador com este Nº Cartão de Cidadão.");
                    _toastNotification.AddErrorToastMessage("Já existe um utilizador com este Nº Cartão de Cidadão.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                // Validar o formato do Nº Cartão de Cidadão
                string combinacao = @"^\d{8}\s*[0-9]\s*[A-Z]{2}\s*\d$";
                string textoCC = user1.CC;
                if (!Regex.IsMatch(textoCC, combinacao))
                {
                    ModelState.AddModelError("CC", "Nº Cartão de Cidadão Inválido. (00000000 0 AA 0) (A = Letra, 0 = Número)\nVerifique se tem algum espaço a mais.");

                    _toastNotification.AddErrorToastMessage("Nº Cartão de Cidadão Inválido. (00000000 0 AA 0) (A = Letra, 0 = Número)\nVerifique se tem algum espaço a mais.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                if(model.Telemovel.Length != 11)
                {
                    ModelState.AddModelError("Telemovel", "O Nº de Telemóvel deve ter exatamente 9 dígitos.");
                    _toastNotification.AddErrorToastMessage("O Nº de Telemóvel deve ter exatamente 9 dígitos.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                // Validar o formato do Nº Telemóvel
                string texto = model.Telemovel;
                if (texto.Any(c => !char.IsDigit(c) && c != ' '))
                {
                    ModelState.AddModelError("Telemovel", "Nº de Telemóvel Inválido.");
                    _toastNotification.AddErrorToastMessage("Nº de Telemóvel Inválido.");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }

                texto = texto.Replace(" ", "");
                for (int j = 3; j < texto.Length; j += 4)
                {
                    texto = texto.Insert(j, " ");
                }
                user1.Telemovel = texto;

                var result = await _userManager.UpdateAsync(user1);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Alterações Guardadas!");
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }
                else
                {
                    return RedirectToAction("DetailsUser", new { id = user1.Id });
                }
            }
            else
            {
                _toastNotification.AddInfoToastMessage("Não foram feitas alterações.");
                return RedirectToAction("DetailsUser", new { id = user1.Id });
            }
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        // Ação para processar a solicitação de redefinição de senha
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage("E-mail não encontrado.");
                    return RedirectToAction(nameof(ForgotPassword));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(nameof(ResetPassword), "User", new { token, email = user.Email }, Request.Scheme);

                await EnviarEmail(user.Email, $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>.", "Redefinir senha");
                _toastNotification.AddSuccessToastMessage("Link para alterar a senha enviado.");
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // Ação para exibir a página de confirmação de solicitação de redefinição de senha
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // Ação para exibir a página de redefinição de senha
        public IActionResult ResetPassword(string token = null)
        {
            if (token == null)
            {
                return BadRequest("Um token é necessário para redefinir a senha.");
            }
            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        // Ação para processar a redefinição de senha
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage("Utilizador não encontrado.");
                    return RedirectToAction(nameof(ResetPassword));
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Senha alterada com sucesso. Faça login novamente.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        // Ação para exibir a página de confirmação de redefinição de senha
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private async Task EnviarEmail(string emailrecebe, string mensagem, string assunto)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(emailrecebe);
            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true; // Permitir HTML no corpo do e-mail
            mailMessage.From = new MailAddress("tradeguardsuporte@gmail.com");

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("tradeguardsuporte@gmail.com", "ojlh jgel shpj pbtr");
            smtpClient.EnableSsl = true;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Não foi possível enviar o e-mail.", ex);
            }
        }
    }
}
