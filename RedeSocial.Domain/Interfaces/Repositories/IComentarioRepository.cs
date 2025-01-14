using RedeSocial.Application.Interfaces.Repositories;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IComentarioRepository : IRepository<Comentario>
    {
        Task<Comentario> GetComentarioById(int id);
    }
}
