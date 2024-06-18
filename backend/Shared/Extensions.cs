using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Commands;
using Shared.Queries;

namespace Shared;

public static class Extensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionsName)
        where TOptions : new()
    {
        var options = new TOptions();
        configuration.GetSection(sectionsName).Bind(options);

        return options;
    }

    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        return services;
    }
}
