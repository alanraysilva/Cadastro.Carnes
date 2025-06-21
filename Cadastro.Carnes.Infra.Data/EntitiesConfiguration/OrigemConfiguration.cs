using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    public class OrigemConfiguration : IEntityTypeConfiguration<Origem>
    {
        public void Configure(EntityTypeBuilder<Origem> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
        }
    }
}
