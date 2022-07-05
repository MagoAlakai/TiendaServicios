namespace TiendaServicios.Api.Autor.Logic.Queries;

public interface IQuery<T, R>
{
    Task<QueryValueObject<T>> RunQueryAsync();
}
