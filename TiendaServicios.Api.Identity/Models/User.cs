namespace TiendaServicios.Api.Identity.Models;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? UserGuid { get; set; }
}
