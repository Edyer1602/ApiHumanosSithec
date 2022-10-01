using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class HumanoConfig : IEntityTypeConfiguration<Humano>
    {
        public void Configure(EntityTypeBuilder<Humano> builder)
        {
            builder.ToTable("Humanos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(p => p.Sexo)
                .HasMaxLength(1)
                .IsRequired()
                .HasColumnType("char");

            builder.Property(p => p.Edad)
                .IsRequired();

            builder.Property(p => p.Altura)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.Peso)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.CreatedBy)
                .HasDefaultValue(0);

            builder.Property(p => p.LastModifiedBy)
                .HasDefaultValue(0);

            builder.Property(p => p.LastModified)                
                .IsRequired(false);
        }
    }
}
