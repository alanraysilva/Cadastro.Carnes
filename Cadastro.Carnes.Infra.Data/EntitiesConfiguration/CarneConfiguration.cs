using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    /// <summary>
    /// Configuração da entidade Carne para o Entity Framework (mapeamento da tabela).
    /// </summary>
    public class CarneConfiguration : IEntityTypeConfiguration<Carne>
    {
        /// <summary>
        /// Define as regras de mapeamento para a tabela Carne no banco de dados.
        /// </summary>
        public void Configure(EntityTypeBuilder<Carne> builder)
        {
            // Define a chave primária da tabela
            builder.HasKey(a => a.Id);

            // Configura o campo Nome como obrigatório e com no máximo 100 caracteres
            builder.Property(p => p.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            // Configura o campo OrigemId como obrigatório
            builder.Property(p => p.OrigemId)
                   .IsRequired();

            // Define o relacionamento: cada Carne tem uma Origem
            builder.HasOne(e => e.Origem)           // Propriedade de navegação em Carne
                   .WithMany()                      // Não há coleção do outro lado (Origem não precisa conhecer todas as carnes)
                   .HasForeignKey(e => e.OrigemId); // Chave estrangeira

            // Aqui fica fácil saber exatamente como a tabela será criada e como os dados se relacionam!
        }
    }
}
