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

        public IComentarioRepository ComentarioRepository { get; }

        public ICurtidaRepository CurtidaRepository { get; }

        public IAutorizacaoRepository AutorizacaoRepository { get; }

        public RepositoryManager(RedeSocialDbContext redeSocialDbContext,
                                IUsuarioRepository usuarioRepository,
                                IPublicacaoRepository publicacaoRepository,
                                IComentarioRepository comentarioRepository,
                                ICurtidaRepository curtidaRepository,
                                IAutorizacaoRepository autorizacaoRepository)                              
        {
            this.dbContext = redeSocialDbContext;
            UsuarioRepository = usuarioRepository;
            PublicacaoRepository = publicacaoRepository;
            ComentarioRepository = comentarioRepository;
            CurtidaRepository = curtidaRepository;
            AutorizacaoRepository = autorizacaoRepository;
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
                    PublicacaoRepository.Dispose();
                    ComentarioRepository.Dispose();
                    CurtidaRepository.Dispose();
                    if (AutorizacaoRepository is IDisposable disposableAutorizacaoRepository)
                    {
                        disposableAutorizacaoRepository.Dispose();
                    }
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
