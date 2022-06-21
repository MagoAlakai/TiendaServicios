namespace TiendaServicios.Api.Autor.Mapping;
public static class GradoAcademicoModelStaticMappingServer
{
    public static GradoAcademicoModel MapToGradoAcademicoModel(this GradoAcademico grado_academico)
    {
        TypeAdapterConfig<GradoAcademico, GradoAcademicoModel>.NewConfig().IgnoreNullValues(true);
        GradoAcademicoModel grado_academico_model = TypeAdapter.Adapt<GradoAcademico, GradoAcademicoModel>(grado_academico);
        return grado_academico_model;
    }
}
