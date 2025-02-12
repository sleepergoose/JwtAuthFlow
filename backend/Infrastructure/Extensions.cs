﻿using Infrastructure.EFCore;
using Infrastructure.EFCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgreSQL(configuration);
        services.AddHostedService<MigrationService>();

        return services;
    }
}
