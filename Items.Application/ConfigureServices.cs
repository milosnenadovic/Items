﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Items.Infrastructure;
using Items.Application.Mappers;
using FluentValidation;
using MapsterMapper;

namespace Items.Application;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
	{
		// Adding FluentValidation validators
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		// Adding MediatR
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

		// Adding Items.Infrastructure services
		services.AddInfrastructureServices(configuration);

		// Add Mapster configuration
		services.AddScoped<IMapper, Mapper>();
		services.RegisterMapsterConfiguration();

		return services;
	}
}