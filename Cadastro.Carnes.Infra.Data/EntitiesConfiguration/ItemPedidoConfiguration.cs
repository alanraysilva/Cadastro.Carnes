using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.PedidoId).IsRequired();
            builder.Property(p => p.CarneId).IsRequired();
            builder.Property(p => p.Quantidade).IsRequired();
            builder.Property(p => p.MoedaId).IsRequired();
            builder.Property(p => p.Valor).HasPrecision(10, 2);


            builder.HasOne(e => e.Pedido).WithMany(p => p.Itens).HasForeignKey(e => e.PedidoId);
            builder.HasOne(e => e.Carne).WithMany().HasForeignKey(e => e.CarneId);
            builder.HasOne(e => e.Moeda).WithMany().HasForeignKey(e => e.MoedaId);
        }
    }
}
