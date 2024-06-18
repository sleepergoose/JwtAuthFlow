using Microsoft.Extensions.Configuration;

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
}
