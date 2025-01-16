using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Enum;

namespace RedeSocial.Domain.Interfaces.Services
{
    public interface IAutorizacaoService : IService
    {
        Task<LoginUsuarioDTO> Login(LoginDTO login);
        string GenerateJwtToken(string email, PermissaoEnum permissao);
        string EncryptSenha(string password);
    }
}