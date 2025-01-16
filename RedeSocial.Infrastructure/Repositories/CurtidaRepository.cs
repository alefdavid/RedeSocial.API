using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public class CurtidaRepository : Repository<Curtida>, ICurtidaRepository
    {
        public CurtidaRepository(RedeSocialDbContext context) : base(context) { }
        
        public async Task<Curtida> GetCurtidaById(int id)
        {
            return await _context.Curtidas.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public async Task<Curtida> GetCurtidasByPublicacao(int publicacaoId)
        {
            return await _context.Curtidas.FirstOrDefaultAsync(c => c.PublicacaoId == publicacaoId && c.FlAtivo);
        }
    }
}
