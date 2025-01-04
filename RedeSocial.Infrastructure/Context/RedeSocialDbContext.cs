using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using System.Reflection;

namespace RedeSocial.Infrastructure.Context
{
    public class RedeSocialDbContext : DbContext
    {
        public RedeSocialDbContext() { }

        public RedeSocialDbContext(DbContextOptions<RedeSocialDbContext> options) : base(options) { }

        public DbSet<Amizade> Amizades { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
        public DbSet<Notificacao> Notificacaos { get; set; }
        public DbSet<Publicacao> Publicacaos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
        }   
    }
}
