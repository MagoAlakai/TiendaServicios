namespace TiendaServicios.Api.Autor.Mapping;
public static class AddAutorStaticMappingFromRequest
{
    public static AutorLibro MapToAddAutorModelFromRequest(this AutorModel autor_model)
    {
        TypeAdapterConfig<AutorModel, AutorLibro>.NewConfig().IgnoreNullValues(true)
            .Map(x => x.AutorLibroId, src => src.AutorLibroId)
            .Map(x => x.Nombre, src => src.Nombre)
            .Map(x => x.Apellido, src => src.Apellido)
            .Map(x => x.AutorLibroGuid, src => src.AutorLibroGuid)
            .Map(x => x.GradoAcademicoGuid, src => src.GradoAcademicoGuid)
            .Map(x => x.FechaNacimiento, src => src.FechaNacimiento.ToDateTime().ToUniversalTime());
        DefaultStaticMappingRules.SetDefaultMappings();
        AutorLibro autor_libro = TypeAdapter.Adapt<AutorModel, AutorLibro>(autor_model);
        return autor_libro;
    }
}
