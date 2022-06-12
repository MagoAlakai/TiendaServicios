namespace TiendaServicios.Api.Autor.Models;

public class AutorLibro
{
    public int AutorLibroId { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? GradoAcademicoGuid { get; set; }
    public string? AutorLibroGuid { get; set; }
}
