namespace TiendaServicios.Api.Autor.Logic.Queries.Autor;

public sealed class GetAutorQuery : IQuery<GetAutorResponse, GetAutorRequest>
{
    private readonly GetAutorRequest _request;
    private readonly ContextoAutor _ctx;

    public GetAutorQuery(GetAutorRequest request, ContextoAutor ctx)
    {
        _request = request;
        _ctx = ctx;
    }

    public async Task<QueryValueObject<GetAutorResponse>> RunQueryAsync()
    {
        AutorLibro? autor = await _ctx.AutorLibro.FindAsync(_request.AutorLibroId);
        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={_request.AutorLibroId} is not found"));
        }

        AutorModel autor_model = autor.MapToAutorModel();

        GetAutorResponse get_autor_response = autor_model is null
            ? new GetAutorResponse
            {
                Success = false,
                AutorModel = autor_model,
            }
            : new GetAutorResponse
            {
                Success = true,
                AutorModel = autor_model,
            };

        return QueryValueObject<GetAutorResponse>.Success(get_autor_response);
    }
     
}
