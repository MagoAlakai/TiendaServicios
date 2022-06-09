namespace TiendaServicios.Api.Autor.Persistency;

public class ContextoAutor: DbContext
{
    public ContextoAutor( DbContextOptions<ContextoAutor> options) : base(options) { }
    public DbSet<AutorLibro> AutorLibro { get; set; } = default!;
    public DbSet<GradoAcademico> GradoAcademico { get; set; } = default!;
}
