using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Carnes.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Carne> Carne { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Comprador> Comprador { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Moeda> Moeda { get; set; }
        public DbSet<Origem> Origem { get; set; }
        public DbSet<Pedido> Pedido { get; set; }

    }
}
