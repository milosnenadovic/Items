using Items.Infrastructure.Context;
using Items.Infrastructure.Interceptors;
using Items.Infrastructure.Interfaces;
using Items.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Items.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Registering interceptors
		services.AddScoped<AuditableEntitySaveChangesInterceptor>();

		// Adding DB context
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			string connectionString = configuration.GetConnectionString("DefaultConnection");
			options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Items.Infrastructure"));
		});

		services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
		services.AddScoped<ApplicationDBContextInitialiser>();

		// Registering services
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IItemService, ItemService>();

		return services;
	}
}