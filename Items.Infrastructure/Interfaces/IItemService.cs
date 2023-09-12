using Items.Domain.Entities;

namespace Items.Infrastructure.Interfaces;

public interface IItemService
{
	Task<List<Item>> GetItems(string? filterName, string? filterDescription, int? category, decimal? minPrice, decimal? maxPrice, DateTime? createdFrom, DateTime? createdTo, bool? active);
	Task<Item?> GetById(int id);
	Task<bool> Existing(string name, int category);
	Task<bool> Add(string name, string description, int categoryId, string producer, string supplier, decimal price);
	Task<bool> Update(int id, string name, string description, int categoryId, string producer, string supplier, decimal price, bool active);
}
