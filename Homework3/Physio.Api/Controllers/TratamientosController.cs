using Microsoft.AspNetCore.Mvc;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientoRepository _repo;

        public TratamientosController(ITratamientoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _repo.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(Tratamiento t)
        {
            var created = await _repo.AddAsync(t);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Tratamiento t)
        {
            if (id != t.Id) return BadRequest();
            await _repo.UpdateAsync(t);
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
