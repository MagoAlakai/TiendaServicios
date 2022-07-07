namespace TiendaServicios.Api.Carrito.Logic.Queries.Carrito;

public sealed class GetAllCarritosQuery : IQuery<GetAllCarritosSesionResponse, GetAllCarritosSesionRequest>
{
    private readonly GetAllCarritosSesionRequest _request;
    private readonly ContextoCarrito _ctx;
    public GetAllCarritosQuery(GetAllCarritosSesionRequest request, ContextoCarrito ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<GetAllCarritosSesionResponse>> RunQueryAsync()
    {
        List<CarritoSesion>? carrito_sesion_list = await _ctx.CarritoSesion.ToListAsync();
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

        return QueryValueObject<GetAllCarritosSesionResponse>.Success(get_all_carritos_sesion_response);
    }
}
