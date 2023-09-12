using Items.Models;
using Items.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Items.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			ViewData["Categories"] = new List<CategoryDto> { new() { Name = "Food1", Id = 1 }, new() { Name = "Drink1", Id = 2 } };
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}