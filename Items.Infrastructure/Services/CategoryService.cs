using Microsoft.EntityFrameworkCore;
using Items.Infrastructure.Interfaces;
using Items.Infrastructure.Context;
using Items.Domain.Entities;

namespace Items.Infrastructure.Service;

public class CategoryService : ICategoryService
{
	#region Setup
	private readonly IApplicationDbContext _dbContext;

	public CategoryService(IApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	#endregion

	#region GetById
	public async Task<Category?> GetById(int id)
	{
		return await _dbContext.Categories
			.FirstOrDefaultAsync(x => x.Id == id);
	}
	#endregion

	#region Existing
	public async Task<bool> Existing(string name)
	{
		if (_dbContext.Categories.Any(x => x.Name == name))
			return await Task.FromResult(true);
		else
			return await Task.FromResult(false);
	}
	#endregion

	#region GetCategories
	public async Task<List<Category>> GetCategories(string? filterName, DateTime? createdFrom, DateTime? createdTo, bool? active)
	{
		var contracts = _dbContext.Categories
			.AsQueryable();

		if (!string.IsNullOrEmpty(filterName))
			contracts = contracts.Where(x => x.Name.Contains(filterName));
		if (createdFrom is not null)
			contracts = contracts.Where(x => x.Created >= createdFrom);
		if (createdTo is not null)
			contracts = contracts.Where(x => x.Created <= createdTo);
		if (active is not null)
			contracts = contracts.Where(x => x.Active == active);

		return await contracts.ToListAsync();
	}
	#endregion

	#region Add
	public async Task<bool> Add(string name)
	{
		Category newCategory = new()
		{
			Name = name
		};

		try
		{
			_dbContext.Categories.Add(newCategory);
			await _dbContext.SaveChangesAsync();
		}
		catch
		{
			return await Task.FromResult(false);
		}

		return await Task.FromResult(true);
	}
	#endregion

	#region Update
	public async Task<bool> Update(int id, string name, bool active)
	{
		try
		{
			var dbCategory = await _dbContext.Categories.SingleAsync(x => x.Id == id);

			dbCategory.Name = name;
			dbCategory.Active = active;

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
