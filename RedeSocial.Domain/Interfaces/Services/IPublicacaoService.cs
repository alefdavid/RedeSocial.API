using RedeSocial.Domain.DTOs;

namespace RedeSocial.Domain.Interfaces.Services
{
    public interface IPublicacaoService : IService
    {
        Task<PublicacaoDTO> GetById(int id);
        Task<PublicacoesUsuarioDTO> GetPublicacoesByUsuario(int usuarioId);
        Task<PublicacaoDTO> Post(PublicacaoDTO publicacaoDTO);
        Task<bool> Put(PublicacaoDTO publicacaoDTO, int id);        
        Task<bool> Delete(int id);
    }
}
