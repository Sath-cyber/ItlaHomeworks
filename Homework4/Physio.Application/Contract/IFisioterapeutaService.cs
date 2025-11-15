using Physio.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Physio.Application.Contract
{
    public interface IFisioterapeutaService
    {
        Task<IEnumerable<FisioterapeutaDto>> GetAllAsync();
        Task<FisioterapeutaDto?> GetByIdAsync(int id);
        Task<FisioterapeutaDto> CreateAsync(FisioterapeutaDto dto);
        Task UpdateAsync(int id, FisioterapeutaDto dto);
        Task SoftDeleteAsync(int id);
    }
}
