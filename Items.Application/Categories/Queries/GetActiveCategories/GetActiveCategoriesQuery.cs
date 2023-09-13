using Items.Infrastructure.Interfaces;
using Items.Shared.DTO;
using Items.Shared.Response;
using Items.Shared.Errors;
using MediatR;
using Mapster;

namespace Items.Application.Categories.Queries.GetActiveCategories;

public record GetActiveCategoriesQuery : IRequest<IResponse<List<CategoryDto>>>
{
	public GetActiveCategoriesQuery() { }

	public GetActiveCategoriesQuery(string? filterName)
	{
		FilterName = filterName;
	}

	public string? FilterName { get; set; }
}

public class GetActiveCategoriesQueryHandler : IRequestHandler<GetActiveCategoriesQuery, IResponse<List<CategoryDto>>>
{
	private readonly ICategoryService _categoryService;

	public GetActiveCategoriesQueryHandler(ICategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	public async Task<IResponse<List<CategoryDto>>> Handle(GetActiveCategoriesQuery request, CancellationToken cancellationToken)
	{
		var existingCategories = await _categoryService.GetCategories(request.FilterName, createdFrom: null, createdTo: null, true);
		if (existingCategories is null)
			return new ErrorResponse<List<CategoryDto>>((int)ErrorCodes.DatabaseGet, ErrorCodes.DatabaseGet.ToString(), Errors.DatabaseGet.Category);

		var categories = existingCategories.Adapt<List<CategoryDto>>();

		return new SuccessResponse<List<CategoryDto>>(categories);
	}
}
