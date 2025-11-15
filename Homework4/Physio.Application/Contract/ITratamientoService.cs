using Physio.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Physio.Application.Contract
{
    public interface ITratamientoService
    {
        Task<IEnumerable<TratamientoDto>> GetAllAsync();
        Task<TratamientoDto?> GetByIdAsync(int id);
        Task<TratamientoDto> CreateAsync(TratamientoDto dto);
        Task UpdateAsync(int id, TratamientoDto dto);
        Task SoftDeleteAsync(int id);
    }
}
