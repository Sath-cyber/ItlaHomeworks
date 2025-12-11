using Microsoft.EntityFrameworkCore;
using Physio.Infrastructure.Context;
using Physio.Infrastructure.Interfaces;
using Physio.Domain.Entities;

namespace Physio.Infrastructure.Repositories
{
    public class SesionRepository : ISesionRepository
    {
        private readonly PhysioContext _context;

        public SesionRepository(PhysioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sesion>> GetAllAsync()
        {
            return await _context.Sesiones
                .Include(s => s.Paciente)
                .Include(s => s.Fisioterapeuta)
                .Include(s => s.Tratamiento)
                .Where(s => !s.IsDeleted)
                .ToListAsync();
        }

        public async Task<Sesion?> GetByIdAsync(int id)
        {
            return await _context.Sesiones
                .Include(s => s.Paciente)
                .Include(s => s.Fisioterapeuta)
                .Include(s => s.Tratamiento)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<Sesion> AddAsync(Sesion entity)
        {
            _context.Sesiones.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Sesion entity)
        {
            _context.Sesiones.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _context.Sesiones.FindAsync(id);
            if (entity == null) return false;

            entity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
