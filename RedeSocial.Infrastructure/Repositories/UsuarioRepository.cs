using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;
using System.Linq.Expressions;

namespace RedeSocial.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RedeSocialDbContext dbContext) : base(dbContext) { }

        public async Task<Usuario> GetUsuarioAtivoById(int id)
        {
            return await _context.Usuarios.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public async Task<List<Usuario>> GetUsuario(string? nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return await _context.Usuarios.Where(x => x.FlAtivo).ToListAsync();
            }

            return await _context.Usuarios.Where(x => x.Nome == nome && x.FlAtivo).ToListAsync();
        }
        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.FlAtivo);
        }
    }
}