using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    public class CarneConfiguration : IEntityTypeConfiguration<Carne>
    {
        public void Configure(EntityTypeBuilder<Carne> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.OrigemId).IsRequired();

            builder.HasOne(e => e.Origem).WithMany().HasForeignKey(e => e.OrigemId);

        }
    }
}
