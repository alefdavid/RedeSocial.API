using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(RedeSocialDbContext context) : base(context) { }
        

        public async Task<Comentario> GetComentarioById(int id)
        {
            return await _context.Comentarios.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }
    }
}
