namespace TiendaServicios.Api.Libro.Database;
public class ContextoLibreria : DbContext
{
    public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }

    public DbSet<LibreriaMaterialEntity> LibreriaMaterialEntity { get; set; } = default!;
}
