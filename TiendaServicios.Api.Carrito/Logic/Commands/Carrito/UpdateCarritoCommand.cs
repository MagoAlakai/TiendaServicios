namespace TiendaServicios.Api.Carrito.Logic.Commands.Carrito;

public sealed class UpdateCarritoCommand : ICommand<UpdateCarritoSesionResponse, UpdateCarritoSesionRequest>
{
    private readonly UpdateCarritoSesionRequest _request;
    private readonly ContextoCarrito _ctx;
    public UpdateCarritoCommand(UpdateCarritoSesionRequest request, ContextoCarrito ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<UpdateCarritoSesionResponse>> RunCommandAsync()
    {
        CarritoSesion? carrito_sesion = await _ctx.CarritoSesion.FindAsync(_request.CarritoSesionId);

        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={_request.CarritoSesionId} is not found"));
        }
        CarritoSesion update_carrito_sesion = _request.CarritoSesionModel.MapToUpdateCarritoSesionModelFromRequest();

        _ctx.ChangeTracker.Clear();
        _ctx.Update(update_carrito_sesion);
        await _ctx.SaveChangesAsync();

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

        return CommandValueObject<UpdateCarritoSesionResponse>.Success(update_carrito_sesion_response);
    }
}
