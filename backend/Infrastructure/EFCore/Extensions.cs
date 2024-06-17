using Infrastructure.EFCore.Contexts;
using Infrastructure.EFCore.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using Application.Services;
using Infrastructure.EFCore.Services;
using Domain.Repositories;
using Infrastructure.EFCore.Repositories;

namespace Infrastructure.EFCore;

internal static class Extensions
{
    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<PostgresOptions>(Constants.PostgreSqlSectionName)
            ?? throw new OptionsNotFoundException(Constants.PostgreSqlSectionName, typeof(PostgresOptions));

        services.AddDbContext<ReadDbContext>(opt => 
            opt.UseNpgsql(options.ConnectionString));

        services.AddDbContext<WriteDbContext>(opt => 
            opt.UseNpgsql(options.ConnectionString));

        AppContext.SetSwitch(switchName: "Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);

        services.AddScoped<IReadUserService, ReadUserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
