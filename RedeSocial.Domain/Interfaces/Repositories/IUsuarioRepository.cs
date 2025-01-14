using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario> 
    {
        Task<Usuario> GetUsuarioAtivoById(int id);
        Task<List<Usuario>> GetUsuario(string? nome);
        Task<Usuario> GetUsuarioByEmail(string email); 
    }
}
