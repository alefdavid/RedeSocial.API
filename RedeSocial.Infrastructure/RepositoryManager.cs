using RedeSocial.Domain;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RedeSocialDbContext dbContext;

        private bool disposed;

        public IUsuarioRepository UsuarioRepository { get; }

        public IPublicacaoRepository PublicacaoRepository { get; }

        public RepositoryManager(RedeSocialDbContext redeSocialDbContext,
                                IUsuarioRepository usuarioRepository
                                
                                )
        {
            this.dbContext = redeSocialDbContext;
            UsuarioRepository = usuarioRepository;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                    UsuarioRepository.Dispose();
                   
                }
                disposed = true;
            }
        }

        ~RepositoryManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
