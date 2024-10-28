using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("Imagens_Anuncios")]
    public class Imagens_Anuncios
    {
        [Key]
        public int Id_Imagem { get; set; }
        [Required]
        public string Imagem { get; set; }
        [Required]
        
        public int Id_Anuncio { get; set; }
        [NotMapped]      
        public Anuncios Anuncios { get; set; }
    }
}
