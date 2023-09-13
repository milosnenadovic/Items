using FluentValidation;
using Items.Shared.Errors;

namespace Items.Application.Items.Queries.GetItem;

public class GetItemQueryValidator : AbstractValidator<GetItemQuery>
{
	public GetItemQueryValidator()
	{
		RuleFor(v => v.Id)
			.GreaterThan(0).WithMessage(Errors.InvalidData.Id);
	}
}
