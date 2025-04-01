namespace TiendaAPI.Models;

public class Producto
{
    public int ID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }

    // Clave foránea hacia Tienda
    public int TiendaID { get; set; }
    public Tienda? Tienda { get; set; }
}
