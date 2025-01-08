using RedeSocial.Domain.DTOs;

namespace RedeSocial.Domain.Interfaces.Services
{
    public interface IUsuarioService : IService
    {
        Task<UsuarioDTO> Post(UsuarioDTO usuarioDTO);
        Task<bool> Put(UsuarioDTO usuarioDTO, int id);
        Task<ListarUsuarioDTO> GetById(int id);
        Task<List<ListarUsuarioDTO>> GetAll(string? nome);
        Task<bool> Delete(int id);
    }
}