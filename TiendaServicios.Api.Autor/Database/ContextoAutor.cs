namespace TiendaServicios.Api.Autor.Database;

public class ContextoAutor : DbContext
{
    public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { }
    public DbSet<AutorLibro> AutorLibro { get; set; } = default!;
    public DbSet<GradoAcademico> GradoAcademico { get; set; } = default!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
