using FluentValidation;
using Items.Shared.Errors;

namespace Items.Application.Categories.Queries.GetActiveCategories;

public class GetActiveCategoriesQueryValidator : AbstractValidator<GetActiveCategoriesQuery>
{
	public GetActiveCategoriesQueryValidator()
	{
		RuleFor(v => v.FilterName)
			.MaximumLength(48).WithMessage(Errors.InvalidData.NameLongerThan48);
	}
}
