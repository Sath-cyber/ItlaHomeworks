using Physio.Domain.Entities;

namespace Physio.Infrastructure.Interfaces
{
    public interface ITratamientoRepository
    {
        Task<IEnumerable<Tratamiento>> GetAllAsync();
        Task<Tratamiento?> GetByIdAsync(int id);
        Task<Tratamiento> AddAsync(Tratamiento tratamiento);
        Task UpdateAsync(Tratamiento tratamiento);
        Task SoftDeleteAsync(int id);
    }
}
