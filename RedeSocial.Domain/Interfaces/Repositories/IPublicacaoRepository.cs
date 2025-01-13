using RedeSocial.Application.Interfaces.Repositories;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IPublicacaoRepository : IRepository<Publicacao>
    {
        Task<Publicacao> GetPublicacoesByUsuario(int usuarioId);
        Task<Publicacao> GetPublicacaoById(int id);

        Task<Comentario> GetComentarioById(int id);
        
        Task<Curtida> GetCurtidaById(int id);
        Task<Curtida> GetCurtidaByPublicacao(int publicacaoId);
    }        
}
