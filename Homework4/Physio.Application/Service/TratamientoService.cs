using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Physio.Application.Contract;
using Physio.Application.Dtos;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Application.Service
{
    public class TratamientoService : ITratamientoService
    {
        private readonly ITratamientoRepository _repo;

        public TratamientoService(ITratamientoRepository repo)
        {
            _repo = repo;
        }

        private static TratamientoDto ToDto(Tratamiento entity) => new TratamientoDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Descripcion = entity.Descripcion,
            Costo = entity.Costo,
            DuracionMinutos = entity.DuracionMinutos
        };

        private static void UpdateEntityFromDto(Tratamiento entity, TratamientoDto dto)
        {
            entity.Nombre = dto.Nombre;
            entity.Descripcion = dto.Descripcion;
            entity.Costo = dto.Costo;
            entity.DuracionMinutos = dto.DuracionMinutos;
        }

        public async Task<IEnumerable<TratamientoDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<TratamientoDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<TratamientoDto> CreateAsync(TratamientoDto dto)
        {
            var entity = new Tratamiento();
            UpdateEntityFromDto(entity, dto);

            var created = await _repo.AddAsync(entity);
            return ToDto(created);
        }

        public async Task UpdateAsync(int id, TratamientoDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"El tratamiento con id {id} no existe.");

            UpdateEntityFromDto(entity, dto);
            await _repo.UpdateAsync(entity);
        }

        public Task SoftDeleteAsync(int id) => _repo.SoftDeleteAsync(id);
    }
}
