namespace TiendaServicios.Api.Carrito.Database;
public class ContextoCarrito : DbContext
{
    public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options) { }
    public DbSet<CarritoSesion> CarritoSesion { get; set; } = default!;
    public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; } = default!;
}
