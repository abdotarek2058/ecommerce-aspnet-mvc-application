using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace IMDB.Data.Base
{
    public class EntityBaseRepositry<T> : IEntityBaseRepositry<T> where T : class, IEntityBase , new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepositry(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            //query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();

        }
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            //query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity =await  GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync (int id, T entity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;

        }
    }
}
