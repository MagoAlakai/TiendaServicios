namespace TiendaServicios.Api.Carrito.Logic.Queries;

public interface IQuery<T, R>
{
    Task<QueryValueObject<T>> RunQueryAsync();
}
