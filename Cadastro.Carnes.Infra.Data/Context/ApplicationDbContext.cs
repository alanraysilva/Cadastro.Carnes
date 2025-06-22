using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Context
{
    /// <summary>
    /// Contexto principal do Entity Framework, responsável por mapear as entidades do domínio para o banco de dados.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor padrão, recebe as opções de configuração do contexto.
        /// </summary>
        /// <param name="options">Opções do contexto, normalmente configuradas via injeção de dependência.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        /// <summary>
        /// Método chamado ao criar o modelo do banco.
        /// Aplica todas as configurações de entidades definidas no assembly.
        /// </summary>
        /// <param name="builder">ModelBuilder para configuração das entidades.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Aplica automaticamente todas as configurações de mapeamento (IEntityTypeConfiguration) do projeto
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        // DbSets representam as tabelas do banco de dados
        public DbSet<Carne> Carne { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Comprador> Comprador { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Moeda> Moeda { get; set; }
        public DbSet<Origem> Origem { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
    }
}
