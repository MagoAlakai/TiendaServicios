namespace TiendaServicios.Api.Autor.Mapping
{
    public static class UpdateAutorStaticMappingFromRequest
    {
        public static AutorLibro MapToUpdateProductModelFromRequest(this AutorModel autor_model)
        {
            TypeAdapterConfig<AddAutorRequest, AutorLibro>.NewConfig().IgnoreNullValues(true);
            AutorLibro autor_libro = TypeAdapter.Adapt<AutorModel, AutorLibro>(autor_model);
            return autor_libro;
        }
    }
}
