namespace TiendaServicios.Api.Libro.Logic;

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
        LibreriaMaterialEntity? libro = await _libreriaContext.LibreriaMaterialEntity.FindAsync(request.LibroId);
        if (libro is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Libro with ID={request.LibroId} is not found"));
        }

        LibroModel libro_model = libro.MapToLibroModel();

        GetLibroResponse get_libro_response = libro_model is null
            ? new GetLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new GetLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return get_libro_response;
    }

    public override async Task<LibroModelListResponse> GetAllLibros(GetAllLibrosRequest request, ServerCallContext context)
    {
        List<LibreriaMaterialEntity>? libreria_list = await _libreriaContext.LibreriaMaterialEntity.ToListAsync();
        IEnumerable<LibroModel>? libro_model_list = libreria_list.Select(x => x.MapToLibroModel());

        LibroModelListResponse get_all_libros_response = libro_model_list is null
            ? new LibroModelListResponse
            {
                Success = false,
                LibroModelList = { libro_model_list },
            }
            : new LibroModelListResponse
            {
                Success = true,
                LibroModelList = { libro_model_list },
            };

        return get_all_libros_response;
    }

    public override async Task<AddLibroResponse> AddLibro(AddLibroRequest request, ServerCallContext context)
    {
        LibreriaMaterialEntity libro = request.LibroModel.MapToAddLibroModelFromRequest();

        _libreriaContext.Add(libro);
        await _libreriaContext.SaveChangesAsync();

        LibroModel libro_model = libro.MapToLibroModel();

        AddLibroResponse add_libro_response = libro_model is null
            ? new AddLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new AddLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return add_libro_response;
    }

    public override async Task<UpdateLibroResponse> UpdateLibro(UpdateLibroRequest request, ServerCallContext context)
    {
        GetLibroRequest add_libro_request = new()
        {
            LibroId = request.LibroId
        };
        GetLibroResponse get_libro_response = await GetLibro(add_libro_request, context);

        bool libro_exist = await _libreriaContext.LibreriaMaterialEntity.AnyAsync(p => p.Id.Equals(request.LibroId));
        if (libro_exist is false)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Libro with ID={request.LibroId} is not found"));
        }

        LibreriaMaterialEntity libro = request.LibroModel.MapToUpdateLibroModelFromRequest();

        _libreriaContext.ChangeTracker.Clear();
        _libreriaContext.Update(libro);
        await _libreriaContext.SaveChangesAsync();

        LibroModel libro_model = libro.MapToLibroModel();

        UpdateLibroResponse update_libro_response = libro_model is null
            ? new UpdateLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new UpdateLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return update_libro_response;
    }

    public override async Task<DeleteLibroResponse> DeleteLibro(DeleteLibroRequest request, ServerCallContext context)
    {
        LibreriaMaterialEntity? libro = await _libreriaContext.LibreriaMaterialEntity.FindAsync(request.LibroId);

        if (libro is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Libro with ID={request.LibroId} is not found"));
        }

        _libreriaContext.LibreriaMaterialEntity.Remove(libro);
        int deleteCount = await _libreriaContext.SaveChangesAsync();

        DeleteLibroResponse update_libro_response = new DeleteLibroResponse
        {
            Success = deleteCount > 0
        };

        return update_libro_response;
    }
}
