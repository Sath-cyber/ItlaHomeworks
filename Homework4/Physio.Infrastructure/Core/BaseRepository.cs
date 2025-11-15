using Microsoft.EntityFrameworkCore;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Physio.Infrastructure.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PhysioContext _db;
        protected readonly DbSet<T> _table;

        public BaseRepository(PhysioContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var prop = typeof(T).GetProperty("IsDeleted");

            if (prop != null && prop.PropertyType == typeof(bool))
            {
                return await _table
                    .Where(e => !EF.Property<bool>(e, "IsDeleted"))
                    .ToListAsync();
            }
            return await _table.ToListAsync();
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await _db.SaveChangesAsync();
        }

        public virtual async Task SoftDeleteAsync(int id)
        {
            var entity = await _table.FindAsync(id);

            if (entity == null)
                return;

            var prop = typeof(T).GetProperty("IsDeleted");

            if (prop != null && prop.PropertyType == typeof(bool))
            {
                prop.SetValue(entity, true);
                await _db.SaveChangesAsync();
            }
            else
            {
                _table.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
