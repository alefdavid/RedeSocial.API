using RedeSocial.Application.Interfaces.Repositories;
using RedeSocial.Domain.Entities;
using System.Linq.Expressions;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario> 
    {
        Task<Usuario> GetUsuarioAtivoById(int id);
        Task<List<Usuario>> GetUsuario(string? nome);
        Task<Usuario> GetUsuarioByEmail(string email); 
    }
}
