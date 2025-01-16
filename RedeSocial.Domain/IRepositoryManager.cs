using RedeSocial.Domain.Interfaces.Repositories;

namespace RedeSocial.Domain
{
    public interface IRepositoryManager : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        IPublicacaoRepository PublicacaoRepository { get; }
        IComentarioRepository ComentarioRepository { get; }
        ICurtidaRepository CurtidaRepository { get; }
        IAutorizacaoRepository AutorizacaoRepository { get; }

        Task Save();
    }
}
