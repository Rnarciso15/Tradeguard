using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace Tradeguard2.Models
{
    [Table("Mensagens")]
    public class Mensagens
    {
        [Key]
        public int id_Mensagens { get; set; }
        [Required]
        public string Utilizador_1 { get; set; }
        [Required]
        public string Utilizador_2 { get; set; }
        [Required]
        public string texto { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public bool Mensagem_Vista { get; set; } = false; 
        public bool Mensagem_Apagada { get; set; } = false;
        [NotMapped]
        [ValidateNever]
        public ApplicationUser User { get; set; }

       
    }

    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string PasswordAntiga { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string PasswordNova { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [Compare("PasswordNova", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        public string ConfirmarPasswordNova { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }

}
