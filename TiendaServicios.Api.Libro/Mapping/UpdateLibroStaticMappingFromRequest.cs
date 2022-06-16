namespace TiendaServicios.Api.Libro.Mapping;

public static class UpdateLibroStaticMappingFromRequest
{
    public static LibreriaMaterialEntity MapToUpdateLibroModelFromRequest(this LibroModel libro_model)
    {
        TypeAdapterConfig<LibroModel, LibreriaMaterialEntity>.NewConfig().IgnoreNullValues(true);
        LibreriaMaterialEntity entity_libro = TypeAdapter.Adapt<LibroModel, LibreriaMaterialEntity>(libro_model);
        return entity_libro;
    }
}
