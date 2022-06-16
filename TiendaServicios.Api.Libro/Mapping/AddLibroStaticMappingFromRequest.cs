namespace TiendaServicios.Api.Libro.Mapping;

public static class AddLibroStaticMappingFromRequest
{
    public static LibreriaMaterialEntity MapToAddLibroModelFromRequest(this LibroModel libro_model)
    {
        TypeAdapterConfig<LibroModel, LibreriaMaterialEntity>.NewConfig().IgnoreNullValues(true)
            .Map(x => x.Id, src => src.Id)
            .Map(x => x.Titulo, src => src.Titulo)
            .Map(x => x.LibreriaMaterialGuid, src => src.LibreriaMaterialGuid)
            .Map(x => x.FechaPublicacion, src => src.FechaPublicacion.ToDateTime().ToUniversalTime())
            .Map(x => x.AutorLibro, src => src.AutorLibro);
        DefaultStaticMappingRules.SetDefaultMappings();
        LibreriaMaterialEntity entity_libro = TypeAdapter.Adapt<LibroModel, LibreriaMaterialEntity>(libro_model);
        return entity_libro;
    }
}
