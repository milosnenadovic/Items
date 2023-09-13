using Items.Domain.Entities;

namespace Items.Infrastructure.Interfaces;

public interface ICategoryService
{
	Task<List<Category>> GetCategories(string? filterName, DateTime? createdFrom, DateTime? createdTo, bool? active);
	Task<Category?> GetById(int id);
	Task<bool> Existing(int id);
	Task<bool> Existing(string name);
	Task<bool> Add(string name);
	Task<bool> Update(int id, string name, bool active);
}
