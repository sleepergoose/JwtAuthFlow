using Infrastructure.EFCore.Contexts;
using Infrastructure.EFCore.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore;

internal static class Extensions
{
    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<PostgresOptions>("PostgreSQL");

        services.AddDbContext<ReadDbContext>(opt => 
            opt.UseNpgsql(options.ConnectionString));

        services.AddDbContext<WriteDbContext>(opt => 
            opt.UseNpgsql(options.ConnectionString));

        return services;
    }
}
