﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tradeguard2.Migrations
{
    /// <inheritdoc />
    public partial class frist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnuncioValidado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_anuncio = table.Column<int>(type: "int", nullable: false),
                    Id_proposta = table.Column<int>(type: "int", nullable: false),
                    Imagem_destaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem_destaque1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem_destaque2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem_destaque3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recebido = table.Column<bool>(type: "bit", nullable: false),
                    CC_Comrador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC_Vendedor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioValidado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Validados = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Avaliacao = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id_Avaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avaliacao_Atribuida = table.Column<int>(type: "int", nullable: false),
                    CC_Vendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC_Comprador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id_Avaliacao);
                });

            migrationBuilder.CreateTable(
                name: "Denuncias",
                columns: table => new
                {
                    Id_Denuncia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false),
                    CC_anunciador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC_denunciador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denuncias", x => x.Id_Denuncia);
                });

            migrationBuilder.CreateTable(
                name: "Elogios",
                columns: table => new
                {
                    Id_Elogio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elogios", x => x.Id_Elogio);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id_Favorito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id_Favorito);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoDeCompra",
                columns: table => new
                {
                    Id_historico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false),
                    CC_vendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC_comprador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vendido = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoDeCompra", x => x.Id_historico);
                });

            migrationBuilder.CreateTable(
                name: "Imagens_Anuncios",
                columns: table => new
                {
                    Id_Imagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens_Anuncios", x => x.Id_Imagem);
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    id_Mensagens = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Utilizador_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Utilizador_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mensagem_Vista = table.Column<bool>(type: "bit", nullable: false),
                    Mensagem_Apagada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.id_Mensagens);
                });

            migrationBuilder.CreateTable(
                name: "MovimentosDaCarteira",
                columns: table => new
                {
                    Id_Movimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantia = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_proposta = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentosDaCarteira", x => x.Id_Movimento);
                });

            migrationBuilder.CreateTable(
                name: "PropostasDeCompra",
                columns: table => new
                {
                    Id_Proposta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Anuncio = table.Column<int>(type: "int", nullable: false),
                    CC_vendedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC_comprador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco_proposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dinheiro_pendente = table.Column<bool>(type: "bit", nullable: false),
                    Data_enviada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Proposta_Aceite = table.Column<bool>(type: "bit", nullable: false),
                    Data_aceite = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Produto_recebido = table.Column<bool>(type: "bit", nullable: false),
                    Venda_Validada = table.Column<bool>(type: "bit", nullable: false),
                    Venda_Concluida = table.Column<bool>(type: "bit", nullable: false),
                    Vendedor_Avaliado = table.Column<bool>(type: "bit", nullable: false),
                    Proposta_vista = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropostasDeCompra", x => x.Id_Proposta);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anuncios",
                columns: table => new
                {
                    Id_anuncio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    localizacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coordenadas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem_destaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Venda_Concluida = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncios", x => x.Id_anuncio);
                    table.ForeignKey(
                        name: "FK_Anuncios_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "f04661dd-9741-418a-a9c5-26f88a690400", "Administrador", "Administrador" },
                    { "2", "6e0bbd65-f3e8-463e-827f-2b04f70a53ea", "Moderador", "Moderador" },
                    { "3", "3228449a-0c4e-4237-834a-924790026c9d", "Utilizador", "Utilizador" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avaliacao", "CC", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "N_Validados", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "Saldo", "SecurityStamp", "Telemovel", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7a98e2c0-58b7-4979-9b67-5e94fabd1982", 0, 0, "00000000 0 AA 0", "81454cdd-3abe-4c70-aff2-44f1f07afb9f", "admin@gmail.com", true, false, null, 0, "Admin", "ADMIN@gmail.COM", "admin@gmail.com", "AQAAAAIAAYagAAAAEHUXtUp/T2wrQ/6WMP1M8BmUa7aFj4X99F0LOxxtTwTrFNZf5Jik7qFlW4TNfP8Uzg==", null, false, new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 1, 104, 0, 0, 1, 104, 8, 3, 0, 0, 0, 77, 59, 145, 231, 0, 0, 0, 32, 99, 72, 82, 77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 96, 0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 1, 26, 80, 76, 84, 69, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 160, 158, 158, 158, 161, 161, 161, 164, 164, 164, 157, 157, 157, 156, 156, 156, 155, 155, 155, 163, 163, 163, 170, 170, 170, 178, 178, 178, 185, 185, 185, 201, 201, 201, 205, 205, 205, 210, 210, 210, 213, 213, 213, 225, 225, 225, 221, 221, 221, 215, 215, 215, 207, 207, 207, 193, 193, 193, 187, 187, 187, 172, 172, 172, 162, 162, 162, 165, 165, 165, 168, 168, 168, 184, 184, 184, 194, 194, 194, 208, 208, 208, 219, 219, 219, 238, 238, 238, 241, 241, 241, 248, 248, 248, 250, 250, 250, 252, 251, 252, 253, 253, 253, 255, 255, 255, 254, 254, 254, 246, 246, 246, 240, 240, 240, 236, 236, 236, 211, 211, 211, 196, 196, 196, 186, 186, 186, 173, 173, 173, 167, 166, 167, 177, 177, 177, 197, 197, 197, 224, 224, 224, 242, 242, 242, 243, 243, 243, 189, 189, 189, 217, 217, 217, 235, 235, 235, 249, 249, 249, 233, 233, 233, 175, 175, 175, 150, 150, 150, 214, 214, 214, 245, 245, 245, 154, 154, 154, 231, 231, 231, 182, 182, 182, 218, 218, 218, 183, 183, 183, 191, 191, 191, 226, 226, 226, 222, 222, 222, 239, 239, 239, 220, 220, 220, 153, 153, 153, 179, 179, 179, 199, 199, 199, 198, 198, 198, 190, 190, 190, 192, 192, 192, 247, 247, 247, 206, 206, 206, 171, 171, 171, 176, 176, 176, 227, 227, 227, 169, 169, 169, 228, 228, 228, 212, 212, 212, 203, 203, 203, 180, 180, 180, 234, 234, 234, 232, 232, 232, 229, 229, 229, 200, 200, 200, 204, 204, 204, 149, 149, 149, 215, 224, 177, 237, 0, 0, 0, 3, 116, 82, 78, 83, 248, 252, 251, 211, 199, 99, 147, 0, 0, 0, 1, 98, 75, 71, 68, 38, 90, 8, 152, 181, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 46, 35, 0, 0, 46, 35, 1, 120, 165, 63, 118, 0, 0, 0, 7, 116, 73, 77, 69, 7, 232, 1, 6, 20, 32, 1, 215, 100, 90, 187, 0, 0, 2, 133, 122, 84, 88, 116, 82, 97, 119, 32, 112, 114, 111, 102, 105, 108, 101, 32, 116, 121, 112, 101, 32, 120, 109, 112, 0, 0, 56, 141, 157, 149, 77, 18, 219, 32, 12, 133, 247, 58, 69, 143, 128, 37, 33, 193, 113, 28, 131, 119, 157, 233, 178, 199, 239, 19, 228, 63, 110, 234, 169, 51, 137, 9, 136, 167, 15, 9, 4, 253, 254, 249, 139, 126, 196, 83, 139, 144, 108, 178, 123, 241, 100, 139, 137, 93, 44, 187, 114, 50, 182, 108, 110, 213, 186, 52, 246, 190, 95, 46, 151, 157, 29, 253, 213, 52, 122, 178, 75, 214, 38, 73, 155, 39, 21, 216, 22, 171, 164, 197, 87, 199, 196, 44, 190, 106, 207, 106, 120, 67, 80, 4, 147, 216, 101, 151, 158, 86, 217, 188, 200, 234, 197, 48, 209, 90, 56, 179, 133, 83, 252, 183, 205, 186, 75, 140, 81, 120, 0, 141, 218, 30, 28, 178, 206, 129, 187, 249, 32, 121, 200, 160, 239, 18, 51, 180, 194, 98, 1, 53, 102, 200, 170, 25, 66, 192, 181, 28, 50, 187, 109, 156, 100, 145, 133, 187, 20, 41, 160, 153, 44, 12, 39, 38, 107, 102, 85, 181, 55, 142, 57, 22, 40, 133, 92, 241, 73, 178, 194, 247, 238, 227, 225, 238, 176, 226, 110, 59, 12, 157, 119, 136, 215, 248, 160, 149, 132, 241, 203, 248, 109, 83, 4, 111, 113, 196, 210, 149, 108, 241, 194, 45, 60, 96, 124, 80, 164, 21, 100, 119, 18, 96, 32, 182, 136, 28, 91, 29, 75, 171, 96, 239, 32, 187, 142, 99, 129, 12, 151, 25, 68, 129, 198, 30, 211, 223, 192, 99, 146, 221, 208, 159, 201, 135, 219, 254, 136, 44, 218, 66, 104, 54, 44, 234, 58, 41, 22, 130, 24, 165, 8, 114, 208, 133, 56, 232, 110, 146, 136, 212, 224, 67, 60, 61, 223, 232, 114, 182, 142, 244, 131, 168, 99, 85, 219, 67, 236, 155, 121, 88, 31, 200, 151, 204, 244, 57, 5, 194, 130, 176, 166, 17, 230, 87, 7, 229, 200, 197, 148, 166, 55, 237, 122, 108, 250, 87, 233, 187, 61, 125, 106, 15, 211, 175, 57, 59, 202, 45, 157, 75, 238, 151, 220, 142, 29, 47, 141, 206, 164, 246, 154, 217, 130, 61, 151, 254, 146, 87, 28, 145, 208, 91, 53, 152, 50, 12, 53, 246, 243, 237, 200, 72, 215, 135, 96, 194, 177, 142, 54, 106, 4, 218, 9, 68, 97, 109, 99, 255, 55, 217, 104, 120, 250, 42, 118, 46, 110, 244, 255, 135, 226, 53, 110, 244, 25, 56, 109, 40, 90, 167, 142, 197, 115, 236, 232, 17, 60, 8, 182, 247, 16, 222, 133, 131, 19, 241, 241, 28, 149, 42, 78, 187, 86, 197, 46, 159, 22, 28, 71, 100, 148, 207, 21, 135, 4, 53, 81, 84, 24, 181, 49, 170, 81, 195, 27, 15, 60, 55, 244, 34, 124, 81, 50, 192, 99, 176, 158, 125, 69, 50, 170, 104, 88, 47, 81, 56, 8, 13, 187, 158, 181, 127, 250, 63, 36, 141, 106, 36, 163, 30, 89, 172, 54, 79, 211, 151, 162, 59, 147, 61, 136, 253, 158, 122, 208, 35, 38, 112, 94, 97, 155, 227, 13, 62, 216, 132, 144, 14, 212, 168, 191, 142, 86, 129, 12, 62, 104, 33, 254, 224, 21, 65, 150, 128, 47, 16, 102, 76, 204, 31, 244, 131, 137, 162, 196, 63, 51, 29, 226, 199, 30, 170, 243, 130, 130, 245, 118, 180, 112, 58, 185, 28, 1, 115, 29, 95, 199, 55, 158, 55, 46, 58, 227, 237, 204, 238, 166, 91, 89, 120, 54, 155, 87, 207, 189, 247, 227, 114, 156, 35, 175, 55, 52, 141, 43, 58, 202, 232, 216, 240, 184, 140, 198, 237, 74, 127, 0, 61, 242, 220, 126, 1, 99, 244, 194, 0, 0, 21, 161, 73, 68, 65, 84, 120, 218, 237, 157, 233, 90, 219, 188, 214, 64, 223, 115, 36, 121, 80, 232, 0, 20, 136, 28, 74, 6, 91, 82, 40, 67, 40, 97, 48, 28, 8, 148, 161, 12, 165, 1, 26, 90, 104, 251, 126, 247, 127, 27, 159, 149, 132, 66, 153, 98, 39, 142, 228, 56, 90, 63, 218, 180, 15, 4, 123, 177, 179, 173, 97, 75, 250, 231, 159, 255, 252, 87, 35, 129, 127, 254, 3, 52, 50, 248, 231, 191, 170, 175, 96, 72, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 9, 45, 90, 18, 90, 180, 36, 180, 104, 73, 104, 209, 146, 208, 162, 37, 161, 69, 75, 66, 139, 150, 132, 22, 45, 137, 84, 137, 134, 170, 47, 224, 5, 146, 44, 26, 66, 100, 24, 200, 52, 45, 219, 68, 24, 103, 70, 94, 189, 126, 243, 118, 116, 172, 205, 248, 187, 137, 215, 147, 83, 35, 89, 76, 130, 47, 35, 166, 9, 16, 180, 96, 128, 234, 139, 126, 142, 100, 139, 22, 218, 136, 147, 155, 126, 63, 147, 47, 20, 75, 174, 71, 217, 61, 184, 91, 42, 207, 126, 24, 159, 155, 95, 88, 172, 32, 4, 32, 193, 72, 139, 238, 138, 32, 158, 13, 188, 244, 113, 185, 74, 57, 227, 156, 11, 183, 247, 85, 139, 255, 161, 148, 114, 74, 87, 242, 239, 23, 42, 4, 2, 29, 209, 17, 48, 209, 221, 235, 204, 234, 218, 58, 229, 109, 171, 156, 122, 129, 88, 151, 242, 38, 194, 176, 144, 207, 93, 63, 112, 239, 241, 149, 141, 183, 11, 155, 90, 116, 4, 44, 146, 67, 8, 67, 251, 127, 217, 247, 91, 235, 219, 156, 179, 103, 104, 249, 254, 243, 15, 74, 61, 207, 123, 183, 74, 16, 8, 210, 57, 81, 125, 19, 79, 144, 52, 209, 65, 72, 194, 26, 65, 149, 165, 185, 29, 143, 241, 231, 61, 63, 20, 221, 162, 180, 62, 179, 155, 129, 144, 160, 222, 47, 36, 110, 146, 38, 58, 80, 141, 28, 103, 250, 211, 74, 217, 167, 244, 121, 205, 207, 136, 118, 153, 87, 29, 221, 195, 40, 129, 166, 19, 39, 26, 129, 236, 254, 114, 145, 117, 228, 41, 209, 205, 215, 238, 246, 193, 72, 2, 77, 39, 74, 116, 96, 7, 231, 222, 143, 185, 188, 179, 231, 23, 41, 31, 78, 101, 12, 96, 153, 170, 239, 231, 62, 137, 18, 157, 65, 198, 210, 216, 103, 234, 246, 232, 153, 113, 230, 31, 77, 25, 199, 88, 245, 253, 220, 39, 81, 162, 225, 201, 86, 149, 246, 106, 185, 149, 74, 216, 202, 220, 9, 104, 231, 143, 68, 164, 145, 68, 137, 62, 221, 161, 84, 116, 66, 98, 113, 205, 138, 95, 50, 77, 197, 72, 139, 110, 131, 144, 101, 67, 132, 178, 227, 183, 141, 135, 94, 115, 116, 27, 239, 240, 21, 49, 48, 33, 137, 48, 157, 4, 209, 128, 56, 6, 112, 206, 10, 52, 94, 207, 1, 159, 247, 201, 113, 45, 25, 33, 157, 4, 209, 8, 65, 96, 228, 41, 139, 95, 52, 229, 91, 153, 132, 116, 203, 147, 32, 26, 212, 142, 95, 237, 112, 143, 121, 52, 110, 209, 204, 115, 199, 166, 73, 77, 245, 253, 9, 148, 139, 134, 36, 200, 162, 95, 55, 234, 252, 30, 241, 137, 14, 40, 206, 99, 51, 1, 49, 173, 92, 180, 109, 130, 204, 23, 151, 185, 253, 18, 29, 100, 164, 243, 139, 4, 152, 86, 46, 218, 2, 155, 111, 169, 127, 55, 92, 17, 179, 232, 224, 189, 92, 54, 147, 0, 211, 202, 69, 147, 203, 181, 32, 234, 188, 191, 76, 199, 154, 58, 130, 79, 203, 74, 96, 250, 118, 194, 102, 40, 69, 99, 155, 88, 139, 7, 177, 106, 125, 38, 125, 124, 59, 177, 81, 115, 2, 70, 153, 107, 165, 162, 9, 6, 151, 7, 62, 139, 55, 130, 159, 128, 211, 250, 183, 70, 208, 86, 55, 135, 85, 116, 14, 108, 94, 137, 207, 118, 223, 241, 41, 187, 114, 28, 52, 156, 17, 29, 220, 178, 149, 25, 103, 253, 143, 103, 214, 156, 234, 98, 87, 13, 27, 66, 117, 15, 69, 85, 162, 17, 48, 109, 146, 201, 203, 176, 124, 203, 97, 6, 91, 80, 217, 51, 81, 89, 68, 19, 96, 27, 223, 93, 41, 241, 220, 198, 95, 115, 44, 98, 168, 74, 31, 234, 68, 219, 149, 31, 117, 57, 137, 163, 5, 165, 244, 71, 206, 196, 195, 39, 218, 156, 247, 229, 69, 179, 168, 107, 10, 90, 235, 243, 192, 16, 154, 85, 140, 230, 41, 203, 209, 141, 133, 178, 84, 209, 76, 212, 217, 212, 175, 113, 240, 163, 9, 186, 71, 202, 69, 35, 148, 189, 145, 153, 159, 219, 81, 205, 54, 114, 112, 200, 68, 231, 102, 228, 123, 22, 188, 205, 153, 8, 12, 143, 104, 132, 78, 99, 153, 24, 236, 130, 211, 28, 68, 195, 35, 26, 110, 22, 99, 31, 60, 10, 67, 240, 51, 171, 35, 67, 20, 209, 182, 177, 198, 84, 136, 110, 254, 204, 124, 195, 28, 26, 209, 230, 106, 73, 157, 104, 255, 43, 0, 195, 34, 58, 187, 209, 154, 81, 81, 33, 186, 196, 214, 47, 161, 152, 14, 6, 45, 223, 105, 22, 93, 89, 246, 219, 101, 228, 10, 112, 185, 183, 101, 66, 100, 203, 190, 105, 5, 162, 241, 116, 219, 179, 18, 209, 30, 229, 55, 215, 6, 176, 101, 119, 197, 21, 136, 174, 44, 183, 61, 43, 17, 205, 131, 118, 229, 209, 133, 105, 166, 95, 180, 8, 232, 126, 204, 13, 134, 21, 29, 252, 84, 190, 32, 127, 97, 145, 116, 209, 200, 89, 102, 125, 153, 132, 141, 194, 207, 92, 45, 245, 162, 201, 228, 172, 122, 209, 229, 121, 233, 19, 181, 210, 69, 227, 67, 214, 159, 178, 130, 72, 140, 73, 31, 151, 150, 46, 250, 186, 220, 135, 98, 198, 232, 33, 189, 32, 123, 254, 80, 174, 104, 132, 209, 79, 218, 167, 66, 153, 8, 112, 70, 199, 77, 35, 197, 162, 29, 135, 92, 222, 243, 172, 76, 180, 199, 88, 97, 196, 145, 187, 150, 72, 170, 104, 195, 48, 207, 251, 86, 53, 26, 158, 230, 218, 141, 239, 216, 148, 58, 163, 37, 85, 180, 69, 26, 133, 4, 136, 230, 98, 165, 104, 97, 209, 145, 186, 146, 89, 170, 232, 227, 218, 66, 191, 170, 70, 163, 153, 22, 97, 189, 32, 119, 31, 21, 169, 162, 161, 249, 174, 111, 117, 208, 145, 69, 231, 229, 206, 133, 75, 21, 141, 54, 171, 205, 153, 111, 213, 162, 155, 172, 52, 172, 212, 138, 134, 31, 57, 77, 140, 232, 226, 190, 147, 90, 209, 228, 147, 82, 181, 127, 227, 230, 109, 153, 201, 67, 154, 104, 241, 228, 89, 252, 165, 218, 238, 29, 156, 175, 47, 166, 86, 244, 106, 89, 181, 222, 59, 40, 163, 147, 105, 20, 13, 77, 136, 192, 111, 213, 118, 239, 17, 60, 32, 222, 34, 8, 164, 117, 15, 229, 137, 14, 58, 98, 7, 138, 159, 127, 15, 184, 34, 16, 72, 91, 236, 41, 45, 117, 88, 16, 100, 102, 19, 37, 154, 207, 94, 32, 152, 62, 209, 38, 49, 167, 149, 55, 233, 254, 22, 237, 237, 97, 83, 90, 91, 90, 162, 104, 235, 125, 194, 68, 187, 95, 177, 157, 186, 28, 45, 158, 58, 227, 9, 19, 205, 62, 165, 83, 180, 115, 149, 40, 207, 1, 99, 21, 121, 27, 92, 201, 123, 24, 146, 139, 15, 170, 197, 62, 100, 35, 155, 194, 28, 109, 161, 205, 170, 106, 177, 15, 153, 189, 132, 210, 74, 195, 228, 165, 14, 184, 171, 218, 235, 99, 166, 83, 216, 188, 131, 112, 95, 181, 214, 199, 76, 202, 43, 118, 148, 38, 26, 161, 31, 170, 181, 62, 130, 191, 193, 233, 123, 24, 34, 242, 93, 181, 215, 199, 162, 183, 82, 40, 26, 146, 67, 213, 94, 31, 139, 30, 71, 233, 19, 109, 162, 228, 137, 102, 227, 32, 125, 205, 59, 147, 28, 38, 171, 183, 194, 154, 17, 173, 69, 107, 209, 93, 2, 201, 77, 242, 68, 111, 164, 48, 71, 107, 209, 178, 68, 227, 4, 138, 254, 73, 164, 85, 43, 13, 117, 68, 179, 95, 41, 20, 157, 196, 135, 33, 187, 194, 41, 20, 141, 18, 40, 250, 32, 157, 162, 85, 107, 125, 204, 152, 188, 243, 22, 228, 229, 232, 36, 70, 116, 26, 69, 3, 52, 154, 56, 209, 124, 52, 149, 139, 238, 255, 77, 158, 232, 127, 165, 221, 188, 196, 212, 97, 126, 85, 237, 245, 49, 95, 205, 244, 117, 88, 44, 115, 79, 181, 214, 199, 236, 165, 112, 114, 182, 6, 22, 87, 84, 123, 125, 200, 108, 54, 133, 93, 112, 27, 101, 10, 170, 197, 62, 100, 35, 39, 111, 97, 150, 196, 34, 71, 227, 74, 181, 216, 135, 140, 73, 60, 116, 72, 94, 135, 165, 217, 99, 73, 86, 195, 35, 15, 97, 10, 69, 67, 114, 154, 52, 209, 31, 161, 188, 213, 179, 18, 151, 86, 224, 61, 158, 28, 209, 162, 10, 208, 221, 133, 242, 86, 192, 73, 236, 176, 144, 74, 53, 65, 166, 61, 70, 11, 57, 81, 30, 159, 50, 209, 98, 147, 219, 202, 6, 101, 170, 182, 36, 125, 66, 180, 247, 43, 141, 162, 155, 219, 54, 255, 102, 52, 49, 17, 29, 92, 201, 169, 1, 229, 109, 37, 33, 81, 52, 66, 171, 190, 167, 218, 239, 31, 184, 87, 90, 69, 18, 247, 70, 145, 88, 123, 135, 208, 230, 70, 98, 2, 90, 76, 24, 46, 74, 12, 104, 185, 162, 241, 184, 106, 187, 247, 57, 34, 50, 183, 175, 146, 42, 154, 124, 76, 208, 210, 217, 210, 169, 188, 39, 161, 108, 209, 104, 100, 54, 49, 173, 14, 38, 119, 41, 184, 228, 136, 70, 121, 213, 122, 239, 216, 178, 141, 212, 138, 118, 200, 106, 66, 34, 154, 179, 210, 62, 73, 113, 68, 155, 57, 177, 96, 40, 1, 139, 224, 56, 171, 98, 36, 117, 199, 93, 169, 162, 1, 194, 223, 19, 32, 154, 122, 193, 5, 140, 34, 100, 165, 86, 52, 172, 128, 221, 114, 2, 134, 240, 234, 140, 85, 119, 1, 145, 186, 17, 172, 220, 136, 110, 128, 204, 145, 106, 203, 77, 252, 177, 138, 72, 209, 105, 20, 125, 203, 25, 75, 64, 55, 188, 196, 166, 100, 159, 76, 38, 93, 116, 102, 67, 125, 234, 160, 98, 224, 78, 50, 210, 69, 219, 243, 202, 61, 51, 230, 78, 74, 91, 49, 171, 76, 52, 193, 63, 21, 91, 14, 126, 209, 87, 242, 143, 52, 148, 191, 199, 191, 189, 47, 247, 28, 195, 7, 150, 57, 227, 37, 182, 32, 111, 211, 42, 101, 162, 9, 220, 84, 187, 205, 96, 16, 208, 203, 139, 72, 250, 113, 168, 242, 69, 19, 184, 176, 173, 52, 162, 153, 127, 102, 200, 171, 231, 80, 39, 218, 80, 187, 115, 102, 137, 249, 159, 140, 6, 146, 59, 208, 161, 66, 52, 34, 0, 92, 86, 101, 28, 111, 255, 52, 148, 21, 22, 135, 230, 152, 61, 252, 165, 174, 46, 164, 235, 243, 195, 115, 230, 172, 89, 25, 83, 215, 152, 94, 118, 134, 231, 224, 200, 28, 28, 81, 82, 89, 42, 126, 187, 179, 35, 160, 50, 52, 162, 49, 68, 95, 84, 204, 0, 80, 159, 179, 83, 4, 241, 208, 136, 70, 208, 28, 201, 171, 24, 46, 229, 236, 112, 4, 14, 209, 41, 202, 16, 88, 112, 100, 71, 65, 72, 243, 111, 215, 98, 3, 182, 225, 17, 109, 90, 182, 181, 32, 63, 121, 120, 254, 130, 109, 217, 112, 152, 68, 139, 90, 172, 121, 79, 106, 246, 224, 148, 250, 251, 22, 6, 114, 143, 95, 81, 44, 26, 154, 65, 80, 87, 222, 4, 189, 97, 121, 166, 93, 198, 39, 240, 177, 18, 201, 10, 69, 7, 158, 17, 110, 44, 75, 237, 31, 250, 249, 172, 196, 50, 221, 164, 136, 134, 54, 174, 128, 203, 101, 153, 162, 199, 23, 49, 81, 148, 55, 84, 137, 110, 202, 14, 58, 194, 240, 100, 135, 74, 168, 152, 22, 35, 118, 212, 253, 185, 107, 73, 124, 246, 37, 71, 116, 243, 145, 143, 47, 119, 130, 94, 132, 12, 209, 108, 231, 2, 85, 134, 86, 180, 117, 129, 79, 126, 249, 50, 86, 1, 120, 27, 187, 168, 1, 100, 54, 231, 18, 37, 58, 99, 34, 184, 249, 171, 185, 174, 165, 191, 182, 233, 205, 133, 229, 0, 99, 104, 69, 55, 171, 151, 22, 15, 2, 19, 174, 215, 55, 209, 156, 122, 254, 216, 165, 37, 187, 131, 146, 56, 209, 22, 105, 244, 187, 150, 151, 231, 179, 21, 233, 61, 193, 196, 137, 38, 199, 168, 178, 85, 234, 167, 231, 250, 81, 22, 219, 149, 161, 23, 13, 27, 24, 162, 127, 171, 253, 171, 19, 219, 62, 39, 6, 4, 38, 28, 110, 209, 98, 228, 33, 232, 38, 218, 171, 133, 224, 113, 216, 135, 7, 162, 199, 62, 79, 213, 204, 219, 178, 209, 161, 22, 45, 250, 137, 21, 76, 174, 143, 250, 82, 252, 72, 215, 174, 17, 169, 220, 150, 231, 14, 189, 104, 219, 192, 36, 247, 186, 15, 137, 186, 252, 177, 129, 8, 62, 214, 162, 209, 237, 107, 0, 17, 216, 189, 29, 249, 136, 109, 69, 192, 216, 46, 18, 123, 249, 152, 67, 45, 250, 9, 156, 223, 159, 69, 189, 7, 237, 249, 224, 95, 206, 248, 54, 243, 103, 127, 19, 114, 251, 28, 80, 79, 146, 68, 3, 176, 121, 84, 106, 199, 116, 47, 162, 169, 232, 106, 250, 71, 139, 248, 86, 116, 18, 72, 148, 232, 26, 180, 39, 63, 148, 169, 219, 251, 41, 113, 254, 250, 42, 2, 90, 244, 51, 32, 3, 97, 226, 76, 30, 148, 122, 21, 237, 111, 156, 230, 142, 29, 199, 208, 162, 159, 166, 169, 4, 145, 139, 247, 27, 219, 162, 173, 231, 69, 31, 255, 240, 68, 19, 145, 238, 188, 191, 108, 237, 208, 152, 28, 205, 201, 18, 221, 34, 232, 149, 227, 253, 241, 106, 179, 251, 210, 69, 92, 187, 249, 87, 134, 105, 90, 90, 116, 8, 144, 77, 50, 251, 115, 43, 204, 143, 154, 65, 234, 124, 118, 107, 58, 99, 5, 154, 213, 55, 231, 30, 146, 68, 209, 162, 28, 193, 48, 115, 147, 135, 219, 17, 77, 95, 205, 59, 205, 170, 13, 144, 128, 118, 243, 67, 18, 41, 90, 32, 36, 101, 63, 238, 148, 31, 148, 217, 208, 86, 43, 155, 123, 190, 152, 65, 15, 26, 204, 174, 43, 82, 140, 231, 127, 126, 55, 249, 234, 225, 74, 88, 45, 58, 44, 24, 103, 247, 207, 175, 182, 133, 221, 230, 136, 19, 247, 158, 24, 16, 225, 188, 188, 51, 51, 185, 89, 1, 72, 139, 238, 18, 12, 107, 198, 49, 172, 100, 247, 95, 175, 125, 43, 84, 75, 222, 95, 195, 123, 60, 248, 167, 87, 174, 126, 91, 251, 241, 117, 233, 2, 155, 193, 35, 20, 1, 45, 186, 75, 2, 123, 6, 9, 244, 33, 163, 146, 61, 153, 154, 124, 59, 250, 105, 124, 163, 205, 209, 218, 247, 153, 175, 11, 75, 23, 14, 6, 193, 195, 15, 145, 102, 177, 136, 22, 221, 29, 127, 198, 234, 111, 5, 6, 77, 63, 130, 177, 33, 250, 34, 173, 255, 125, 121, 209, 188, 22, 29, 146, 71, 162, 91, 219, 171, 96, 252, 71, 179, 22, 29, 11, 143, 69, 223, 190, 132, 127, 232, 244, 253, 170, 239, 225, 150, 36, 136, 134, 127, 254, 120, 192, 35, 209, 176, 89, 30, 105, 153, 48, 154, 104, 61, 76, 218, 66, 108, 46, 108, 61, 39, 250, 225, 215, 70, 217, 202, 68, 204, 253, 154, 72, 230, 198, 152, 207, 147, 4, 209, 129, 14, 248, 228, 103, 188, 215, 143, 190, 72, 232, 4, 0, 233, 171, 100, 159, 34, 9, 162, 161, 5, 97, 216, 136, 142, 132, 248, 246, 230, 84, 150, 234, 59, 4, 42, 231, 12, 239, 189, 62, 27, 29, 61, 123, 242, 107, 186, 23, 253, 103, 172, 99, 127, 107, 107, 95, 209, 45, 254, 133, 42, 209, 65, 35, 141, 64, 11, 89, 168, 178, 255, 238, 64, 12, 94, 140, 45, 17, 18, 116, 62, 226, 234, 112, 136, 167, 102, 173, 102, 93, 28, 137, 247, 46, 140, 158, 100, 136, 97, 96, 18, 160, 42, 141, 168, 75, 29, 129, 9, 132, 175, 71, 215, 203, 172, 53, 25, 187, 50, 133, 172, 7, 31, 242, 30, 68, 35, 96, 89, 208, 88, 45, 180, 222, 155, 251, 235, 115, 187, 200, 129, 168, 82, 145, 119, 242, 202, 223, 168, 139, 232, 160, 127, 247, 113, 140, 139, 113, 34, 79, 12, 26, 137, 98, 151, 17, 140, 98, 235, 66, 27, 53, 187, 146, 231, 205, 121, 94, 49, 66, 82, 247, 86, 14, 78, 179, 198, 93, 131, 48, 245, 219, 72, 252, 17, 177, 144, 47, 54, 199, 134, 88, 123, 210, 155, 51, 183, 240, 213, 105, 111, 193, 211, 18, 210, 149, 232, 182, 72, 104, 124, 92, 23, 195, 168, 205, 49, 85, 175, 53, 91, 179, 189, 53, 221, 184, 253, 178, 180, 139, 14, 252, 193, 218, 177, 177, 120, 54, 86, 124, 106, 30, 234, 219, 190, 41, 176, 58, 119, 70, 158, 253, 1, 160, 246, 127, 196, 132, 198, 201, 161, 235, 182, 127, 145, 247, 234, 68, 234, 99, 95, 178, 150, 131, 108, 19, 167, 93, 52, 64, 24, 147, 145, 55, 31, 158, 41, 180, 243, 202, 51, 39, 200, 128, 77, 219, 221, 137, 174, 57, 21, 11, 27, 211, 115, 190, 199, 233, 221, 136, 53, 111, 125, 112, 132, 234, 194, 219, 77, 51, 120, 58, 164, 94, 52, 0, 123, 91, 179, 79, 175, 15, 106, 234, 240, 10, 115, 23, 70, 208, 161, 235, 50, 162, 197, 161, 76, 214, 197, 76, 145, 178, 123, 115, 232, 127, 34, 154, 83, 177, 90, 183, 144, 159, 134, 88, 234, 230, 209, 64, 129, 232, 253, 124, 213, 127, 166, 188, 174, 93, 183, 75, 103, 127, 236, 25, 40, 130, 232, 102, 79, 251, 246, 31, 4, 156, 204, 20, 61, 246, 215, 12, 250, 157, 232, 82, 115, 223, 44, 191, 124, 24, 252, 4, 185, 247, 45, 111, 19, 88, 2, 108, 84, 217, 91, 235, 92, 28, 35, 154, 122, 163, 11, 134, 25, 180, 177, 77, 179, 34, 22, 232, 119, 124, 107, 4, 45, 139, 144, 154, 101, 0, 114, 150, 119, 95, 122, 235, 128, 214, 180, 227, 214, 153, 129, 177, 129, 58, 189, 247, 192, 137, 22, 149, 22, 112, 247, 128, 133, 40, 247, 106, 218, 88, 57, 248, 146, 65, 65, 255, 133, 88, 157, 183, 2, 20, 17, 29, 60, 98, 109, 224, 44, 254, 190, 169, 187, 33, 103, 206, 235, 99, 171, 14, 73, 225, 9, 157, 38, 57, 59, 160, 252, 222, 199, 248, 121, 209, 205, 141, 180, 61, 182, 61, 58, 189, 136, 76, 220, 185, 47, 23, 36, 153, 160, 89, 110, 108, 174, 30, 20, 69, 53, 170, 231, 134, 50, 237, 49, 119, 121, 26, 73, 219, 12, 182, 239, 162, 111, 3, 114, 228, 123, 89, 204, 167, 134, 168, 20, 21, 191, 13, 183, 212, 92, 0, 80, 92, 155, 204, 86, 96, 167, 45, 9, 130, 79, 11, 113, 86, 183, 138, 226, 247, 72, 121, 184, 242, 106, 145, 66, 74, 172, 124, 52, 146, 22, 209, 102, 208, 19, 54, 81, 101, 241, 45, 111, 19, 234, 99, 125, 143, 242, 207, 209, 211, 205, 12, 14, 18, 177, 152, 46, 108, 142, 86, 180, 86, 115, 181, 94, 1, 228, 100, 71, 190, 172, 221, 20, 74, 209, 246, 89, 105, 117, 100, 40, 95, 153, 9, 84, 227, 238, 91, 57, 137, 17, 29, 152, 134, 70, 99, 126, 214, 239, 86, 180, 160, 120, 179, 252, 110, 254, 108, 111, 41, 155, 67, 183, 231, 75, 155, 54, 116, 46, 54, 175, 87, 39, 215, 198, 214, 87, 194, 229, 138, 39, 76, 211, 224, 35, 182, 242, 59, 139, 131, 198, 125, 191, 119, 71, 239, 187, 104, 195, 36, 151, 135, 180, 125, 99, 93, 137, 110, 234, 8, 122, 141, 43, 133, 141, 171, 241, 119, 115, 109, 70, 63, 29, 236, 220, 204, 138, 56, 166, 174, 71, 163, 191, 111, 251, 114, 130, 246, 54, 59, 188, 70, 118, 109, 224, 35, 26, 101, 206, 235, 172, 238, 245, 32, 186, 233, 154, 62, 245, 189, 189, 212, 81, 223, 93, 142, 203, 234, 175, 51, 3, 157, 58, 130, 20, 106, 128, 213, 141, 110, 85, 200, 131, 123, 235, 175, 157, 10, 52, 240, 128, 138, 6, 200, 88, 92, 243, 188, 126, 44, 212, 140, 89, 52, 229, 222, 120, 6, 144, 126, 14, 128, 244, 85, 180, 115, 86, 12, 58, 6, 42, 55, 64, 15, 41, 58, 72, 77, 94, 241, 180, 98, 244, 209, 116, 95, 68, 223, 38, 187, 252, 182, 136, 22, 213, 26, 195, 137, 14, 254, 26, 63, 233, 163, 233, 126, 136, 14, 158, 42, 132, 216, 198, 215, 217, 222, 87, 87, 201, 50, 221, 250, 107, 253, 61, 52, 13, 220, 185, 166, 47, 41, 162, 131, 86, 46, 114, 42, 223, 203, 61, 181, 10, 84, 136, 102, 245, 181, 70, 208, 193, 2, 3, 35, 154, 216, 96, 115, 214, 237, 173, 249, 165, 6, 119, 125, 97, 144, 34, 218, 114, 94, 87, 91, 145, 50, 80, 162, 169, 136, 13, 247, 183, 211, 151, 83, 225, 98, 23, 13, 109, 155, 56, 91, 221, 116, 138, 147, 194, 167, 70, 197, 185, 219, 98, 58, 177, 162, 109, 27, 141, 92, 249, 201, 57, 209, 62, 42, 193, 133, 255, 92, 68, 6, 73, 188, 232, 154, 181, 80, 77, 126, 203, 249, 101, 214, 23, 238, 182, 34, 74, 172, 232, 198, 155, 106, 208, 118, 246, 7, 52, 162, 197, 234, 47, 202, 74, 254, 28, 78, 178, 104, 177, 168, 7, 47, 207, 14, 98, 107, 227, 158, 234, 230, 72, 160, 63, 142, 97, 188, 59, 243, 198, 26, 209, 199, 240, 98, 231, 239, 130, 149, 193, 163, 125, 241, 254, 97, 198, 136, 117, 226, 54, 86, 209, 181, 235, 13, 175, 249, 233, 27, 108, 209, 173, 57, 203, 157, 17, 39, 177, 162, 95, 221, 220, 93, 173, 106, 97, 189, 136, 22, 27, 120, 82, 113, 254, 111, 18, 69, 35, 66, 78, 7, 57, 146, 239, 187, 110, 85, 183, 178, 141, 249, 24, 43, 199, 98, 19, 237, 152, 31, 183, 83, 34, 250, 207, 195, 188, 248, 222, 138, 45, 125, 196, 38, 58, 247, 187, 52, 208, 15, 193, 135, 162, 197, 141, 208, 242, 151, 216, 10, 215, 99, 19, 61, 51, 144, 131, 72, 47, 136, 110, 222, 73, 233, 60, 174, 98, 200, 88, 68, 59, 24, 79, 20, 89, 122, 68, 223, 119, 62, 97, 24, 177, 164, 143, 88, 68, 99, 48, 225, 178, 1, 111, 63, 63, 135, 55, 129, 98, 9, 234, 120, 68, 79, 112, 239, 254, 7, 46, 77, 120, 108, 34, 57, 162, 39, 92, 207, 99, 189, 212, 109, 36, 24, 223, 243, 38, 18, 33, 186, 98, 144, 55, 252, 30, 170, 197, 196, 14, 247, 188, 239, 4, 244, 220, 248, 232, 89, 52, 50, 39, 188, 84, 139, 22, 179, 46, 163, 168, 214, 107, 207, 165, 103, 209, 246, 191, 119, 117, 117, 41, 21, 29, 36, 234, 83, 245, 17, 61, 191, 237, 165, 94, 52, 229, 238, 111, 216, 99, 72, 247, 36, 26, 215, 208, 130, 106, 9, 82, 224, 244, 243, 148, 245, 215, 121, 147, 145, 231, 20, 123, 18, 109, 225, 221, 117, 213, 14, 100, 153, 94, 63, 201, 89, 202, 68, 155, 187, 31, 82, 153, 42, 158, 50, 237, 22, 78, 128, 50, 209, 185, 159, 170, 239, 95, 38, 99, 153, 230, 82, 14, 201, 162, 33, 48, 76, 188, 86, 98, 117, 213, 183, 47, 15, 239, 200, 128, 206, 35, 211, 125, 143, 104, 8, 193, 76, 137, 43, 56, 219, 91, 161, 233, 115, 147, 56, 210, 83, 7, 68, 171, 190, 138, 51, 212, 85, 178, 61, 137, 101, 167, 14, 8, 204, 205, 109, 94, 31, 174, 136, 14, 76, 55, 31, 136, 68, 158, 104, 8, 173, 198, 134, 56, 48, 37, 190, 195, 105, 6, 1, 151, 93, 57, 0, 255, 15, 75, 19, 13, 1, 113, 214, 104, 42, 7, 250, 95, 196, 99, 222, 86, 112, 243, 24, 203, 18, 109, 146, 202, 105, 189, 185, 85, 195, 112, 137, 14, 240, 63, 2, 40, 47, 162, 77, 188, 88, 186, 221, 3, 94, 245, 157, 203, 166, 190, 7, 228, 61, 12, 141, 205, 159, 237, 19, 173, 134, 79, 52, 187, 89, 132, 210, 68, 147, 124, 107, 223, 141, 161, 20, 205, 214, 228, 137, 254, 82, 31, 94, 209, 156, 213, 39, 129, 12, 209, 98, 119, 129, 2, 243, 82, 61, 254, 252, 34, 148, 221, 228, 16, 130, 253, 23, 77, 200, 39, 47, 229, 3, 253, 157, 120, 231, 56, 145, 87, 4, 68, 22, 141, 208, 251, 180, 207, 17, 118, 196, 159, 52, 156, 190, 139, 198, 35, 85, 119, 216, 69, 211, 226, 82, 228, 115, 198, 163, 136, 110, 45, 190, 91, 254, 203, 243, 80, 138, 102, 222, 97, 95, 115, 52, 180, 77, 3, 205, 247, 245, 84, 250, 65, 193, 159, 183, 96, 180, 125, 100, 35, 165, 14, 219, 50, 51, 67, 53, 169, 242, 60, 55, 217, 136, 53, 234, 145, 68, 67, 72, 206, 7, 125, 13, 97, 76, 184, 19, 57, 28, 105, 213, 86, 36, 209, 38, 190, 172, 15, 225, 64, 210, 147, 212, 175, 141, 62, 138, 118, 198, 24, 79, 254, 206, 61, 253, 71, 24, 56, 140, 182, 9, 100, 52, 209, 243, 172, 60, 180, 45, 141, 7, 184, 124, 62, 82, 129, 122, 36, 209, 75, 3, 190, 42, 54, 94, 62, 103, 251, 34, 218, 12, 30, 178, 115, 94, 58, 171, 250, 187, 195, 157, 1, 226, 80, 208, 184, 69, 91, 152, 236, 81, 74, 7, 122, 85, 108, 204, 84, 175, 9, 238, 188, 233, 114, 228, 212, 129, 201, 59, 45, 250, 111, 230, 34, 236, 148, 23, 90, 52, 129, 39, 61, 109, 228, 154, 70, 170, 35, 48, 116, 221, 116, 104, 209, 200, 57, 212, 162, 31, 80, 206, 59, 125, 24, 235, 216, 227, 186, 197, 241, 16, 127, 55, 116, 19, 47, 180, 232, 202, 152, 22, 253, 16, 234, 45, 95, 196, 46, 250, 21, 29, 178, 242, 175, 16, 120, 220, 155, 138, 85, 180, 56, 42, 44, 95, 210, 158, 31, 66, 185, 127, 36, 202, 61, 99, 20, 109, 191, 175, 234, 33, 142, 199, 112, 238, 77, 218, 225, 90, 30, 225, 82, 7, 54, 242, 3, 186, 237, 87, 223, 201, 135, 220, 161, 55, 148, 104, 2, 167, 135, 168, 178, 63, 26, 254, 52, 12, 117, 234, 103, 40, 209, 57, 148, 247, 122, 191, 164, 116, 66, 143, 80, 46, 54, 209, 246, 101, 177, 247, 43, 74, 43, 213, 37, 43, 54, 209, 134, 158, 192, 122, 30, 247, 60, 212, 65, 114, 29, 69, 139, 55, 185, 248, 160, 250, 110, 146, 76, 33, 27, 166, 217, 209, 89, 116, 240, 46, 251, 58, 67, 191, 0, 221, 39, 33, 42, 15, 58, 139, 54, 0, 42, 168, 190, 151, 100, 243, 185, 66, 30, 28, 104, 222, 149, 104, 68, 246, 84, 223, 73, 210, 121, 101, 160, 142, 19, 45, 29, 69, 227, 26, 62, 84, 125, 35, 73, 103, 220, 57, 142, 65, 180, 117, 50, 171, 250, 70, 146, 78, 113, 164, 243, 65, 159, 157, 155, 119, 232, 141, 30, 228, 232, 196, 105, 165, 163, 233, 142, 162, 29, 227, 155, 234, 219, 72, 62, 7, 149, 142, 91, 182, 117, 20, 109, 238, 233, 206, 74, 71, 188, 235, 222, 115, 52, 124, 167, 27, 209, 157, 153, 235, 216, 144, 238, 40, 186, 242, 89, 245, 77, 12, 2, 235, 141, 158, 69, 159, 233, 194, 243, 16, 148, 191, 246, 44, 122, 84, 245, 61, 12, 2, 156, 173, 129, 14, 221, 240, 23, 69, 139, 194, 50, 221, 91, 9, 197, 183, 214, 204, 106, 183, 17, 109, 89, 187, 186, 205, 17, 10, 127, 31, 246, 32, 26, 215, 192, 140, 234, 59, 24, 20, 206, 141, 30, 68, 27, 53, 60, 0, 103, 32, 39, 131, 111, 29, 70, 165, 95, 206, 209, 104, 186, 172, 250, 6, 6, 133, 234, 210, 203, 115, 180, 47, 138, 182, 42, 191, 117, 25, 88, 72, 234, 63, 108, 235, 165, 5, 158, 47, 138, 70, 198, 178, 167, 69, 135, 228, 19, 32, 93, 139, 198, 155, 43, 190, 22, 29, 146, 98, 3, 116, 31, 209, 251, 37, 93, 225, 31, 18, 151, 79, 229, 186, 22, 109, 126, 87, 125, 249, 131, 196, 40, 120, 105, 13, 254, 139, 162, 27, 87, 58, 152, 195, 243, 43, 243, 210, 94, 7, 47, 138, 94, 170, 235, 210, 198, 208, 120, 254, 200, 75, 237, 187, 23, 68, 67, 176, 235, 106, 209, 161, 161, 222, 94, 208, 241, 120, 118, 221, 225, 179, 162, 33, 128, 214, 154, 167, 83, 71, 120, 220, 137, 255, 65, 244, 236, 186, 195, 231, 35, 26, 34, 227, 70, 7, 116, 104, 56, 163, 203, 93, 69, 52, 182, 200, 146, 55, 108, 27, 68, 247, 0, 101, 188, 120, 129, 96, 244, 230, 157, 97, 162, 5, 170, 69, 135, 38, 112, 181, 114, 141, 158, 63, 234, 233, 249, 136, 70, 224, 95, 174, 69, 135, 70, 44, 14, 60, 37, 40, 186, 104, 132, 200, 26, 27, 174, 125, 184, 123, 66, 244, 160, 191, 99, 0, 34, 139, 134, 166, 179, 161, 69, 135, 71, 136, 254, 149, 179, 186, 104, 117, 88, 141, 178, 222, 63, 41, 60, 66, 116, 245, 162, 6, 35, 139, 198, 96, 183, 164, 69, 71, 195, 125, 101, 119, 241, 48, 36, 95, 93, 61, 114, 23, 13, 58, 111, 118, 241, 48, 36, 231, 122, 203, 136, 168, 188, 53, 163, 167, 14, 136, 215, 180, 232, 168, 28, 193, 232, 15, 67, 152, 251, 73, 181, 231, 136, 172, 147, 46, 68, 111, 22, 60, 45, 58, 34, 197, 70, 23, 205, 187, 221, 109, 170, 61, 71, 164, 116, 25, 61, 71, 131, 41, 223, 211, 162, 163, 65, 189, 93, 16, 93, 244, 71, 79, 143, 70, 71, 196, 245, 230, 163, 143, 222, 193, 115, 170, 69, 71, 196, 247, 222, 68, 23, 109, 126, 210, 35, 119, 81, 241, 248, 90, 244, 212, 97, 235, 197, 88, 145, 225, 236, 32, 122, 171, 195, 188, 209, 1, 29, 21, 78, 55, 186, 152, 97, 249, 172, 69, 71, 133, 210, 66, 37, 178, 232, 236, 182, 234, 203, 30, 60, 40, 91, 217, 140, 44, 250, 68, 175, 169, 136, 12, 103, 165, 221, 200, 162, 247, 93, 213, 151, 61, 136, 248, 39, 209, 69, 235, 5, 179, 93, 224, 238, 69, 23, 173, 139, 103, 34, 67, 25, 157, 138, 46, 122, 216, 206, 252, 142, 1, 159, 241, 253, 200, 162, 127, 112, 166, 99, 58, 34, 156, 211, 55, 209, 69, 139, 99, 176, 53, 145, 224, 180, 11, 209, 255, 106, 209, 145, 233, 74, 244, 27, 166, 83, 71, 84, 186, 18, 61, 161, 250, 170, 7, 16, 202, 217, 247, 200, 162, 245, 34, 240, 232, 116, 37, 250, 173, 234, 171, 30, 64, 180, 104, 73, 188, 36, 250, 255, 1, 16, 55, 123, 157, 80, 38, 104, 157, 0, 0, 0, 86, 101, 88, 73, 102, 77, 77, 0, 42, 0, 0, 0, 16, 69, 120, 105, 102, 77, 101, 116, 97, 0, 4, 1, 26, 0, 5, 0, 0, 0, 1, 0, 0, 0, 70, 1, 27, 0, 5, 0, 0, 0, 1, 0, 0, 0, 78, 1, 40, 0, 3, 0, 0, 0, 1, 0, 2, 0, 0, 2, 19, 0, 3, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 44, 0, 0, 0, 1, 0, 0, 1, 44, 0, 0, 0, 1, 82, 113, 247, 29, 0, 0, 0, 37, 116, 69, 88, 116, 100, 97, 116, 101, 58, 99, 114, 101, 97, 116, 101, 0, 50, 48, 50, 52, 45, 48, 49, 45, 48, 54, 84, 50, 48, 58, 51, 50, 58, 48, 49, 43, 48, 48, 58, 48, 48, 14, 64, 101, 107, 0, 0, 0, 37, 116, 69, 88, 116, 100, 97, 116, 101, 58, 109, 111, 100, 105, 102, 121, 0, 50, 48, 50, 52, 45, 48, 49, 45, 48, 54, 84, 50, 48, 58, 51, 50, 58, 48, 49, 43, 48, 48, 58, 48, 48, 127, 29, 221, 215, 0, 0, 0, 40, 116, 69, 88, 116, 100, 97, 116, 101, 58, 116, 105, 109, 101, 115, 116, 97, 109, 112, 0, 50, 48, 50, 52, 45, 48, 49, 45, 48, 54, 84, 50, 48, 58, 51, 50, 58, 48, 49, 43, 48, 48, 58, 48, 48, 40, 8, 252, 8, 0, 0, 0, 23, 116, 69, 88, 116, 101, 120, 105, 102, 58, 89, 67, 98, 67, 114, 80, 111, 115, 105, 116, 105, 111, 110, 105, 110, 103, 0, 49, 172, 15, 128, 99, 0, 0, 0, 36, 116, 69, 88, 116, 120, 109, 112, 58, 67, 114, 101, 97, 116, 111, 114, 84, 111, 111, 108, 0, 65, 100, 111, 98, 101, 32, 83, 116, 111, 99, 107, 32, 80, 108, 97, 116, 102, 111, 114, 109, 54, 126, 66, 113, 0, 0, 0, 61, 116, 69, 88, 116, 120, 109, 112, 77, 77, 58, 68, 111, 99, 117, 109, 101, 110, 116, 73, 68, 0, 120, 109, 112, 46, 105, 105, 100, 58, 50, 51, 57, 52, 50, 99, 56, 56, 45, 99, 51, 51, 101, 45, 52, 52, 55, 102, 45, 97, 102, 100, 53, 45, 52, 56, 53, 57, 99, 56, 49, 50, 50, 56, 54, 50, 69, 221, 215, 60, 0, 0, 0, 71, 116, 69, 88, 116, 120, 109, 112, 77, 77, 58, 73, 110, 115, 116, 97, 110, 99, 101, 73, 68, 0, 97, 100, 111, 98, 101, 58, 100, 111, 99, 105, 100, 58, 115, 116, 111, 99, 107, 58, 56, 97, 54, 57, 97, 101, 54, 57, 45, 57, 100, 97, 100, 45, 52, 55, 101, 55, 45, 56, 98, 98, 98, 45, 55, 51, 56, 51, 49, 98, 49, 51, 100, 98, 54, 53, 244, 212, 81, 76, 0, 0, 0, 52, 116, 69, 88, 116, 120, 109, 112, 77, 77, 58, 79, 114, 105, 103, 105, 110, 97, 108, 68, 111, 99, 117, 109, 101, 110, 116, 73, 68, 0, 97, 100, 111, 98, 101, 58, 100, 111, 99, 105, 100, 58, 115, 116, 111, 99, 107, 58, 51, 52, 57, 52, 57, 55, 57, 51, 51, 76, 107, 208, 67, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, 0m, "", "000000000", false, "admin@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Anuncios_UserId",
                table: "Anuncios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anuncios");

            migrationBuilder.DropTable(
                name: "AnuncioValidado");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Denuncias");

            migrationBuilder.DropTable(
                name: "Elogios");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "HistoricoDeCompra");

            migrationBuilder.DropTable(
                name: "Imagens_Anuncios");

            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "MovimentosDaCarteira");

            migrationBuilder.DropTable(
                name: "PropostasDeCompra");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
