using Microsoft.EntityFrameworkCore;
using Physio.Domain.Entities;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Interfaces;

namespace Physio.Infrastructure.Repositories
{
    public class TratamientoRepository : ITratamientoRepository
    {
        private readonly PhysioContext _context;

        public TratamientoRepository(PhysioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tratamiento>> GetAllAsync()
            => await _context.Tratamientos.Where(t => !t.IsDeleted).ToListAsync();

        public async Task<Tratamiento?> GetByIdAsync(int id)
            => await _context.Tratamientos.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

        public async Task<Tratamiento> AddAsync(Tratamiento tratamiento)
        {
            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();
            return tratamiento;
        }

        public async Task UpdateAsync(Tratamiento tratamiento)
        {
            tratamiento.UpdatedAt = DateTime.UtcNow;
            _context.Tratamientos.Update(tratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
