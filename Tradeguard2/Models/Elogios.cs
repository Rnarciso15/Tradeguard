using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tradeguard2.Models
{
    [Table("Elogios")]
    public class Elogios
    {
        //•	Elogios [Id_Elogio(Pk), Tipo, Data, Id_Anuncio(Fk), CC_id(Fk)]
        [Key]
        public int Id_Elogio { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Id_Anuncio { get; set; }
        [Required]
        public string CC { get; set; }

        public DateTime Data { get; set; }

        [NotMapped]
        public ApplicationUser User { get; set; }
    }
}
