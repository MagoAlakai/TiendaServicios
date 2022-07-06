namespace TiendaServicios.Api.Autor.Logic.Commands.Autor;

public class DeleteAutorCommand : ICommand<DeleteAutorResponse, DeleteAutorRequest>
{
    private readonly DeleteAutorRequest _request;
    private readonly ContextoAutor _ctx;

    public DeleteAutorCommand(DeleteAutorRequest request, ContextoAutor ctx)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }
    public async Task<CommandValueObject<DeleteAutorResponse>> RunCommandAsync()
    {
        AutorLibro? autor = await _ctx.AutorLibro.FindAsync(_request.AutorLibroId);

        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={_request.AutorLibroId} is not found"));
        }

        _ctx.AutorLibro.Remove(autor);
        int deleteCount = await _ctx.SaveChangesAsync();

        DeleteAutorResponse delete_autor_response = new DeleteAutorResponse
        {
            Success = deleteCount > 0
        };

        return CommandValueObject<DeleteAutorResponse>.Success(delete_autor_response);
    }
}
