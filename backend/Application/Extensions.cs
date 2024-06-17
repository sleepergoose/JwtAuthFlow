using Microsoft.Extensions.DependencyInjection;
using Shared.Commands;
using System.Reflection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Scan and add commands
        // Use Scrutor simply not to add each CommandHandler manually
        var assembly = Assembly.GetCallingAssembly();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        return services;
    }
}
