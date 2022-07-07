namespace TiendaServicios.Api.Libro.Logic.Commands.Libro;

public sealed class UpdateLibroCommand : ICommand<UpdateLibroResponse, UpdateLibroRequest>
{
    private readonly UpdateLibroRequest _request;
    private readonly ContextoLibreria _ctx;
    public UpdateLibroCommand(UpdateLibroRequest request, ContextoLibreria contextoLibreria)
    {
        _request = request;
        _ctx = contextoLibreria;
    }
    public async Task<CommandValueObject<UpdateLibroResponse>> RunCommandAsync()
    {
        LibreriaMaterialEntity? autor = await _ctx.LibreriaMaterialEntity.FindAsync(_request.LibroId);

        if (autor is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Autor with ID={_request.LibroId} is not found"));
        }

        LibreriaMaterialEntity libro_mapped = _request.LibroModel.MapToUpdateLibroModelFromRequest();

        _ctx.ChangeTracker.Clear();
        _ctx.Update(libro_mapped);
        await _ctx.SaveChangesAsync();

        LibroModel libro_model = libro_mapped.MapToLibroModel();

        UpdateLibroResponse update_libro_response = libro_model is null
            ? new UpdateLibroResponse
            {
                Success = false,
                LibroModel = libro_model,
            }
            : new UpdateLibroResponse
            {
                Success = true,
                LibroModel = libro_model,
            };

        return CommandValueObject<UpdateLibroResponse>.Success(update_libro_response);
    }
}
