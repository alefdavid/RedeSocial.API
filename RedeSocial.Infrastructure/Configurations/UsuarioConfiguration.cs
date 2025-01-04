using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("tb_usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.DataNascimento)
                .IsRequired();

            builder.Property(u => u.FotoPerfil)
                .HasConversion<byte[]>();

            builder.Property(u => u.Biografia)
                .HasMaxLength(500);

            builder.Property(u => u.Visibilidade)
                .IsRequired();

            builder.Property(u => u.ReceberNotificacoes)
                .IsRequired();

            builder.Property(u => u.Permissao)
                .IsRequired();

            builder.Property(u => u.FlAtivo)
                .IsRequired();

            builder.Property(u => u.DataCriacao)
                .IsRequired();

            builder.Property(u => u.DataAlteracao)
                .IsRequired(false);

            builder.HasMany(u => u.Publicacoes)
                .WithOne()
                .HasForeignKey(p => p.UsuarioId);

            builder.HasMany(u => u.Amizades)
                .WithOne()
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
