using Items.Infrastructure.Interfaces;
using Items.Shared.Response;
using Items.Shared.Errors;
using MediatR;

namespace Items.Application.Items.Commands.AddItem;

public record AddItemCommand : IRequest<IResponse<bool>>
{
	public AddItemCommand() { }

	public AddItemCommand(string name, string description, int categoryId, string producer, string supplier, decimal price)
	{
		Name = name;
		Description = description;
		CategoryId = categoryId;
		Producer = producer;
		Supplier = supplier;
		Price = price;
	}

	public string Name { get; set; }
	public string Description { get; set; }
	public int CategoryId { get; set; }
	public string Producer { get; set; }
	public string Supplier { get; set; }
	public decimal Price { get; set; }
}

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, IResponse<bool>>
{
	private readonly IItemService _itemService;
	private readonly ICategoryService _categoryService;

	public AddItemCommandHandler(IItemService itemService, ICategoryService categoryService)
	{
		_itemService = itemService;
		_categoryService = categoryService;
	}

	public async Task<IResponse<bool>> Handle(AddItemCommand request, CancellationToken cancellationToken)
	{
		var existingItem = await _itemService.Existing(request.Name, request.CategoryId);
		if (existingItem) return new ErrorResponse<bool>((int)ErrorCodes.AlreadyExists, ErrorCodes.AlreadyExists.ToString(), Errors.AlreadyExists.Item);

		var existingCategory = await _categoryService.Existing(request.CategoryId);
		if (!existingCategory) return new ErrorResponse<bool>((int)ErrorCodes.CantFind, ErrorCodes.CantFind.ToString(), Errors.CantFind.Category);

		var savedItem = await _itemService.Add(request.Name, request.Description, request.CategoryId, request.Producer, request.Supplier, request.Price);
		if (!savedItem) return new ErrorResponse<bool>((int)ErrorCodes.DatabaseAdd, ErrorCodes.DatabaseAdd.ToString(), Errors.DatabaseAdd.Item);

		return new SuccessResponse<bool>(await Task.FromResult(true));
	}
}
