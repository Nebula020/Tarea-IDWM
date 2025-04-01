namespace TiendaAPI.Models;

public class Tienda
{
    public int ID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;

    // Relación: Una tienda tiene varios productos
    public List<Producto> Productos { get; set; } = new List<Producto>();
}
