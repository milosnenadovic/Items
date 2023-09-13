using Items.Infrastructure.Interfaces;
using Items.Shared.Response;
using Items.Shared.Errors;
using MediatR;

namespace Items.Application.Items.Commands.UpdateItem;

public record UpdateItemCommand : IRequest<IResponse<bool>>
{
	public UpdateItemCommand() { }

	public UpdateItemCommand(int id, string name, string description, int categoryId, string producer, string supplier, decimal price, bool active)
	{
		Id = id;
		Name = name;
		Description = description;
		CategoryId = categoryId;
		Producer = producer;
		Supplier = supplier;
		Price = price;
		Active = active;
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public int CategoryId { get; set; }
	public string Producer { get; set; }
	public string Supplier { get; set; }
	public decimal Price { get; set; }
	public bool Active { get; set; }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, IResponse<bool>>
{
	private readonly IItemService _itemService;
	private readonly ICategoryService _categoryService;

	public UpdateItemCommandHandler(IItemService itemService, ICategoryService categoryService)
	{
		_itemService = itemService;
		_categoryService = categoryService;
	}

	public async Task<IResponse<bool>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
	{
		var existingItem = await _itemService.Existing(request.Name, request.CategoryId);
		if (!existingItem) return new ErrorResponse<bool>((int)ErrorCodes.CantFind, ErrorCodes.CantFind.ToString(), Errors.CantFind.Item);

		var existingCategory = await _categoryService.Existing(request.CategoryId);
		if (!existingCategory) return new ErrorResponse<bool>((int)ErrorCodes.CantFind, ErrorCodes.CantFind.ToString(), Errors.CantFind.Category);

		var savedItem = await _itemService.Update(request.Id, request.Name, request.Description, request.CategoryId, request.Producer, request.Supplier, request.Price, request.Active);
		if (!savedItem) return new ErrorResponse<bool>((int)ErrorCodes.DatabaseUpdate, ErrorCodes.DatabaseUpdate.ToString(), Errors.DatabaseUpdate.Item);

		return new SuccessResponse<bool>(await Task.FromResult(true));
	}
}
