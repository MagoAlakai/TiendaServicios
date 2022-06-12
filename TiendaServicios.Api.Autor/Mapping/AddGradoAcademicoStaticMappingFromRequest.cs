using Google.Protobuf.WellKnownTypes;

namespace TiendaServicios.Api.Autor.Mapping;
public static class AddGradoAcademicoStaticMappingFromRequest
{
    public static GradoAcademico MapToAddGradoAcademicoModelFromRequest(this GradoAcademicoModel grado_academico_model)
    {
        TypeAdapterConfig<GradoAcademicoModel, GradoAcademico>.NewConfig().IgnoreNullValues(true)
            .Map(x => x.AutorLibroId, src => src.AutorLibroId)
            .Map(x => x.Nombre, src => src.Nombre)
            .Map(x => x.CentroAcademico, src => src.CentroAcademico)
            .Map(x => x.FechaGrado, src => src.FechaGrado.ToDateTime().ToUniversalTime())
            .Map(x => x.GradoAcademicoGuid, src => src.GradoAcademicoGuid)
            .Map(x => x.GradoAcademicoId, src => src.GradoAcademicoId);
        DefaultStaticMappingRules.SetDefaultMappings();
        GradoAcademico grado_academico = TypeAdapter.Adapt<GradoAcademicoModel, GradoAcademico>(grado_academico_model);
        return grado_academico;
    }
}
