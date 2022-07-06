namespace TiendaServicios.Api.Libro.Logic.Commands;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Response Type</typeparam>
/// <typeparam name="R">Request Type</typeparam>
public interface ICommand<T, R>
{
    Task<CommandValueObject<T>> RunCommandAsync();
}
