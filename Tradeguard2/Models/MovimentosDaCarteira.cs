using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("MovimentosDaCarteira")]
    public class MovimentosDaCarteira
    {
        [Key]
        public int Id_Movimento { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Quantia { get; set; }       
        public DateTime Data { get; set; }

        public int? Id_proposta { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
    public class PagamentoViewModel
    {
        public string N_Cartao { get; set; }
        public string CVV { get; set; }
        public string Nome { get; set; }
        public string Quantia { get; set; }
        public string Validade { get; set; }
        
      
    }
    }
