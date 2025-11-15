using System.Collections.Generic;
using System.Threading.Tasks;
using Physio.Domain.Entities;

namespace Physio.Infrastructure.Interfaces
{
    public interface IFisioterapeutaRepository
    {
        Task<Fisioterapeuta> AddAsync(Fisioterapeuta fisio);
        Task<Fisioterapeuta> GetByIdAsync(int id);
        Task<List<Fisioterapeuta>> GetAllAsync();
        Task UpdateAsync(Fisioterapeuta fisio);
        Task SoftDeleteAsync(int id);
    }
}
