using Physio.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Physio.Application.Contract
{
    public interface ISesionService
    {
        Task<IEnumerable<SesionDto>> GetAllAsync();
        Task<SesionDto?> GetByIdAsync(int id);
        Task<SesionDto> CreateAsync(SesionDto dto);
        Task UpdateAsync(int id, SesionDto dto);
        Task SoftDeleteAsync(int id);
    }
}
