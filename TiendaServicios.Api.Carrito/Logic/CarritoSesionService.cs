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
}
