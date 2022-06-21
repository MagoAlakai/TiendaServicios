namespace TiendaServicios.Api.Carrito.Models;
public class CarritoSesionDetalle
{
    public int CarritoSesionDetalleId { get; set; }
    public DateTime? CarritoSesionDetalleDate { get; set; }
    public string? ProductoSeleccionado { get; set; }
    public int CarritoSesionId  { get; set; }
}
