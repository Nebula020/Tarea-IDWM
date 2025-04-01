using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAPI.Data;
using TiendaAPI.Models;

namespace TiendaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StoreController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Tienda>> CrearTienda(Tienda tienda)
    {
        _context.Tiendas.Add(tienda);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerTienda), new { id = tienda.ID }, tienda);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tienda>>> ListarTiendas()
    {
        return await _context.Tiendas.Include(t => t.Productos).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tienda>> ObtenerTienda(int id)
    {
        var tienda = await _context.Tiendas.Include(t => t.Productos).FirstOrDefaultAsync(t => t.ID == id);

        if (tienda == null)
            return NotFound();

        return tienda;
    }
}
