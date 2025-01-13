using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Repositories;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public class PublicacaoRepository : Repository<Publicacao>, IPublicacaoRepository
    {
        public PublicacaoRepository(RedeSocialDbContext context) : base(context) { }

        public async Task<Comentario> GetComentarioById(int id)
        {
            return await _context.Publicacoes.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public Task<Curtida> GetCurtidaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Curtida> GetCurtidaByPublicacao(int publicacaoId)
        {
            throw new NotImplementedException();
        }

        public Task<Publicacao> GetPublicacaoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Publicacao> GetPublicacoesByUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
