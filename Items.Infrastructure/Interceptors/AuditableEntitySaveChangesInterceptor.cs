using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Items.Domain.Common;

namespace Items.Infrastructure.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		UpdateEntities(eventData.Context);

		return base.SavingChanges(eventData, result);
	}

	public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		UpdateEntities(eventData.Context);

		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	public static void UpdateEntities(DbContext? context)
	{
		if (context is null) return;

		foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
		{
			if (entry.State == EntityState.Added)
				entry.Entity.Created = DateTime.Now;

			if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
				entry.Entity.LastModified = DateTime.Now;
		}
	}
}

public static class Extensions
{
	public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
		entry.References.Any(r =>
			r.TargetEntry != null &&
			r.TargetEntry.Metadata.IsOwned() &&
			(r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}