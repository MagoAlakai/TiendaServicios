namespace TiendaServicios.Api.Autor.Logic.Queries.Autor;

public sealed class GetAutorQuery : IQuery<GetAutorResponse, GetAutorRequest>
{
    public async Task<QueryValueObject<GetAutorResponse>> RunQueryAsync()
        => await Task.FromResult(QueryValueObject<GetAutorResponse>.Failed("Not implemented"));
}
