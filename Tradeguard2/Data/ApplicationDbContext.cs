using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tradeguard2.Models;

namespace Tradeguard2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
     

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }

        public DbSet<Anuncios> Anuncios { get; set; }
        public DbSet<Imagens_Anuncios> Imagens_Anuncios { get; set; }
        public DbSet<Denuncias> Denuncias { get; set; }
        public DbSet<Elogios> Elogios { get; set; }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Mensagens> Mensagens { get; set; }
        public DbSet<Favoritos> Favoritos { get; set; }
        public DbSet<PropostasDeCompra> PropostasDeCompra { get; set; }
        public DbSet<HistoricoDeCompra> HistoricoDeCompra { get; set; }
        public DbSet<AnuncioValidado> AnuncioValidado { get; set; }
        public DbSet<MovimentosDaCarteira> MovimentosDaCarteira { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedRolesData();
            modelBuilder.SeedAdmin();
         
   

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });


        }

        internal void SaveChangesAsync(PropostasDeCompra propostasDeCompra)
        {
            throw new NotImplementedException();
        }
    }
}