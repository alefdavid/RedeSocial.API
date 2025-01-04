using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Configurations
{
    public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("tb_notificacao");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.UsuarioId)
                .IsRequired();

            builder.Property(n => n.Mensagem)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.Lido)
                .IsRequired();

            builder.Property(n => n.DataCriacao)
                .IsRequired();

            builder.Property(n => n.DataAlteracao)
                .IsRequired(false);

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

