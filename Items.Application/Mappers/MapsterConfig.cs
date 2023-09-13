using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Items.Domain.Entities;
using Mapster;

namespace Items.Application.Mappers;

public static class MapsterConfig
{
	public static void RegisterMapsterConfiguration(this IServiceCollection services)
	{
		TypeAdapterConfig<Item, Shared.DTO.ItemDto>
			.NewConfig()
			.Map(dest => dest.Category, src => src.Category == null ? string.Empty : src.Category.Name);

		TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
	}
}
