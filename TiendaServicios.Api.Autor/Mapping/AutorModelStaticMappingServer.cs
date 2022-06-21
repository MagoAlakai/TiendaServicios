namespace TiendaServicios.Api.Autor.Mapping;
public static class AutorModelStaticMappingServer
{
    public static AutorModel MapToAutorModel(this AutorLibro autor_libro)
    {
        TypeAdapterConfig<AutorLibro, AutorModel>.NewConfig().IgnoreNullValues(true);
        AutorModel autor_model = TypeAdapter.Adapt<AutorLibro, AutorModel>(autor_libro);
        return autor_model;
    }
}
