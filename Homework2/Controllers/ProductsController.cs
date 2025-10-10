using Homework_2.Data;
using Homework_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    // GET: api/products?q=martillo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] string? q)
    {
        var query = _db.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Name.Contains(q) || (p.Category != null && p.Category.Contains(q)));
        return Ok(await query.OrderByDescending(p => p.Id).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _db.Products.FindAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product dto)
    {
        _db.Products.Add(dto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Product dto)
    {
        if (id != dto.Id) return BadRequest("Id del body no coincide con la ruta.");
        var exists = await _db.Products.AnyAsync(p => p.Id == id);
        if (!exists) return NotFound();

        _db.Entry(dto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:int}/stock")]
    public async Task<IActionResult> AdjustStock(int id, [FromQuery] int amount)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return NotFound();
        p.Stock += amount;
        await _db.SaveChangesAsync();
        return Ok(p);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return NotFound();
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
