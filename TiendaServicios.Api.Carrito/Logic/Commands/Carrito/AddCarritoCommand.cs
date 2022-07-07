namespace TiendaServicios.Api.Carrito.Logic.Commands.Carrito;

public class AddCarritoCommand : ICommand<AddCarritoSesionResponse, AddCarritoSesionRequest>
{
    private readonly AddCarritoSesionRequest _request;
    private readonly ContextoCarrito _ctx;
    public AddCarritoCommand(AddCarritoSesionRequest request, ContextoCarrito ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<AddCarritoSesionResponse>> RunCommandAsync()
    {
        CarritoSesion carrito_sesion = _request.CarritoSesionModel.MapToAddCarritoSesionModelFromRequest();

        _ctx.Add(carrito_sesion);
        await _ctx.SaveChangesAsync();

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

        return CommandValueObject<AddCarritoSesionResponse>.Success(add_libro_response);
    }
}
