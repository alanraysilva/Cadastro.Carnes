using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Moeda para o Entity Framework Core.
    /// </summary>
    public class MoedaConfiguration : IEntityTypeConfiguration<Moeda>
    {
        /// <summary>
        /// Define como a tabela Moeda será criada no banco.
        /// </summary>
        public void Configure(EntityTypeBuilder<Moeda> builder)
        {
            // Chave primária (Id da moeda)
            builder.HasKey(a => a.Id);

            // Nome da moeda é obrigatório e limitado a 100 caracteres
            builder.Property(p => p.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            // Sigla da moeda (ex: BRL, USD) obrigatória e limitada a 10 caracteres
            builder.Property(p => p.Sigla)
                   .HasMaxLength(10)
                   .IsRequired();

            // Fim das configurações da Moeda.
        }
    }
}
