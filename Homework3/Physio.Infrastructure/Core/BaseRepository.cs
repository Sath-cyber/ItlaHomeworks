using Microsoft.EntityFrameworkCore;
using Physio.Domain.Core;
using Physio.Infrastructure.Context;

namespace Physio.Infrastructure.Core;

public abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly PhysioContext _db;
    protected readonly DbSet<T> _set;

    protected BaseRepository(PhysioContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _set.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public Task<T?> GetByIdAsync(int id)
        => _set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public Task<List<T>> GetAllAsync()
        => _set.AsNoTracking().ToListAsync();

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _set.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var entity = await _set.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) return;
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }
}
