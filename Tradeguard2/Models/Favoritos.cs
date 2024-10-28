using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("Favoritos")]
    public class Favoritos
    {
        //	Favoritos[Id_Denuncia(Pk), Tipo, Data, Id_Anuncio(Fk), CC_id(Fk)]
        [Key]
        public int Id_Favorito{ get; set; }
        [Required]
        public int Id_Anuncio { get; set; }
        [Required]
        public string CC { get; set; }       

        [NotMapped]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [NotMapped]
        [ValidateNever]
        public Anuncios Anuncios { get; set; }

    }
}
