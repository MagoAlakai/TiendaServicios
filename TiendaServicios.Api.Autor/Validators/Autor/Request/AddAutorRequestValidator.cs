using FluentValidation;

namespace TiendaServicios.Api.Autor.Validators.Autor.Request;
public class AddAutorRequestValidator : AbstractValidator<AddAutorRequest>
{
    public AddAutorRequestValidator()
    {
        RuleFor(request => request.AutorModel)
            .NotNull().SetValidator(new AutorModelValidator())
            .WithMessage("Is not a valid Autor").WithSeverity(FluentValidation.Severity.Warning);
    }
}
