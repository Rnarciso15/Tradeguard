using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradeguard2.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        //•	Avaliacao [Id_Avaliacao(Pk), CC_id(Fk), Data, Avaliacao_Atribuida]
        [Key]
        public int Id_Avaliacao { get; set; }
        [Required]
        public int Avaliacao_Atribuida { get; set; }
        public string CC_Vendedor { get; set; }
        public string CC_Comprador { get; set; }
        public DateTime Data { get; set; }

        [NotMapped]
        public ApplicationUser User { get; set; }
    }
}
