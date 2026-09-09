# Tradeguard

Plataforma web para publicação e gestão de anúncios, propostas de compra e comunicação entre utilizadores.

O projeto foi desenvolvido com ASP.NET Core MVC e inclui autenticação, moderação de anúncios e funcionalidades de apoio a transações entre utilizadores.

## Funcionalidades

- Registo e autenticação de utilizadores
- Publicação, validação e gestão de anúncios
- Favoritos e propostas de compra
- Mensagens entre utilizadores
- Histórico de compras
- Avaliações, elogios e denúncias
- Área de administração

## Tecnologias

- C# e .NET 8
- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core e SQL Server
- SignalR
- Razor Views

## Executar localmente

### Pré-requisitos

- SDK .NET 8
- SQL Server

### Preparação

```bash
git clone https://github.com/Rnarciso15/Tradeguard.git
cd Tradeguard
dotnet restore
dotnet run --project Tradeguard2
```

Configure a ligação à base de dados e quaisquer valores sensíveis através de variáveis de ambiente ou `dotnet user-secrets`.

## Estado

Projeto académico preservado para demonstrar competências em ASP.NET Core MVC, autenticação e persistência de dados. Não está preparado para utilização em produção sem uma revisão técnica e de segurança.

