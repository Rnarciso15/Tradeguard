using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("Denuncias")]
    public class Denuncias
    {
        //	Denuncias[Id_Denuncia(Pk), Tipo, Data, Id_Anuncio(Fk), CC_id(Fk)]
        [Key]
        public int Id_Denuncia { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string SubTipo { get; set; }
        [Required]
        public int Id_Anuncio { get; set; }
        [Required]
        public string CC_anunciador { get; set; }
        [Required]
        public string CC_denunciador { get; set; }
        public DateTime Data { get; set; }

        [NotMapped]
        [ValidateNever]
        public ApplicationUser User { get; set; }

    }
    public class DenunciasAnuncioViewModel
    {
        public Denuncias Denuncias { get; set; }
        public Anuncios Anuncio { get; set; }

        public List<AnuncioValidado> AnuncioValidado { get; set; }
    }

}
