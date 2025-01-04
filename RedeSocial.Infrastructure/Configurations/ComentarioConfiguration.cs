using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("tb_comentario");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Texto)
                .IsRequired()
                .HasMaxLength(1000);

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
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.PublicacaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
