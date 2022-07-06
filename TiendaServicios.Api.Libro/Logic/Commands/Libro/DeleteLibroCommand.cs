namespace TiendaServicios.Api.Libro.Logic.Commands.Libro;

public class DeleteLibroCommand : ICommand<DeleteLibroResponse, DeleteLibroRequest>
{
    private readonly DeleteLibroRequest _request;
    private readonly ContextoLibreria _ctx;
    public DeleteLibroCommand(DeleteLibroRequest request, ContextoLibreria ctx)
    {
        _request = request;
        _ctx = ctx;
    }
    public async Task<CommandValueObject<DeleteLibroResponse>> RunCommandAsync()
    {
        LibreriaMaterialEntity? libro = await _ctx.LibreriaMaterialEntity.FindAsync(_request.LibroId);

        if (libro is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Libro with ID={_request.LibroId} is not found"));
        }

        _ctx.LibreriaMaterialEntity.Remove(libro);
        int deleteCount = await _ctx.SaveChangesAsync();

        DeleteLibroResponse update_libro_response = new DeleteLibroResponse
        {
            Success = deleteCount > 0
        };

        return CommandValueObject<DeleteLibroResponse>.Success(update_libro_response);
    }
}
