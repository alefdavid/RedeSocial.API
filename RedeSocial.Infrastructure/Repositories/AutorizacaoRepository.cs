using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public class AutorizacaoRepository : IAutorizacaoRepository
    {
        protected readonly RedeSocialDbContext _context;

        public AutorizacaoRepository(RedeSocialDbContext dbContext) 
        {
            _context = dbContext;
        }

        public async Task<Usuario> Login(LoginDTO loginUsuarioDTO)
        {
            return await _context.Usuarios.Where(x => x.Email == loginUsuarioDTO.Email && x.Senha == loginUsuarioDTO.Senha && x.FlAtivo).FirstOrDefaultAsync();
        }   
    }
}