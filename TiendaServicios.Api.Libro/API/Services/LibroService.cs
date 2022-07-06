using TiendaServicios.Api.Libro.Logic.Commands;
using TiendaServicios.Api.Libro.Logic.Commands.Libro;
using TiendaServicios.Api.Libro.Logic.Queries;
using TiendaServicios.Api.Libro.Logic.Queries.Libro;

namespace TiendaServicios.Api.Libro.API.Services;

public class LibroService : LibrosServices.LibrosServicesBase
{
    private readonly ContextoLibreria _libreriaContext;
    private readonly ILogger<LibroService> _logger;

    public LibroService(ContextoLibreria? libreriaContext, ILogger<LibroService>? logger)
    {
        _libreriaContext = libreriaContext ?? throw new ArgumentNullException(nameof(libreriaContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<GetLibroResponse> GetLibro(GetLibroRequest request, ServerCallContext context)
    {
        GetLibroQuery query = new(request, _libreriaContext);
        QueryValueObject<GetLibroResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<LibroModelListResponse> GetAllLibros(GetAllLibrosRequest request, ServerCallContext context)
    {
        GetAllLibrosQuery query = new(request, _libreriaContext);
        QueryValueObject<LibroModelListResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<AddLibroResponse> AddLibro(AddLibroRequest request, ServerCallContext context)
    {
        AddLibroCommand command = new(request, _libreriaContext);
        CommandValueObject<AddLibroResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<UpdateLibroResponse> UpdateLibro(UpdateLibroRequest request, ServerCallContext context)
    {
        UpdateLibroCommand command = new(request, _libreriaContext);
        CommandValueObject<UpdateLibroResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<DeleteLibroResponse> DeleteLibro(DeleteLibroRequest request, ServerCallContext context)
    {
        DeleteLibroCommand command = new(request, _libreriaContext);
        CommandValueObject<DeleteLibroResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
}
