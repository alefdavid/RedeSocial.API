using RedeSocial.Domain.Interfaces.Repositories;

namespace RedeSocial.Domain
{
    public interface IRepositoryManager : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        IPublicacaoRepository PublicacaoRepository { get; }

        Task Save();
    }
}
