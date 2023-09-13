using Microsoft.EntityFrameworkCore;
using Items.Infrastructure.Interfaces;
using Items.Infrastructure.Context;
using Items.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Items.Infrastructure.Service;

public class ItemService : IItemService
{
	#region Setup
	private readonly IApplicationDbContext _dbContext;
	private readonly ILogger<ItemService> _logger;

	public ItemService(IApplicationDbContext dbContext, ILogger<ItemService> logger)
	{
		_dbContext = dbContext;
		_logger = logger;
	}
	#endregion

	#region GetById
	public async Task<Item?> GetById(int id)
	{
		return await _dbContext.Items
			.FirstOrDefaultAsync(x => x.Id == id);
	}
	#endregion

	#region Existing
	public async Task<bool> Existing(string name, int category)
	{
		if (_dbContext.Items.Any(x => x.Name == name && x.CategoryId == category))
			return await Task.FromResult(true);
		else
			return await Task.FromResult(false);
	}
	#endregion

	#region GetItems
	public async Task<List<Item>> GetItems(string? filterName, string? filterDescription, int? category, decimal? minPrice, decimal? maxPrice, DateTime? createdFrom, DateTime? createdTo, bool? active)
	{
		var items = _dbContext.Items
			.Include(x => x.Category)
			.AsQueryable();

		if (!string.IsNullOrEmpty(filterName))
			items = items.Where(x => x.Name.Contains(filterName));
		if (!string.IsNullOrEmpty(filterDescription))
			items = items.Where(x => x.Description.Contains(filterDescription));
		if (category is not null)
			items = items.Where(x => x.CategoryId == category);
		if (minPrice is not null)
			items = items.Where(x => x.Price >= minPrice);
		if (maxPrice is not null)
			items = items.Where(x => x.Price <= maxPrice);
		if (createdFrom is not null)
			items = items.Where(x => x.Created >= createdFrom);
		if (createdTo is not null)
			items = items.Where(x => x.Created <= createdTo);
		if (active is not null)
			items = items.Where(x => x.Active == active);

		return await items.ToListAsync();
	}
	#endregion

	#region Add
	public async Task<bool> Add(string name, string description, int categoryId, string producer, string supplier, decimal price)
	{
		Item newItem = new()
		{
			Name = name,
			Description = description,
			CategoryId = categoryId,
			Producer = producer,
			Supplier = supplier,
			Price = price,
			Active = true
		};

		try
		{
			_dbContext.Items.Add(newItem);
			await _dbContext.SaveChangesAsync();
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex.Message);
			return await Task.FromResult(false);
		}

		return await Task.FromResult(true);
	}
	#endregion

	#region Update
	public async Task<bool> Update(int id, string name, string description, int categoryId, string producer, string supplier, decimal price, bool active)
	{
		try
		{
			var dbItem = await _dbContext.Items.SingleAsync(x => x.Id == id);

			dbItem.Name = name;
			dbItem.Description = description;
			dbItem.CategoryId = categoryId;
			dbItem.Producer = producer;
			dbItem.Supplier = supplier;
			dbItem.Price = price;
			dbItem.Active = active;

			await _dbContext.SaveChangesAsync();
		}
		catch
		{
			return await Task.FromResult(false);
		}

		return await Task.FromResult(true);
	}
	#endregion
}
