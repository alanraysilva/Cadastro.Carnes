using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade ItemPedido para o Entity Framework Core.
    /// </summary>
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        /// <summary>
        /// Define o mapeamento da entidade ItemPedido para a tabela do banco de dados.
        /// </summary>
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            // Chave primária da tabela (Id do item)
            builder.HasKey(a => a.Id);

            // PedidoId é obrigatório (chave estrangeira para Pedido)
            builder.Property(p => p.PedidoId).IsRequired();

            // CarneId é obrigatório (chave estrangeira para Carne)
            builder.Property(p => p.CarneId).IsRequired();

            // Quantidade é obrigatória (não existe item sem quantidade)
            builder.Property(p => p.Quantidade).IsRequired();

            // MoedaId é obrigatório (sempre tem moeda, mesmo que seja BRL)
            builder.Property(p => p.MoedaId).IsRequired();

            // Valor com precisão de 10 casas e 2 decimais (exemplo: 12345678.90)
            builder.Property(p => p.Valor).HasPrecision(10, 2);

            // Relacionamento com Pedido: muitos itens para um pedido
            builder.HasOne(e => e.Pedido)
                   .WithMany(p => p.Itens)
                   .HasForeignKey(e => e.PedidoId);

            // Relacionamento com Carne: cada item aponta para uma carne (não define coleção de itens na Carne)
            builder.HasOne(e => e.Carne)
                   .WithMany()
                   .HasForeignKey(e => e.CarneId);

            // Relacionamento com Moeda: cada item tem uma moeda (não define coleção de itens na Moeda)
            builder.HasOne(e => e.Moeda)
                   .WithMany()
                   .HasForeignKey(e => e.MoedaId);

            // Fim das configurações do ItemPedido.
        }
    }
}
