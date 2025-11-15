using Physio.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Physio.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteDto>> GetAllAsync();
        Task<PacienteDto?> GetByIdAsync(int id);
        Task<PacienteDto> CreateAsync(PacienteDto dto);
        Task UpdateAsync(int id, PacienteDto dto);
        Task SoftDeleteAsync(int id);
    }
}
