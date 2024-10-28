using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    public class ApplicationUser : IdentityUser 
    {


        [Display(Name = "ProfilePicture")]
        public byte[] ProfilePicture { get; set; }
     
        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nº Cartão de Cidadão")]
        public string CC { get; set; }

        public int N_Validados { get; set; }
        public decimal Saldo { get; set; }
        public int Avaliacao { get; set; }
        [NotMapped]
        public List<Mensagens> Mensagens { get; set; }
    }
}
