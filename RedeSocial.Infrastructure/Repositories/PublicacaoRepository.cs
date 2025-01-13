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
            return await _context.Comentarios.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public async Task<Curtida> GetCurtidaById(int id)
        {
            return await _context.Curtidas.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public async Task<Curtida> GetCurtidasByPublicacao(int publicacaoId)
        {
            return await _context.Curtidas.FirstOrDefaultAsync(c => c.PublicacaoId == publicacaoId && c.FlAtivo);
        }

        public async Task<Publicacao> GetPublicacaoById(int id)
        {
            return await _context.Publicacoes.Where(x => x.Id == id && x.FlAtivo).FirstOrDefaultAsync();
        }

        public async Task<Publicacao> GetPublicacoesByUsuario(int usuarioId)
        {
            return await _context.Publicacoes.FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.FlAtivo);
        }
    }
}
