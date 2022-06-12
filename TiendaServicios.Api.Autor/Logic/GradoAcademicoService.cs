namespace TiendaServicios.Api.Autor.Logic;

public class GradoAcademicoService : GradoAcademicoServices.GradoAcademicoServicesBase
{
    private readonly ContextoAutor _autorsContext;
    private readonly ILogger<AutorService> _logger;

    public GradoAcademicoService(ContextoAutor? autorsContext, ILogger<AutorService>? logger)
    {
        _autorsContext = autorsContext ?? throw new ArgumentNullException(nameof(autorsContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<AddGradoAcademicoResponse> AddGradoAcademico(AddGradoAcademicoRequest request, ServerCallContext context)
    {
        GradoAcademico grado_academico = request.GradoAcademicoModel.MapToAddGradoAcademicoModelFromRequest();

        _autorsContext.Add(grado_academico);
        await _autorsContext.SaveChangesAsync();

        GradoAcademicoModel grado_academico_model = grado_academico.MapToGradoAcademicoModel();

        AddGradoAcademicoResponse add_grado_academico_response = grado_academico_model is null
            ? new AddGradoAcademicoResponse
            {
                Success = false,
                GradoAcademicoModel = grado_academico_model,
            }
            : new AddGradoAcademicoResponse
            {
                Success = true,
                GradoAcademicoModel = grado_academico_model,
            };

        return add_grado_academico_response;
    }
}
