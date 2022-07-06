namespace TiendaServicios.Api.Autor.API.Services;

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
        GetAutorQuery query = new(request, _autorsContext); // arrange
        QueryValueObject<GetAutorResponse> response = await query.RunQueryAsync(); // act
        return response.IsSuccessfull is false // assert
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<AutorModelListResponse> GetAllAutors(GetAllAutorsRequest request, ServerCallContext context)
    {
        GetAllAutorsQuery query = new(request, _autorsContext); // arrange
        QueryValueObject<AutorModelListResponse> response = await query.RunQueryAsync(); // act
        return response.IsSuccessfull is false // assert
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<AddAutorResponse> AddAutor(AddAutorRequest request, ServerCallContext context)
    {
        AddAutorCommand command = new(request, _autorsContext); // arrange
        CommandValueObject<AddAutorResponse> response = await command.RunCommandAsync(); // act
        return response.IsSuccessfull is false // assert
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<UpdateAutorResponse> UpdateAutor(UpdateAutorRequest request, ServerCallContext context)
    {
        UpdateAutorCommand command = new(request, _autorsContext);
        CommandValueObject<UpdateAutorResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }

    public override async Task<DeleteAutorResponse> DeleteAutor(DeleteAutorRequest request, ServerCallContext context)
    {
        DeleteAutorCommand command = new(request, _autorsContext);
        CommandValueObject<DeleteAutorResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
}
