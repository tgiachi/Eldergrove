using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddDefaultJsonSettingsExtension
{
    public static IServiceCollection AddDefaultJsonSettings(this IServiceCollection services)
    {
        services.AddSingleton(JsonUtils.GetDefaultJsonSettings());

        return services;
    }
}
