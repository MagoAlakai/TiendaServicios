using FluentValidation;

namespace TiendaServicios.Api.Autor.Validators.Autor.Request;

public sealed class AutorModelValidator : AbstractValidator<AutorModel>
{
    public AutorModelValidator()
    {
        RuleFor(x => x.AutorLibroGuid)
            .NotNull().Must(NotBeEmptyGuid)
            .WithMessage("Is not a valid Guid").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(x => x.AutorLibroId)
            .NotNull()
            .WithMessage("Is not a valid Id").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(x => x.Nombre)
            .NotNull()
            .WithMessage("Is not a valid Name").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(x => x.Apellido)
            .NotNull()
            .WithMessage("Is not a valid Last Name").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(x => x.GradoAcademicoGuid)
            .NotNull().Must(NotBeEmptyGuid)
            .WithMessage("Is not a valid Guid").WithSeverity(FluentValidation.Severity.Warning);

        RuleFor(x => x.FechaNacimiento)
            .NotNull()
            .WithMessage("Is not a valid Date").WithSeverity(FluentValidation.Severity.Warning);
    }
    private bool NotBeEmptyGuid(string guid) => Guid.Empty.Equals(Guid.Parse(guid)) is not true;
}
