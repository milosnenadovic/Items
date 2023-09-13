using Items.Infrastructure.Interfaces;
using Items.Shared.DTO;
using Items.Shared.Response;
using Items.Shared.Errors;
using MediatR;
using Mapster;

namespace Items.Application.Items.Queries.GetItem;

public record GetItemQuery : IRequest<IResponse<ItemDto>>
{
	public GetItemQuery(int id)
	{
		Id = id;
	}

	public int Id { get; set; }
}

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, IResponse<ItemDto>>
{
	private readonly IItemService _itemService;

	public GetItemQueryHandler(IItemService itemService)
	{
		_itemService = itemService;
	}

	public async Task<IResponse<ItemDto>> Handle(GetItemQuery request, CancellationToken cancellationToken)
	{
		var existingItem = await _itemService.GetById(request.Id);
		if (existingItem is null)
			return new ErrorResponse<ItemDto>((int)ErrorCodes.CantFind, ErrorCodes.CantFind.ToString(), Errors.CantFind.Item);

		var item = existingItem.Adapt<ItemDto>();

		return new SuccessResponse<ItemDto>(item);
	}
}
