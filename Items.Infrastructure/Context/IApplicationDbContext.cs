using Microsoft.EntityFrameworkCore;
using Items.Domain.Entities;

namespace Items.Infrastructure.Context;

public interface IApplicationDbContext
{
    DbSet<Item> Items { get; }
	DbSet<Category> Categories { get; }

	Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}