using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Physio.Application.Contract;
using Physio.Application.Dtos;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Application.Service
{
    public class SesionService : ISesionService
    {
        private readonly ISesionRepository _repo;

        public SesionService(ISesionRepository repo)
        {
            _repo = repo;
        }

        private static SesionDto ToDto(Sesion entity) => new SesionDto
        {
            Id = entity.Id,
            FechaHora = entity.FechaHora,
            Notas = entity.Notas,
            PacienteId = entity.PacienteId,
            FisioterapeutaId = entity.FisioterapeutaId,
            TratamientoId = entity.TratamientoId
        };

        private static void UpdateEntityFromDto(Sesion entity, SesionDto dto)
        {
            entity.FechaHora = dto.FechaHora;
            entity.Notas = dto.Notas;
            entity.PacienteId = dto.PacienteId;
            entity.FisioterapeutaId = dto.FisioterapeutaId;
            entity.TratamientoId = dto.TratamientoId;
        }

        public async Task<IEnumerable<SesionDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<SesionDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<SesionDto> CreateAsync(SesionDto dto)
        {
            var entity = new Sesion();
            UpdateEntityFromDto(entity, dto);

            var created = await _repo.AddAsync(entity);
            return ToDto(created);
        }

        public async Task UpdateAsync(int id, SesionDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                throw new KeyNotFoundException($"La sesion con id {id} no existe.");

            UpdateEntityFromDto(entity, dto);
            await _repo.UpdateAsync(entity);
        }

        public Task SoftDeleteAsync(int id) => _repo.SoftDeleteAsync(id);
    }
}
