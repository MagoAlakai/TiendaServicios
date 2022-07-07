namespace TiendaServicios.Api.Libro.Logic.Queries.Libro;

public sealed class GetAllLibrosQuery : IQuery<LibroModelListResponse, GetAllLibrosRequest>
{
    private readonly GetAllLibrosRequest _request;
    private readonly ContextoLibreria _ctx;
    public GetAllLibrosQuery(GetAllLibrosRequest request, ContextoLibreria ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<LibroModelListResponse>> RunQueryAsync()
    {
        List<LibreriaMaterialEntity>? libreria_list = await _ctx.LibreriaMaterialEntity.ToListAsync();
        IEnumerable<LibroModel>? libro_model_list = libreria_list.Select(x => x.MapToLibroModel());

        LibroModelListResponse get_all_libros_response = libro_model_list is null
            ? new LibroModelListResponse
            {
                Success = false,
                LibroModelList = { libro_model_list },
            }
            : new LibroModelListResponse
            {
                Success = true,
                LibroModelList = { libro_model_list },
            };

        return QueryValueObject<LibroModelListResponse>.Success(get_all_libros_response);
    }
}
