using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface ICurtidaRepository : IRepository<Curtida>
    {
        Task<Curtida> GetCurtidaById(int id);
        Task<Curtida> GetCurtidasByPublicacao(int publicacaoId);
    }
}
