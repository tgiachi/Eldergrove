using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddEldergroveServiceExtension
{
    public static IServiceCollection AddEldergroveService<TInterface, TService>(this IServiceCollection services) where TInterface : class where TService : class, TInterface
    {
        services.AddSingleton<TInterface, TService>();

        var attribute = typeof(TService).GetCustomAttribute<AutostartServiceAttribute>();

        if (attribute != null)
        {
            services.AddToRegisterTypedList(new AutostartServiceData(attribute.Order, typeof(TInterface)));
        }

        return services;
    }

}
