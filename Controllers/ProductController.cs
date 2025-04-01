using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAPI.Data;
using TiendaAPI.Models;

namespace TiendaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> CrearProducto(Producto producto)
    {
        var tiendaExiste = await _context.Tiendas.AnyAsync(t => t.ID == producto.TiendaID);
        if (!tiendaExiste)
            return BadRequest("La tienda especificada no existe.");

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.ID }, producto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> ObtenerProducto(int id)
    {
        var producto = await _context.Productos.Include(p => p.Tienda).FirstOrDefaultAsync(p => p.ID == id);

        if (producto == null)
            return NotFound();

        return producto;
    }
}
