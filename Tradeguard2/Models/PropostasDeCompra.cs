using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("PropostasDeCompra")]
    public class PropostasDeCompra
    {
        [Key]
        public int Id_Proposta { get; set; }
        [Required]
        public int Id_Anuncio { get; set; }
        [Required]
        public string CC_vendedor { get; set; }
        [Required]
        public string CC_comprador { get; set; }
        [Required]
        public string Preco_proposta { get; set; }
        [Required]
        public bool Dinheiro_pendente { get; set; } = true;
        [Required]
        public DateTime Data_enviada { get; set; }
        public bool Proposta_Aceite { get; set; } = false;
   
        public DateTime Data_aceite { get; set; }
  
        public DateTime Data_Final { get; set; }
        public bool Produto_enviado { get; set; } = false;

        public bool Produto_recebido  { get; set; } = false;

        public bool Venda_Validada{ get; set; } = false;

        public bool Venda_Concluida { get; set; } = false;

        public bool Vendedor_Avaliado { get; set; } = false;

        public bool Proposta_vista { get; set; } = false;

        [NotMapped]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
    public class PropostaAnuncioViewModel
    {
        public PropostasDeCompra Proposta { get; set; }
        public Anuncios Anuncio { get; set; }

        public List<AnuncioValidado> AnuncioValidado { get; set; }
    }

        public class AnuncioPropostaViewModel
        {
            public Anuncios Anuncio { get; set; }
            public AnuncioValidado AnuncioValidado { get; set; }
            public PropostasDeCompra Proposta { get; set; }
        }

}
