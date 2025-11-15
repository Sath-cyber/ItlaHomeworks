using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Physio.Application.Contract;
using Physio.Application.Dtos;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Application.Service
{
    public class FisioterapeutaService : IFisioterapeutaService
    {
        private readonly IFisioterapeutaRepository _repo;

        public FisioterapeutaService(IFisioterapeutaRepository repo)
        {
            _repo = repo;
        }

        private static FisioterapeutaDto ToDto(Fisioterapeuta entity) => new FisioterapeutaDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Apellido = entity.Apellido,
            Especialidad = entity.Especialidad,
            Telefono = entity.Telefono
        };

        private static void UpdateEntityFromDto(Fisioterapeuta entity, FisioterapeutaDto dto)
        {
            entity.Nombre = dto.Nombre;
            entity.Apellido = dto.Apellido;
            entity.Especialidad = dto.Especialidad;
            entity.Telefono = dto.Telefono;
        }

        public async Task<IEnumerable<FisioterapeutaDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<FisioterapeutaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<FisioterapeutaDto> CreateAsync(FisioterapeutaDto dto)
        {
            var entity = new Fisioterapeuta();
            UpdateEntityFromDto(entity, dto);

            var created = await _repo.AddAsync(entity);
            return ToDto(created);
        }

        public async Task UpdateAsync(int id, FisioterapeutaDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"El fisioterapeuta con id {id} no existe.");

            UpdateEntityFromDto(entity, dto);
            await _repo.UpdateAsync(entity);
        }

        public Task SoftDeleteAsync(int id) => _repo.SoftDeleteAsync(id);
    }
}
