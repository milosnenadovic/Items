using FluentValidation;
using Items.Shared.Errors;

namespace Items.Application.Items.Queries.GetItems;

public class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
{
	public GetItemsQueryValidator()
	{
		RuleFor(v => v.FilterName)
			.MaximumLength(48).WithMessage(Errors.InvalidData.NameLongerThan48);
		RuleFor(v => v.FilterDescription)
			.MaximumLength(96).WithMessage(Errors.InvalidData.DescriptionLongerThan96);
		RuleFor(v => v.PageNumber)
			.NotNull().WithMessage(Errors.Required.PageNumber)
			.GreaterThan(0).WithMessage(Errors.InvalidData.PageNumber);
		RuleFor(v => v.PageSize)
			.NotNull().WithMessage(Errors.Required.PageSize)
			.GreaterThan(0).WithMessage(Errors.InvalidData.PageSize);
	}
}
