using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Domain.Interfaces.Repositories
{
    public interface IAutorizacaoRepository 
    {
        Task<Usuario> Login(LoginDTO loginUsuarioDTO);
    }
}
