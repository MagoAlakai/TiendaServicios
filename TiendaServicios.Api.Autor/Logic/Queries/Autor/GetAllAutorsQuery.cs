namespace TiendaServicios.Api.Autor.Logic.Queries.Autor;

public class GetAllAutorsQuery : IQuery<AutorModelListResponse, GetAllAutorsRequest>
{
    private readonly GetAllAutorsRequest _request;
    private readonly ContextoAutor _ctx;
    public GetAllAutorsQuery(GetAllAutorsRequest request, ContextoAutor ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<QueryValueObject<AutorModelListResponse>> RunQueryAsync()
    {
        List<AutorLibro>? autor_list = await _ctx.AutorLibro.ToListAsync();
        IEnumerable<AutorModel>? autor_model_list = (IEnumerable<AutorModel>?)autor_list.Select(x => x.MapToAutorModel());

        AutorModelListResponse get_all_autors_response = autor_model_list is null
            ? new AutorModelListResponse
            {
                Success = false,
                AutorModelList = { autor_model_list },
            }
            : new AutorModelListResponse
            {
                Success = true,
                AutorModelList = { autor_model_list },
            };

        return QueryValueObject<AutorModelListResponse>.Success(get_all_autors_response);
    }
}
