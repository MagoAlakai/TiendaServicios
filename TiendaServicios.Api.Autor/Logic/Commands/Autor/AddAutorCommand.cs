namespace TiendaServicios.Api.Autor.Logic.Commands.Autor;
public sealed class AddAutorCommand : ICommand<AddAutorResponse, AddAutorRequest>
{
    private readonly AddAutorRequest _request;
    private readonly ContextoAutor _ctx;

    public AddAutorCommand(AddAutorRequest request, ContextoAutor ctx)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    public async Task<CommandValueObject<AddAutorResponse>> RunCommandAsync()
    {
        AutorLibro autor = _request.AutorModel.MapToAddAutorModelFromRequest();

        _ctx.Add(autor);
        await _ctx.SaveChangesAsync();

        AutorModel autor_model = autor.MapToAutorModel();

        AddAutorResponse add_autor_response = autor_model is null
            ? new AddAutorResponse
            {
                Success = false,
                AutorModel = autor_model
            }
            : new AddAutorResponse
            {
                Success = true,
                AutorModel = autor_model
            };

        return CommandValueObject<AddAutorResponse>.Success(add_autor_response);
    }
}
