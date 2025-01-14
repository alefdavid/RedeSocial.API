using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        Task<Publicacao> GetPublicacoesByUsuario(int usuarioId);
        Task<Publicacao> GetPublicacaoById(int id);       
    }        
}
