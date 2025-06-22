using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Pedido para o Entity Framework Core.
    /// Define como a tabela será criada no banco e seus relacionamentos.
    /// </summary>
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        /// <summary>
        /// Método responsável por mapear os campos e relacionamentos da entidade Pedido.
        /// </summary>
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            // Define a chave primária (campo Id).
            builder.HasKey(a => a.Id);

            // Campo Data é obrigatório.
            builder.Property(p => p.Data).IsRequired();

            // Campo CompradorId é obrigatório (relacionamento com Comprador).
            builder.Property(p => p.CompradorId).IsRequired();

            // Campo Total com precisão de 10 casas e 2 decimais (ex: 99999999.99).
            builder.Property(p => p.Total).HasPrecision(10, 2);

            // Relacionamento: Um pedido tem um comprador (FK CompradorId).
            builder.HasOne(e => e.Comprador)
                   .WithMany()
                   .HasForeignKey(e => e.CompradorId);

            // Fim da configuração. Simples, direto e sem firula.
        }
    }
}
