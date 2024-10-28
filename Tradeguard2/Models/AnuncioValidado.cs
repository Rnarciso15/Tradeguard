using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Printing;

namespace Tradeguard2.Models
{
    [Table("AnuncioValidado")]
    public class AnuncioValidado
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Id_anuncio { get; set; }
        [Required]
        public int Id_proposta { get; set; }
        
        public string? Imagem_destaque { get; set; }
        public string? Imagem_destaque1 { get; set; }
        
        public string? Imagem_destaque2 { get; set; }
       
        public string? Imagem_destaque3 { get; set; }
        public bool Recebido { get; set; }
        public string CC_Comrador { get; set; }
        public string CC_Vendedor { get; set; }

    }

    public class AnunciosImagensViewModel
    {
        public List<Anuncios> Anuncios { get; set; }
        public List<Imagens_Anuncios> ImagensAnuncio { get; set; }
        public List<AnuncioValidado> AnuncioValidado { get; set; }
    }
    public class DenunciasAnunciosImagensViewModel
    {
        public List<Anuncios> Anuncios { get; set; }
        public List<Denuncias> Denuncias { get; set; }
        public List<Imagens_Anuncios> ImagensAnuncio { get; set; }
        public List<AnuncioValidado> AnuncioValidado { get; set; }
    }
}
