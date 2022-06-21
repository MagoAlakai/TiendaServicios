namespace TiendaServicios.Api.Carrito.Logic;
public class CarritoSesionService : CarritoSesionServices.CarritoSesionServicesBase
{
    private readonly ContextoCarrito _carritoContext;
    private readonly ILogger<CarritoSesionService> _logger;

    public CarritoSesionService(ContextoCarrito? carritoContext, ILogger<CarritoSesionService>? logger)
    {
        _carritoContext = carritoContext ?? throw new ArgumentNullException(nameof(carritoContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<AddCarritoSesionResponse> AddCarritoSesion(AddCarritoSesionRequest request, ServerCallContext context)
    {
        CarritoSesion carrito_sesion = request.CarritoSesionModel.MapToAddCarritoSesionModelFromRequest();

        _carritoContext.Add(carrito_sesion);
        await _carritoContext.SaveChangesAsync();

        CarritoSesionModel carrito_sesion_model = carrito_sesion.MapToCarritoSesionModel();

        AddCarritoSesionResponse add_libro_response = carrito_sesion_model is null
            ? new AddCarritoSesionResponse
            {
                Success = false,
                CarritoSesionModel = carrito_sesion_model,
            }
            : new AddCarritoSesionResponse
            {
                Success = true,
                CarritoSesionModel = carrito_sesion_model,
            };

        return add_libro_response;
    }

    public override async Task<GetCarritoSesionResponse> GetCarritoSesion(GetCarritoSesionRequest request, ServerCallContext context)
    {
        CarritoSesion? carrito_sesion = await _carritoContext.CarritoSesion.FindAsync(request.CarritoSesionId);
        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={request.CarritoSesionId} is not found"));
        }

        CarritoSesionModel carrito_sesion_model = carrito_sesion.MapToCarritoSesionModel();

        GetCarritoSesionResponse get_carrito_sesion_response = carrito_sesion_model is null
            ? new GetCarritoSesionResponse
            {
                Success = false,
                CarritoSesionModel = carrito_sesion_model,
            }
            : new GetCarritoSesionResponse
            {
                Success = true,
                CarritoSesionModel = carrito_sesion_model,
            };

        return get_carrito_sesion_response;
    }

    public override async Task<GetAllCarritosSesionResponse> GetAllCarritosSesion(GetAllCarritosSesionRequest request, ServerCallContext context)
    {
        List<CarritoSesion>? carrito_sesion_list = await _carritoContext.CarritoSesion.ToListAsync();
        IEnumerable<CarritoSesionModel>? carrito_sesion_model_list = (IEnumerable<CarritoSesionModel>?)carrito_sesion_list.Select(x => x.MapToCarritoSesionModel());

        GetAllCarritosSesionResponse get_all_carritos_sesion_response = carrito_sesion_model_list is null
            ? new GetAllCarritosSesionResponse
            {
                Success = false,
                CarritoSesionModelList = { carrito_sesion_model_list },
            }
            : new GetAllCarritosSesionResponse
            {
                Success = true,
                CarritoSesionModelList = { carrito_sesion_model_list },
            };

        return get_all_carritos_sesion_response;
    }

    public override async Task<UpdateCarritoSesionResponse> UpdateCarritoSesion(UpdateCarritoSesionRequest request, ServerCallContext context)
    {
        CarritoSesion? carrito_sesion = await _carritoContext.CarritoSesion.FindAsync(request.CarritoSesionId);
        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={request.CarritoSesionId} is not found"));
        }

        CarritoSesion update_carrito_sesion = request.CarritoSesionModel.MapToUpdateCarritoSesionModelFromRequest();

        _carritoContext.ChangeTracker.Clear();
        _carritoContext.Update(update_carrito_sesion);
        await _carritoContext.SaveChangesAsync();

        CarritoSesionModel carrito_sesion_model = update_carrito_sesion.MapToCarritoSesionModel();

        UpdateCarritoSesionResponse update_carrito_sesion_response = carrito_sesion_model is null
            ? new UpdateCarritoSesionResponse
            {
                Success = false,
                CarritoSesionModel = carrito_sesion_model,
            }
            : new UpdateCarritoSesionResponse
            {
                Success = true,
                CarritoSesionModel = carrito_sesion_model,
            };

        return update_carrito_sesion_response;
    }

    public override async Task<DeleteCarritoSesionResponse> DeleteCarritoSesion(DeleteCarritoSesionRequest request, ServerCallContext context)
    {
        CarritoSesion? carrito_sesion = await _carritoContext.CarritoSesion.FindAsync(request.CarritoSesionId);
        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={request.CarritoSesionId} is not found"));
        }

        _carritoContext.Remove(carrito_sesion);
        int deleteCount = await _carritoContext.SaveChangesAsync();

        DeleteCarritoSesionResponse update_autor_response = new DeleteCarritoSesionResponse
        {
            Success = deleteCount > 0
        };

        return update_autor_response;
    }
}
