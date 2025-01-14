using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public class AmizadeRepository : Repository<Amizade>, IAmizadeRepository
    {
        public AmizadeRepository(RedeSocialDbContext dbContext) : base(dbContext) { }

        public void Cadastrar(Amizade amizade)
        {
            var amizadeExistente = _context.Amizades.Where(x => x.UsuarioId == amizade.UsuarioId || x.AmigoId == amizade.AmigoId).FirstOrDefault();

            if (amizadeExistente == null)
            {
                _context.Amizades.Add(amizade);
                amizade.DataCriacao = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public Task<List<Amizade>> GetAmigos(int usuarioId)
        {
            return _context.Amizades.Where(x => x.UsuarioId == usuarioId && x.FlAtivo).ToListAsync();
        }
    }
}
