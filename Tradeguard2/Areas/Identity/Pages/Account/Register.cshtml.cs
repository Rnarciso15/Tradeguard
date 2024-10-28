using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tradeguard2.Models;
using static System.Net.Mime.MediaTypeNames;
using NToastNotify;
using Image = System.Drawing.Image;

namespace Tradeguard2.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IToastNotification _toastNotification;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IToastNotification toastNotification)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _toastNotification = toastNotification;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "Por favor insira um endereço de email válido.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "ProfilePicture")]
            public IFormFile ProfilePicture { get; set; }

            public decimal Saldo { get; set; }
            public int N_Validados { get; set; }
            public int Avaliacao { get; set; }

            [Required(ErrorMessage = "O campo Nº de Telemóvel é obrigatório.")]
            [Display(Name = "Telemóvel")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "O Nº de Telemóvel deve ter exatamente 9 dígitos.")]
            public string Telemovel { get; set; }

            [Required(ErrorMessage = "O campo Nome é obrigatório.")]
            [Display(Name = "Nome")]
            [StringLength(40, MinimumLength = 4, ErrorMessage = "O Nome deve ter pelo menos 4 caracteres.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O campo Nº de Cartão de Cidadão é obrigatório.")]
            [Display(Name = "Nº Cartão de Cidadão")]
            public string CC { get; set; }
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
            catch (Exception ex)
            {
                // Handle exception
            }
        }
        public bool IsImage(IFormFile file)
        {
            try
            {
                using (var img = Image.FromStream(file.OpenReadStream()))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                returnUrl ??= Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                if (Input.Nome.Length < 4)
                {
                    ModelState.AddModelError("Input.Nome", "O Nome deve ter pelo menos 4 caracteres.");
                    _toastNotification.AddErrorToastMessage("O Nome deve ter pelo menos 4 caracteres.");
                    return Page();
                }

                if (string.IsNullOrEmpty(Input.Nome))
                {
                    ModelState.AddModelError("Input.Nome", "O campo Nome é obrigatório.");
                    _toastNotification.AddErrorToastMessage("O campo Nome é obrigatório.");
                    return Page();
                }

                if (Input.Telemovel.Length != 11)
                {
                    ModelState.AddModelError("Input.Telemovel", "O Nº de Telemóvel deve ter exatamente 9 dígitos.");
                    _toastNotification.AddErrorToastMessage("O Nº de Telemóvel deve ter exatamente 9 dígitos.");
                    return Page();
                }

                if (string.IsNullOrEmpty(Input.Telemovel))
                {
                    ModelState.AddModelError("Input.Telemovel", "O campo Nº de Telemóvel é obrigatório.");
                    _toastNotification.AddErrorToastMessage("O campo Nº de Telemóvel é obrigatório.");
                    return Page();
                }

                var existingUserByCC = await _userManager.Users.FirstOrDefaultAsync(u => u.CC == Input.CC);
                if (existingUserByCC != null)
                {
                    ModelState.AddModelError("Input.CC", "Já existe um utilizador com este Nº Cartão de Cidadão.");
                    _toastNotification.AddErrorToastMessage("Já existe um utilizador com este Nº Cartão de Cidadão.");
                    return Page();
                }

                string combinacao = @"^\d{8}\s*[0-9]\s*[A-Z]{2}\s*\d$";
                string textoCC = Input.CC.ToUpper();

                if (!Regex.IsMatch(textoCC, combinacao))
                {
                    ModelState.AddModelError("Input.CC", "Nº Cartão de Cidadão Inválido. (00000000 0 AA 0) (A = Letra, 0 = Número)\nVerifique se tem algum espaço a mais.");
                    _toastNotification.AddErrorToastMessage("Nº Cartão de Cidadão Inválido. (00000000 0 AA 0) (A = Letra, 0 = Número)\nVerifique se tem algum espaço a mais.");
                    return Page();
                }

                var existingUserByEmail = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == Input.Email);

                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError("Input.Email", "Já existe um utilizador com este Email.");
                    _toastNotification.AddErrorToastMessage("Já existe um utilizador com este Email.");
                    return Page();
                }

                if (!IsValidEmail(Input.Email))
                {
                    ModelState.AddModelError("Input.Email", "O Email inserido não é válido.");
                    _toastNotification.AddErrorToastMessage("O Email inserido não é válido.");
                    return Page();
                }

                var passwordErrors = ValidatePassword(Input.Password);
                if (passwordErrors.Any())
                {
                    foreach (var error in passwordErrors)
                    {
                        ModelState.AddModelError("Input.Password", error);
                        _toastNotification.AddErrorToastMessage(error);
                    }
                    return Page();
                }


                var passwordErrors1 = ValidatePassword(Input.ConfirmPassword);
                if (passwordErrors1.Any())
                {
                    foreach (var error in passwordErrors1)
                    {
                        ModelState.AddModelError("Input.ConfirmPassword", error);
                        _toastNotification.AddErrorToastMessage(error);
                    }
                    return Page();
                }

                if (Input.ProfilePicture != null)
                {
                    if (!IsImage(Input.ProfilePicture))
                    {
                        _toastNotification.AddErrorToastMessage("O ficheiro selecionado não é uma imagem. Selecione uma foto de perfil válida.");
                        return Page();
                    }
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Selecione uma foto de perfil.");
                    return Page();
                }

                var user = CreateUser();
                user.CC = Input.CC.ToUpper();

                if (Input.ProfilePicture != null)
                {
                    using (var dataStream = new MemoryStream())
                    {
                        await Input.ProfilePicture.CopyToAsync(dataStream);
                        user.ProfilePicture = dataStream.ToArray();
                    }
                }




                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var roleName = IsUserAdmin() ? "Moderador" : "Utilizador";
                    var resultRoles = await _userManager.AddToRoleAsync(user, roleName);
                    if (resultRoles.Succeeded)
                    {
                        _logger.LogInformation("Utilizador criado com sucesso.");
                        string nomeUsuario = Input.Nome;
                        string mensagem = $"Prezado(a) {nomeUsuario},\n\nObrigado por se registrar no nosso site. Estamos muito felizes em tê-lo(a) como parte de nossa comunidade.\n\nAtenciosamente,\n Tradeguard";
                        string assunto = "Agradecimento por se Registrar - Tradeguard";

                        await EnviarEmail(user.Email, mensagem, assunto);
                        var userId = await _userManager.GetUserIdAsync(user);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro: {ex}");
            }

            return Page();
        }

        private IEnumerable<string> ValidatePassword(string password)
        {
            var errors = new List<string>();
            var options = _userManager.Options.Password;

            if (password.Length < options.RequiredLength)
            {
                errors.Add($"A senha deve ter pelo menos {options.RequiredLength} caracteres.");
            }
            if (options.RequireDigit && !password.Any(char.IsDigit))
            {
                errors.Add("A senha deve ter pelo menos um número.");
            }
            if (options.RequireLowercase && !password.Any(char.IsLower))
            {
                errors.Add("A senha deve ter pelo menos uma letra minúscula.");
            }
            if (options.RequireUppercase && !password.Any(char.IsUpper))
            {
                errors.Add("A senha deve ter pelo menos uma letra maiúscula.");
            }
            if (options.RequireNonAlphanumeric && !password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                errors.Add("A senha deve ter pelo menos um caracter não seja uma alfanumérico.");
            }

            return errors;
        }

        private bool IsUserAdmin()
        {
            return User.IsInRole("Administrador");
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                var user = Activator.CreateInstance<ApplicationUser>();
                user.CC = Input.CC;
                user.Saldo = 0;
                user.Avaliacao = 0;
                user.N_Validados = 0;
                user.Nome = Input.Nome;
                user.Telemovel = Input.Telemovel;

                return user;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
