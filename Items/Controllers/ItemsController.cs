using Microsoft.AspNetCore.Mvc;
using Items.Shared.Contracts.Items;
using Items.Application.Items.Queries.GetItems;
using Items.Application.Items.Commands.AddItem;
using Items.Application.Items.Commands.UpdateItem;
using MapsterMapper;
using MediatR;
using Items.Application.Items.Queries.GetItem;
using Items.Application.Categories.Queries.GetActiveCategories;
using Items.Models;
using System.Diagnostics;
using Items.Controllers.Base;

namespace Items.Controllers;

public class ItemsController : BaseController
{
	public ItemsController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

	[HttpGet]
	public async Task<IActionResult> Index([FromQuery] GetItemsRequest request)
	{
		var query = Mapper.Map<GetItemsQuery>(request);
		var result = await Mediator.Send(query);
		var response = ConvertToResponse(result);
		return View("Items", response);
	}

	[HttpGet]
	public async Task<IActionResult> EditItem(int itemId)
	{
		var item = await Mediator.Send(new GetItemQuery(itemId));
		var categories = await Mediator.Send(new GetActiveCategoriesQuery());
		ViewData["Categories"] = categories.Result;
		UpdateItemRequest responseModel = new()
		{
			Id = item.Result.Id,
			Name = item.Result.Name,
			Description = item.Result.Description,
			CategoryId = item.Result.CategoryId,
			Producer = item.Result.Producer,
			Supplier = item.Result.Supplier,
			Price = item.Result.Price,
			Active = item.Result.Active
		};
		return View("EditItem", responseModel);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddItemRequest request)
	{
		var command = Mapper.Map<AddItemCommand>(request);
		var result = await Mediator.Send(command);
		var response = ConvertToResponse(result);
		return RedirectToAction("Index", response);
	}

	[HttpPost]
	public async Task<IActionResult> Update(UpdateItemRequest request)
	{
		var command = Mapper.Map<UpdateItemCommand>(request);
		var result = await Mediator.Send(command);
		if (result.IsSuccess)
			return RedirectToAction("Index");
		else
			return RedirectToAction("Error");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
