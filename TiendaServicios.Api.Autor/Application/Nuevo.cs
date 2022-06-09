namespace TiendaServicios.Api.Autor.Application;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        public readonly ContextoAutor _context;

        public Manejador (ContextoAutor context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            AutorLibro autor_libro = new()
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Guid.NewGuid(),
            };

            _context.AutorLibro.Add(autor_libro);
            int count = await _context.SaveChangesAsync();

            if (count is 0)
            {
                throw new Exception("No se pudo insertar el Autor");
            }
            return Unit.Value;
        }
    }
}
