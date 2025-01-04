using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class PublicacaoConfiguration : IEntityTypeConfiguration<Publicacao>
    {
        public void Configure(EntityTypeBuilder<Publicacao> builder)
        {
            builder.ToTable("tb_publicacao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Texto)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.Visibilidade)
                .IsRequired();

            builder.Property(p => p.UsuarioId)
                .IsRequired();

            builder.Property(p => p.FlAtivo)
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .IsRequired();

            builder.Property(p => p.DataAlteracao)
                .IsRequired(false);

            builder.HasOne<Usuario>() 
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comentarios)
                .WithOne(c => c.Publicacao) 
                .HasForeignKey(c => c.PublicacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Curtidas)
                .WithOne(c => c.Publicacao) 
                .HasForeignKey(c => c.PublicacaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


