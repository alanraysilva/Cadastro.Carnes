using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Origem para o Entity Framework Core.
    /// </summary>
    public class OrigemConfiguration : IEntityTypeConfiguration<Origem>
    {
        /// <summary>
        /// Define como a tabela Origem será criada no banco de dados.
        /// </summary>
        public void Configure(EntityTypeBuilder<Origem> builder)
        {
            // Chave primária da tabela (Id).
            builder.HasKey(a => a.Id);

            // Nome da origem é obrigatório e limitado a 100 caracteres.
            builder.Property(p => p.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            // Fim da configuração. Não tem segredo!
        }
    }
}
