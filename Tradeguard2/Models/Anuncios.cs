    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing.Printing;

    namespace Tradeguard2.Models
    {
        [Table("Anuncios")]
        public class Anuncios
        {
            [Key]
            public int Id_anuncio { get; set; }
            [Required]
            [StringLength(50)]
            public string Titulo { get; set; }
            [Required]
            public decimal Preco {  get; set; }
            [Required]
            public string localizacao { get; set; }

            public string coordenadas { get; set; }
            [Required]
            public string Categoria { get; set; }
       
            public string SubCategoria { get; set; }

            [Required]
            public string Imagem_destaque { get; set; }
            public DateTime Data { get; set; }
            public string Email { get; set; }
            public string Telemovel { get; set; }

            public string descricao { get; set; }

        public bool Venda_Concluida { get; set; } = false;
        public string UserId { get; set; }

            [NotMapped]
            public List<IFormFile> FicheiroImagemDestaque { get; set; }
            [NotMapped]
            public bool RemoverImagemDestaque { get; set; }
            [NotMapped]
            public List<IFormFile> FicheiroImagem { get; set; }
            [NotMapped]
            public bool RemoverImagem { get; set; }

            [NotMapped]
            [ValidateNever]
            public ICollection<Imagens_Anuncios> Imagens_Anuncios { get; set; } 

            public ApplicationUser User { get; set; }
            [NotMapped]
            public Denuncias Denuncia { get; set; }
            [NotMapped]
            public Favoritos Favoritos { get; set; }
        }


  
    public class AnunciosFavoritosViewModel
    {
        public Anuncios Anuncios { get; set; }
        public Favoritos Favoritos { get; set; }
      
    }

    public class AnunciosValidadosViewModel
    {
        public Anuncios Anuncios { get; set; }
        public AnuncioValidado AnuncioValidado { get; set; }

        public PropostasDeCompra PropostasDeCompra { get; set; }

    }

}
