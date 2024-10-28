using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tradeguard2.Models
{

    [Table("HistoricoDeCompra")]
    public class HistoricoDeCompra
    {
            
       
            [Key]
            public int Id_historico { get; set; }
            [Required]
            public string Tipo { get; set; } //venda ou compra
            [Required]
            public int Id_Anuncio { get; set; }
            [Required]
            public string CC_vendedor { get; set; }
            [Required]
            public string CC_comprador { get; set; }
            [Required]
            public string Preco { get; set; }
            [Required]
            public bool vendido { get; set; } = false;
            public DateTime Data { get; set; }

            [NotMapped]
            public ApplicationUser User { get; set; }
  
  

}
}
