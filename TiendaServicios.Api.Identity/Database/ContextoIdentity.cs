namespace TiendaServicios.Api.Identity.Database;

public class ContextoIdentity : DbContext
{
    public ContextoIdentity(DbContextOptions<ContextoIdentity> options) : base(options) { }
    public DbSet<User> User { get; set; } = default!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
