using Items.Application.Categories.Queries.GetActiveCategories;
using Items.Controllers.Base;
using Items.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Items.Controllers;

public class HomeController : BaseController
{
	public HomeController(ISender mediator, IMapper mapper) : base(mediator, mapper) { }

	public async Task<IActionResult> Index()
	{
		var categories = await Mediator.Send(new GetActiveCategoriesQuery());
		ViewData["Categories"] = categories.Result;
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}