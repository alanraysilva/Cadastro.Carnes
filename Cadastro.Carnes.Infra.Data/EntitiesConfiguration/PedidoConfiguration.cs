using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.Data).IsRequired();
            builder.Property(p => p.CompradorId).IsRequired();
            builder.Property(p => p.Total).HasPrecision(10, 2);

            builder.HasOne(e => e.Comprador).WithMany().HasForeignKey(e => e.CompradorId);
        }
    }
}
