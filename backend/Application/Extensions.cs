using Application.Commands;
using Application.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Shared.Commands;
using Shared.Queries;
using System.Reflection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Scan and add commands
        // Use Scrutor simply not to add each CommandHandler manually
        var assembly = Assembly.GetCallingAssembly();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<ICommandHandler<CreateUserCommand>, CreateUserHandler>();

        return services;
    }
}
