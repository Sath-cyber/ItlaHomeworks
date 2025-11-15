using Microsoft.AspNetCore.Mvc;
using Physio.Application.Dtos;
using Physio.Application.Contract;

namespace Physio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientoService _service;

        public TratamientosController(ITratamientoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientoDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TratamientoDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item is null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TratamientoDto>> Create(TratamientoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, TratamientoDto dto)
        {
            if (id != dto.Id && dto.Id != 0)
                return BadRequest("El id de la URL y del cuerpo no coinciden.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            dto.Id = id;
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing is null) return NotFound();

            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
