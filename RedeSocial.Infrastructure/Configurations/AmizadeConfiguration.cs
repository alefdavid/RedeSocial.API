using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class AmizadeConfiguration : IEntityTypeConfiguration<Amizade>
    {
        public void Configure(EntityTypeBuilder<Amizade> builder)
        {
            builder.ToTable("tb_amizade");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.UsuarioId)
                .IsRequired();

            builder.Property(a => a.AmigoId)
                .IsRequired();

            builder.Property(a => a.FlAtivo)
                .IsRequired();

            builder.Property(a => a.DataCriacao)
                .IsRequired();

            builder.Property(a => a.DataAlteracao)
                .IsRequired(false);

            builder.HasOne<Usuario>()
                .WithMany(u => u.Amizades)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(a => a.AmigoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
