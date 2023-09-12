using Items.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Items.Infrastructure.Context;

public class ApplicationDBContextInitialiser
{
	private readonly ILogger<ApplicationDBContextInitialiser> _logger;
	private readonly ApplicationDbContext _context;

	public ApplicationDBContextInitialiser(ILogger<ApplicationDBContextInitialiser> logger, ApplicationDbContext context)
	{
		_logger = logger;
		_context = context;
	}

	public async Task InitialiseAsync()
	{
		try
		{
			await _context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database.");
			throw;
		}
	}

	public async Task SeedAsync()
	{
		try
		{
			await TrySeedAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while seeding the database.");
			throw;
		}
	}

	public async Task TrySeedAsync()
	{
		await SeedCategories();
	}

	private async Task SeedCategories()
	{
		SeedCategory(new Category() { Name = "Food", Active = true });
		SeedCategory(new Category() { Name = "Drink", Active = true });

		void SeedCategory(Category category)
		{
			var dbCategory = _context.Categories.FirstOrDefault(x => x.Name == category.Name);

			if (dbCategory is null)
				_context.Categories.Add(category);
			else
				dbCategory.Active = category.Active;
		}

		await _context.SaveChangesAsync();
	}
}