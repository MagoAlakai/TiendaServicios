namespace TiendaServicios.Api.Carrito.Logic.Commands.Carrito;

public sealed class DeleteCarritoCommand : ICommand<DeleteCarritoSesionResponse, DeleteCarritoSesionResponse>
{
    private readonly DeleteCarritoSesionRequest _request;
    private readonly ContextoCarrito _ctx;
    public DeleteCarritoCommand(DeleteCarritoSesionRequest request, ContextoCarrito ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<DeleteCarritoSesionResponse>> RunCommandAsync()
    {
        CarritoSesion? carrito_sesion = await _ctx.CarritoSesion.FindAsync(_request.CarritoSesionId);
        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={_request.CarritoSesionId} is not found"));
        }

        _ctx.Remove(carrito_sesion);
        int deleteCount = await _ctx.SaveChangesAsync();

        DeleteCarritoSesionResponse update_autor_response = new DeleteCarritoSesionResponse
        {
            Success = deleteCount > 0
        };

        return CommandValueObject<DeleteCarritoSesionResponse>.Success(update_autor_response);
    }
}
