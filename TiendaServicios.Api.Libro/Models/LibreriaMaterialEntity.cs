namespace TiendaServicios.Api.Libro.Models;

public class LibreriaMaterialEntity
{
    public int Id { get; set; }
    public Guid? LibreriaMaterialGuid { get; set; }
    public string? Titulo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
}
