using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Cidade para o Entity Framework Core.
    /// </summary>
    public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
    {
        /// <summary>
        /// Define como a tabela Cidade será criada e seus campos configurados no banco de dados.
        /// </summary>
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            // Define a chave primária da tabela
            builder.HasKey(a => a.Id);

            // Nome é obrigatório e limitado a 100 caracteres
            builder.Property(p => p.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            // Estado é obrigatório e limitado a 2 caracteres (ex: 'SP', 'RJ')
            builder.Property(p => p.Estado)
                   .HasMaxLength(2)
                   .IsRequired();

            // Fim da configuração. Sem relacionamentos diretos configurados aqui.
        }
    }
}
