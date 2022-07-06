namespace TiendaServicios.Api.Libro.Logic.Queries;

public interface IQuery<T, R>
{
    Task<QueryValueObject<T>> RunQueryAsync();
}
