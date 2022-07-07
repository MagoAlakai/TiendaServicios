namespace TiendaServicios.Api.Carrito.Logic.Queries.Carrito;

public sealed class GetCarritoQuery : IQuery<GetCarritoSesionResponse, GetCarritoSesionRequest>
{
    private readonly GetCarritoSesionRequest _request;
    private readonly ContextoCarrito _ctx;
    public GetCarritoQuery(GetCarritoSesionRequest request, ContextoCarrito ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<GetCarritoSesionResponse>> RunQueryAsync()
    {
        CarritoSesion? carrito_sesion = await _ctx.CarritoSesion.FindAsync(_request.CarritoSesionId);
        if (carrito_sesion is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Carrito Sesion with ID={_request.CarritoSesionId} is not found"));
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

        return QueryValueObject<GetCarritoSesionResponse>.Success(get_carrito_sesion_response);
    }
}
