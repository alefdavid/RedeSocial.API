using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IAmizadeRepository : IRepository<Amizade> 
    {
        Task<List<Amizade>> GetAmigos(int usuarioId);
        void Cadastrar(Amizade amizade);
    }
}
