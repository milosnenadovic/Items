using Items.Shared.Errors;
using FluentValidation;

namespace Items.Application.Items.Commands.AddItem;

public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
{
    public AddItemCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage(Errors.Required.Name)
            .MaximumLength(48).WithMessage(Errors.InvalidData.NameLongerThan48);
        RuleFor(v => v.Description)
            .NotEmpty().WithMessage(Errors.Required.Description)
            .MaximumLength(96).WithMessage(Errors.InvalidData.DescriptionLongerThan96);
        RuleFor(v => v.CategoryId)
            .GreaterThan(0).WithMessage(Errors.InvalidData.CategoryId);
        RuleFor(v => v.Producer)
            .NotEmpty().WithMessage(Errors.Required.Producer)
            .MaximumLength(48).WithMessage(Errors.InvalidData.ProducerLongerThan48);
        RuleFor(v => v.Supplier)
            .NotEmpty().WithMessage(Errors.Required.Supplier)
            .MaximumLength(48).WithMessage(Errors.InvalidData.SupplierLongerThan48);
        RuleFor(v => v.Price)
            .NotNull().WithMessage(Errors.Required.Price)
            .GreaterThan(0).WithMessage(Errors.InvalidData.Price);
    }
}
