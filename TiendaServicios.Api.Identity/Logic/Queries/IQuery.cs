namespace TiendaServicios.Api.Identity.Logic.Queries;

public interface IQuery<T, R>
{
    Task<QueryValueObject<T>> RunQueryAsync();
}
