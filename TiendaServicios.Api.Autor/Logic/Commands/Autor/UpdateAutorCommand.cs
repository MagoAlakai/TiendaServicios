namespace TiendaServicios.Api.Autor.Logic.Commands.Autor;

public class UpdateAutorCommand : ICommand<UpdateAutorResponse, UpdateAutorRequest>
{
    private readonly UpdateAutorRequest _request;
    private readonly ContextoAutor _ctx;

    public UpdateAutorCommand(UpdateAutorRequest request, ContextoAutor ctx)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    public async Task<CommandValueObject<UpdateAutorResponse>> RunCommandAsync()
    {
        AutorLibro? autor = await _ctx.AutorLibro.FindAsync(_request.AutorLibroId);

        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={_request.AutorLibroId} is not found"));
        }

        AutorLibro autor_mapped = _request.AutorModel.MapToUpdateAutorModelFromRequest();

        _ctx.ChangeTracker.Clear();
        _ctx.Update(autor_mapped);
        await _ctx.SaveChangesAsync();

        AutorModel autor_model = autor_mapped.MapToAutorModel();

        UpdateAutorResponse update_autor_response = autor_model is null
            ? new UpdateAutorResponse
            {
                Success = false,
                AutorModel = autor_model,
            }
            : new UpdateAutorResponse
            {
                Success = true,
                AutorModel = autor_model,
            };

        return CommandValueObject<UpdateAutorResponse>.Success(update_autor_response);
    }
}
