namespace TiendaServicios.Api.Carrito.Models;
public class CarritoSesion
{
    public int CarritoSesionId { get; set; }
    public DateTime? CarritoSesionDate { get; set; }
    public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; } = default!;
}
