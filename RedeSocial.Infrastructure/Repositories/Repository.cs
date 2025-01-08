using Microsoft.EntityFrameworkCore;
using RedeSocial.Application.Interfaces.Repositories;
using RedeSocial.Domain.Entities;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly RedeSocialDbContext _context;

        private bool disposed;

        public Repository(RedeSocialDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Put(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return false;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }

                disposed = true;
            }
        }

        ~Repository()
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
