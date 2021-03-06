namespace TiendaServicios.Api.Autor.Logic.Commands;

/// <summary>
/// CommandValueObject<T>.Success(value) where value is of type T
/// </summary>
/// <typeparam name="T"></typeparam>
public class CommandValueObject<T> // where T : struct
{
    public bool IsSuccessfull { get; init; }

    private T? _value;
    public T Value => IsSuccessfull is false || _value is null
        ? throw new Exception(Error)
        : _value;

    private string? _error;
    public string Error => IsSuccessfull is false || string.IsNullOrWhiteSpace(_error) is true
        ? "No error message provided"
        : _error;

    public static CommandValueObject<T> Success(T value) => new()
    {
        _value = value,
        IsSuccessfull = true
    };

    public static CommandValueObject<T> Failed(string error) => new()
    {
        _error = error,
        IsSuccessfull = false
    };
}
