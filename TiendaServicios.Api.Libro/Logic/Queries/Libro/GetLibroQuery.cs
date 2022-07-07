namespace TiendaServicios.Api.Libro.Logic.Queries.Libro;
public sealed class GetLibroQuery : IQuery<GetLibroResponse, GetLibroRequest>
{
    private readonly GetLibroRequest _request;
    private readonly ContextoLibreria _ctx;
    public GetLibroQuery(GetLibroRequest request, ContextoLibreria ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<GetLibroResponse>> RunQueryAsync()
    {
        LibreriaMaterialEntity? libro = await _ctx.LibreriaMaterialEntity.FindAsync(_request.LibroId);
        if (libro is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Libro with ID={_request.LibroId} is not found"));
        }

        LibroModel libro_model = libro.MapToLibroModel();

        GetLibroResponse get_libro_response = libro_model is null
            ? new GetLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new GetLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return QueryValueObject<GetLibroResponse>.Success(get_libro_response);
    }
}
