namespace TiendaServicios.Api.Carrito.API.Services;
public class CarritoSesionService : CarritoSesionServices.CarritoSesionServicesBase
{
    private readonly ContextoCarrito _carritoContext;
    private readonly ILogger<CarritoSesionService> _logger;

    public CarritoSesionService(ContextoCarrito? carritoContext, ILogger<CarritoSesionService>? logger)
    {
        _carritoContext = carritoContext ?? throw new ArgumentNullException(nameof(carritoContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public override async Task<GetCarritoSesionResponse> GetCarritoSesion(GetCarritoSesionRequest request, ServerCallContext context)
    {
        GetCarritoQuery query = new(request, _carritoContext);
        QueryValueObject<GetCarritoSesionResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
    public override async Task<GetAllCarritosSesionResponse> GetAllCarritosSesion(GetAllCarritosSesionRequest request, ServerCallContext context)
    {
        GetAllCarritosQuery query = new(request, _carritoContext);
        QueryValueObject<GetAllCarritosSesionResponse> response = await query.RunQueryAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
    public override async Task<AddCarritoSesionResponse> AddCarritoSesion(AddCarritoSesionRequest request, ServerCallContext context)
    {
        AddCarritoCommand command = new(request, _carritoContext);
        CommandValueObject<AddCarritoSesionResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
    public override async Task<UpdateCarritoSesionResponse> UpdateCarritoSesion(UpdateCarritoSesionRequest request, ServerCallContext context)
    {
        UpdateCarritoCommand command = new(request, _carritoContext);
        CommandValueObject<UpdateCarritoSesionResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
    public override async Task<DeleteCarritoSesionResponse> DeleteCarritoSesion(DeleteCarritoSesionRequest request, ServerCallContext context)
    {
        DeleteCarritoCommand command = new(request, _carritoContext);
        CommandValueObject<DeleteCarritoSesionResponse> response = await command.RunCommandAsync();
        return response.IsSuccessfull is false
            ? throw new RpcException(new Status(StatusCode.Internal, response.Error))
            : response.Value;
    }
}
