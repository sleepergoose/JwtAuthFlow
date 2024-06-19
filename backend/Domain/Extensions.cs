using Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<IUserFactory, UserFactory>();

        return services;
    }
}
