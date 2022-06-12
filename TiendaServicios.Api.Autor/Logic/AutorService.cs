namespace TiendaServicios.Api.Autor.Logic;

public class AutorService : AutorServices.AutorServicesBase
{
    private readonly ContextoAutor _autorsContext;
    private readonly ILogger<AutorService> _logger;

    public AutorService(ContextoAutor? autorsContext, ILogger<AutorService>? logger)
    {
        _autorsContext = autorsContext ?? throw new ArgumentNullException(nameof(autorsContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<GetAutorResponse> GetAutor(GetAutorRequest request, ServerCallContext context)
    {
        AutorLibro? autor = await _autorsContext.AutorLibro.FindAsync(request.AutorLibroGuid);
        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={request.AutorLibroGuid} is not found"));
        }

        AutorModel autor_model = autor.MapToAutorModel();

        GetAutorResponse get_autor_response = autor_model is null
            ? new GetAutorResponse
            {
                Success = false,
                AutorModel = autor_model,
            }
            : new GetAutorResponse
            {
                Success = true,
                AutorModel = autor_model,
            };

        return get_autor_response;
    }

    public override async Task<AutorModelListResponse> GetAllAutors(GetAllAutorsRequest request, ServerCallContext context)
    {
        List<AutorLibro>? autor_list = await _autorsContext.AutorLibro.ToListAsync();

        IEnumerable<AutorModel>? autor_model_list = autor_list.Select(x => x.MapToAutorModel());


        AutorModelListResponse get_all_autors_response = autor_model_list is null
            ? new AutorModelListResponse
            {
                Success = false,
                AutorModelList = { autor_model_list },
            }
            : new AutorModelListResponse
            {
                Success = true,
                AutorModelList = { autor_model_list },
            };

        return get_all_autors_response;
    }

    public override async Task<AddAutorResponse> AddAutor(AddAutorRequest request, ServerCallContext context)
    {
        AutorLibro autor = request.AutorModel.MapToAddProductModelFromRequest();

        _autorsContext.Add(autor);
        await _autorsContext.SaveChangesAsync();

        AutorModel autor_model = autor.MapToAutorModel();

        AddAutorResponse add_autor_response = autor_model is null
            ? new AddAutorResponse
            {
                Success = false,
                AutorModel = autor_model,
            }
            : new AddAutorResponse
            {
                Success = true,
                AutorModel = autor_model,
            };

        return add_autor_response;
    }

    public override async Task<UpdateAutorResponse> UpdateAutor(UpdateAutorRequest request, ServerCallContext context)
    {
        GetAutorRequest add_autor_request = new()
        {
            AutorLibroGuid = request.AutorLibroGuid
        };
        GetAutorResponse get_autor_response = await GetAutor(add_autor_request, context);

        AutorLibro autor = get_autor_response.AutorModel.MapToUpdateProductModelFromRequest();

        bool autor_exist = await _autorsContext.AutorLibro.AnyAsync(p => p.AutorLibroGuid.Equals(autor.AutorLibroGuid));
        if (autor_exist is false)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={autor.AutorLibroGuid} is not found"));
        }

        _autorsContext.Update(autor);
        await _autorsContext.SaveChangesAsync();

        AutorModel autor_model = autor.MapToAutorModel();

        UpdateAutorResponse update_autor_response = autor_model is null
            ? new UpdateAutorResponse
            {
                Success = false,
                AutorModel = autor_model,
            }
            : new UpdateAutorResponse
            {
                Success = true,
                AutorModel = autor_model,
            };

        return update_autor_response;
    }

    public override async Task<DeleteAutorResponse> DeleteAutor(DeleteAutorRequest request, ServerCallContext context)
    {
        AutorLibro? autor = await _autorsContext.AutorLibro.FindAsync(request.AutorLibroGuid);

        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={request.AutorLibroGuid} is not found"));
        }

        _autorsContext.AutorLibro.Remove(autor);
        int deleteCount = await _autorsContext.SaveChangesAsync();

        DeleteAutorResponse update_autor_response = new DeleteAutorResponse
        {
            Success = deleteCount > 0
        };

        return update_autor_response;
    }
}
