using Cadastro.Carnes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Carnes.Infra.Data.EntitiesConfiguration
{
    public class CompradorConfiguration : IEntityTypeConfiguration<Comprador>
    {
        public void Configure(EntityTypeBuilder<Comprador> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Documento).HasMaxLength(14).IsRequired();
            builder.Property(p => p.CidadeId).IsRequired();


            builder.HasOne(e => e.Cidade).WithMany().HasForeignKey(e => e.CidadeId);
        }
    }
}
