using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tradeguard2.Data
{
    public static class SeedRoles
    {
        public static void SeedRolesData(this ModelBuilder modelBuilder)
        {
            // Role Administrador
            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Id = "1",
                     Name = "Administrador",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                     NormalizedName = "Administrador"
                 },
                 new IdentityRole
                 {
                     Id = "2",
                     Name = "Moderador",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                     NormalizedName = "Moderador"
                 },
                 new IdentityRole
                 {
                     Id = "3",
                     Name = "Utilizador",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                     NormalizedName = "Utilizador"
                 }
             );
        }
    }
}
