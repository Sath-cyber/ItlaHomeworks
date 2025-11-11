using Microsoft.AspNetCore.Mvc;
using Physio.Domain.Entities;
using Physio.Infrastructure.Interfaces;

namespace Physio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FisioterapeutasController : ControllerBase
    {
        private readonly IFisioterapeutaRepository _repo;

        public FisioterapeutasController(IFisioterapeutaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fisioterapeuta>>> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Fisioterapeuta>> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Fisioterapeuta>> Create(Fisioterapeuta fisio)
        {
            var created = await _repo.AddAsync(fisio);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Fisioterapeuta fisio)
        {
            if (id != fisio.Id) return BadRequest("Id mismatch");
            await _repo.UpdateAsync(fisio);
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
