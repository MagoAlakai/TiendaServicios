namespace TiendaServicios.Api.Libro.Mapping;

public static class LibroModelStaticMappingServer
{
    public static LibroModel MapToLibroModel(this LibreriaMaterialEntity libro)
    {
        TypeAdapterConfig<LibreriaMaterialEntity, LibroModel>.NewConfig().IgnoreNullValues(true);
        LibroModel libro_model = TypeAdapter.Adapt<LibreriaMaterialEntity, LibroModel>(libro);
        return libro_model;
    }
}
