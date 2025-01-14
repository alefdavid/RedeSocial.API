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

        Task<ComentarioDTO> GetComentarioById(int id);
        Task<ComentarioDTO> PostComentario(ComentarioDTO comentarioDTO);
        Task<ComentarioDTO> PutComentario(ComentarioDTO comentarioDTO, int id);
        Task<bool> DeleteComentario(int id);

        Task<CurtidaDTO> GetCurtidaById(int id);
        Task<CurtidasPublicacaoDTO> GetCurtidasByPublicacao(int publicacaoId);
        Task<CurtidaDTO> PostCurtida(CurtidaDTO curtidaDTO);
        Task<bool> DeleteCurtida(int id);
    }
}
