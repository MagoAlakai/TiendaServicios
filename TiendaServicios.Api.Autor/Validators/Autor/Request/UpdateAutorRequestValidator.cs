using FluentValidation;

namespace TiendaServicios.Api.Autor.Validators.Autor.Request;

public class UpdateAutorRequestValidator : AbstractValidator<UpdateAutorRequest>
{
    public UpdateAutorRequestValidator()
    {
        RuleFor(request => request.AutorLibroId)
            .NotNull()
            .WithMessage("Is not a valid AutorLibroId").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(request => request.AutorModel)
            .NotNull().SetValidator(new AutorModelValidator())
            .WithMessage("Is not a valid Autor").WithSeverity(FluentValidation.Severity.Warning);
    }
}