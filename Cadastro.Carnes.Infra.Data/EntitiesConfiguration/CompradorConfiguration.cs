using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Comprador para o Entity Framework Core.
    /// </summary>
    public class CompradorConfiguration : IEntityTypeConfiguration<Comprador>
    {
        /// <summary>
        /// Define as regras de mapeamento da entidade Comprador para o banco de dados.
        /// </summary>
        public void Configure(EntityTypeBuilder<Comprador> builder)
        {
            // Define a chave primária da tabela
            builder.HasKey(a => a.Id);

            // Campo Nome é obrigatório e limitado a 100 caracteres
            builder.Property(p => p.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            // Campo Documento é obrigatório e limitado a 14 caracteres (pode ser CPF ou CNPJ sem máscara)
            builder.Property(p => p.Documento)
                   .HasMaxLength(14)
                   .IsRequired();

            // Campo CidadeId é obrigatório, referência para a cidade do comprador
            builder.Property(p => p.CidadeId)
                   .IsRequired();

            // Relacionamento: Cada comprador tem uma cidade (CidadeId é FK)
            builder.HasOne(e => e.Cidade)
                   .WithMany() // Não define coleção de compradores na cidade
                   .HasForeignKey(e => e.CidadeId);

            // Fim da configuração. Pronto para rodar o migration.
        }
    }
}
