﻿using Items.Application.Items.Queries.GetItems;
using Items.Shared.Contracts.Items;
using Items.Server.Controllers.Base;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Items.Controllers;

public class ItemsController : BaseController
{
	public ItemsController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

	[HttpGet]
	public async Task<IActionResult> Items([FromQuery] GetItemsRequest request)
	{
		var query = Mapper.Map<GetItemsQuery>(request);
		var result = await Mediator.Send(query);
		var response = ConvertToResponse(result);
		return View(response);
	}
}