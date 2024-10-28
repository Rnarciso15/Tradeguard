using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tradeguard2.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tradeguard2.Data
{
    public static class AdminSeeder
    {
        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            var imagePath = Path.Combine("wwwroot", "imagens", "UserDefault.png");
            byte[] defaultProfilePicture;
            using (var stream = new FileStream(imagePath, FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    defaultProfilePicture = memoryStream.ToArray();
                }
            }

            // Hash da senha "654321Qweqwe"
            var passwordHash = GetPasswordHash("654321Qweqwe");

            // Cria o usuário administrador
            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@gmail.com",
                Nome = "Admin",
                NormalizedUserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@gmail.COM",
                EmailConfirmed = true,
                PasswordHash = passwordHash,
                Telemovel = "000000000",
                SecurityStamp = string.Empty,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                ProfilePicture = defaultProfilePicture,
                CC = "00000000 0 AA 0",
                Saldo = 0,
                Avaliacao = 0
            };

            // Adiciona o usuário administrador ao contexto
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
        }

        // Função para obter o hash da senha
        private static string GetPasswordHash(string password)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            return hasher.HashPassword(null, password);
        }
    }


}
