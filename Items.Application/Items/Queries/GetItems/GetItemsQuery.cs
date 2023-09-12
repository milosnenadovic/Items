using Items.Infrastructure.Interfaces;
using Items.Shared.DTO;
using Items.Shared.DTO.Common;
using Items.Shared.Enums;
using Items.Shared.Response;
using Items.Shared.Errors;
using MediatR;
using Mapster;

namespace Items.Application.Items.Queries.GetItems;

public record GetItemsQuery : BaseQueryStringParameters<GetItemsSortBy>, IRequest<IResponse<PaginatedList<ItemDto>>>
{
	public GetItemsQuery() { }

	public GetItemsQuery(string? filterName, string? filterDescription, int? category, decimal? minPrice, decimal? maxPrice, DateTime? createdFrom, DateTime? createdTo, bool? active)
	{
		FilterName = filterName;
		FilterDescription = filterDescription;
		Category = category;
		MinPrice = minPrice;
		MaxPrice = maxPrice;
		CreatedFrom = createdFrom;
		CreatedTo = createdTo;
		Active = active;
	}

	public string? FilterName { get; set; }
	public string? FilterDescription { get; set; }
	public int? Category { get; set; }
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public DateTime? CreatedFrom { get; set; }
	public DateTime? CreatedTo { get; set; }
	public bool? Active { get; set; }
}

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IResponse<PaginatedList<ItemDto>>>
{
	private readonly IItemService _itemService;

	public GetItemsQueryHandler(IItemService itemService)
	{
		_itemService = itemService;
	}

	public async Task<IResponse<PaginatedList<ItemDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
	{
		var existingItems = await _itemService.GetItems(request.FilterName, request.FilterDescription, request.Category, request.MinPrice, request.MaxPrice, request.CreatedFrom, request.CreatedTo, request.Active);
		if (existingItems is null) return new ErrorResponse<PaginatedList<ItemDto>>((int)ErrorCodes.DatabaseGet, ErrorCodes.DatabaseGet.ToString(), Errors.DatabaseGet.Item);

		var items = existingItems.Adapt<List<ItemDto>>();

		switch (request.SortBy)
		{
			case GetItemsSortBy.Name:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Name).ToList();
				else items = items.OrderBy(x => x.Name).ToList();
				break;
			case GetItemsSortBy.Description:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Description).ToList();
				else items = items.OrderBy(x => x.Description).ToList();
				break;
			case GetItemsSortBy.Category:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Category).ToList();
				else items = items.OrderBy(x => x.Category).ToList();
				break;
			case GetItemsSortBy.Producer:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Producer).ToList();
				else items = items.OrderBy(x => x.Producer).ToList();
				break;
			case GetItemsSortBy.Supplier:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Supplier).ToList();
				else items = items.OrderBy(x => x.Supplier).ToList();
				break;
			case GetItemsSortBy.Price:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Price).ToList();
				else items = items.OrderBy(x => x.Price).ToList();
				break;
			case GetItemsSortBy.Created:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Created).ToList();
				else items = items.OrderBy(x => x.Created).ToList();
				break;
			case GetItemsSortBy.LastModified:
				if (request.SortDescending) items = items.OrderByDescending(x => x.LastModified).ToList();
				else items = items.OrderBy(x => x.LastModified).ToList();
				break;
			default:
				if (request.SortDescending) items = items.OrderByDescending(x => x.Id).ToList();
				else items = items.OrderBy(x => x.Id).ToList();
				break;
		}

		var paginatedList = new PaginatedList<ItemDto>(items, request.PageNumber, request.PageSize);

		return new SuccessResponse<PaginatedList<ItemDto>>(paginatedList);
	}
}
