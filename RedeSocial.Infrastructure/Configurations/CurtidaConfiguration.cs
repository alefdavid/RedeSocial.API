using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class CurtidaConfiguration : IEntityTypeConfiguration<Curtida>
    {
        public void Configure(EntityTypeBuilder<Curtida> builder)
        {
            builder.ToTable("tb_curtida");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.UsuarioId)
                .IsRequired();

            builder.Property(c => c.PublicacaoId)
                .IsRequired();

            builder.Property(c => c.FlAtivo)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .IsRequired(false);

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Publicacao>()
                .WithMany(p => p.Curtidas)
                .HasForeignKey(c => c.PublicacaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

