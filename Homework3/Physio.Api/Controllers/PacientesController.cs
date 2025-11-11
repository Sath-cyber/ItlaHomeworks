using Microsoft.AspNetCore.Mvc;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repo;

        public PacientesController(IPacienteRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<ActionResult<List<Paciente>>> GetAll()
            => await _repo.GetAllAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Paciente>> GetById(int id)
        {
            var pac = await _repo.GetByIdAsync(id);
            return pac is null ? NotFound() : pac;
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> Create(Paciente paciente)
        {
            var created = await _repo.AddAsync(paciente);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Paciente paciente)
        {
            if (id != paciente.Id) return BadRequest("Id mismatch");
            await _repo.UpdateAsync(paciente);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _repo.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
