using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedeSocial.Domain;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Enum;
using RedeSocial.Domain.Interfaces.Services;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RedeSocial.Application.Services
{
    public class AutorizacaoService : IAutorizacaoService
    {
        public readonly IRepositoryManager _repositoryManager;

        public readonly IConfiguration _configuration;

        public readonly IMapper _mapper;

        private bool disposed;

        public AutorizacaoService(IRepositoryManager repositoryManager, IConfiguration configuration,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        public async Task<LoginUsuarioDTO> Login(LoginDTO login)
        {
            login.Senha = EncryptSenha(login.Senha);

            var retorno = await _repositoryManager.AutorizacaoRepository.Login(login) ?? throw new Exception("Usuário ou senha inválido!");
            var token = GenerateJwtToken(retorno.Email, retorno.Permissao);

            return new LoginUsuarioDTO(retorno.Email, token);
        }

        public string GenerateJwtToken(string email, PermissaoEnum permissao)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("userName", email),
                new Claim(ClaimTypes.Role, permissao.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials,
                claims: claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public string EncryptSenha(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _repositoryManager.Dispose();
                }

                disposed = true;
            }
        }

        ~AutorizacaoService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

