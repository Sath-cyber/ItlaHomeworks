using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Physio.Application.Dtos;
using Physio.Application.Interfaces;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repo;

        public PacienteService(IPacienteRepository repo)
        {
            _repo = repo;
        }

        private static PacienteDto ToDto(Paciente entity) => new PacienteDto
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Apellido = entity.Apellido,
            Telefono = entity.Telefono,
            Edad = entity.Edad
        };

        private static void UpdateEntityFromDto(Paciente entity, PacienteDto dto)
        {
            entity.Nombre = dto.Nombre;
            entity.Apellido = dto.Apellido;
            entity.Telefono = dto.Telefono;
            entity.Edad = dto.Edad;
        }

        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto);
        }

        public async Task<PacienteDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<PacienteDto> CreateAsync(PacienteDto dto)
        {
            var entity = new Paciente();
            UpdateEntityFromDto(entity, dto);

            var created = await _repo.AddAsync(entity);
            return ToDto(created);
        }

        public async Task UpdateAsync(int id, PacienteDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
            {
                throw new KeyNotFoundException($"El paciente con id {id} no existe.");
            }

            UpdateEntityFromDto(entity, dto);
            await _repo.UpdateAsync(entity);
        }

        public Task SoftDeleteAsync(int id)
        {
            return _repo.SoftDeleteAsync(id);
        }
    }
}
