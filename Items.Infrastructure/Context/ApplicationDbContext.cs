using Items.Domain.Entities;
using Items.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Items.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	public class OptionsBuild
	{
		public OptionsBuild()
		{
			configuration = new ApplicationConfiguration();
			optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(configuration.DbConnectionString);
			databaseOptions = optionsBuilder.Options;
		}

		public DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder { get; set; }
		public DbContextOptions<ApplicationDbContext> databaseOptions { get; set; }
		private ApplicationConfiguration configuration { get; set; }
	}

	private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options) 
		=> _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

	public DbSet<Item> Items { get; set; }
	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(modelBuilder);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		configurationBuilder.Properties<string>(x => x.HaveMaxLength(96));
		configurationBuilder.Properties<Enum>(x => x.HaveMaxLength(24));
		configurationBuilder.Properties<decimal>().HavePrecision(16, 4);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await base.SaveChangesAsync(cancellationToken);
	}

	public async Task<int> SaveChangesAsync()
	{
		return await SaveChangesAsync(CancellationToken.None);
	}
}
