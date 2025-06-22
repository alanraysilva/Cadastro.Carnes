using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cadastro.Carnes.Infra.Data.Context
{
    /// <summary>
    /// Fábrica para criação do ApplicationDbContext em tempo de design.
    /// Necessária para comandos do EF Core como Migrations e Update-Database via CLI.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Cria uma nova instância do ApplicationDbContext para uso em tempo de design.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando (normalmente não utilizados).</param>
        /// <returns>Instância de ApplicationDbContext configurada.</returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Carrega as configurações a partir do appsettings.json do projeto principal
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Usa o diretório atual como base
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // Configura o contexto para usar SQL Server com a connection string definida
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // Retorna o contexto configurado
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
