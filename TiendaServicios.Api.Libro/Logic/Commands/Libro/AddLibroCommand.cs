namespace TiendaServicios.Api.Libro.Logic.Commands.Libro;
public sealed class AddLibroCommand : ICommand<AddLibroResponse, AddLibroRequest>
{
    private readonly AddLibroRequest _request;
    private readonly ContextoLibreria _ctx;
    public AddLibroCommand(AddLibroRequest request, ContextoLibreria ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<AddLibroResponse>> RunCommandAsync()
    {
        LibreriaMaterialEntity libro = _request.LibroModel.MapToAddLibroModelFromRequest();

        _ctx.Add(libro);
        await _ctx.SaveChangesAsync();

        LibroModel libro_model = libro.MapToLibroModel();

        AddLibroResponse add_libro_response = libro_model is null
            ? new AddLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new AddLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return CommandValueObject<AddLibroResponse>.Success(add_libro_response);
    }
}
