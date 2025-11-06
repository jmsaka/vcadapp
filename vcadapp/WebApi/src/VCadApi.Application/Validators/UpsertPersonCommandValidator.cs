namespace VCadApi.Application.Validators;

public class UpsertPersonCommandValidator : AbstractValidator<UpsertPersonCommand>
{
    public UpsertPersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow.Date).WithMessage("BirthDate deve ser uma data passada.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .MaximumLength(200).WithMessage("Email deve ter no máximo 200 caracteres.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.MaritalStatus)
            .NotEmpty().WithMessage("MaritalStatus é obrigatório.")
            .MaximumLength(50).WithMessage("MaritalStatus deve ter no máximo 50 caracteres.");
    }
}