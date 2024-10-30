using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Map;
using Eldergrove.Engine.Core.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddMapGeneratorExtension
{
    public static IServiceCollection RegisterMapGeneratorType<TService>(
        this IServiceCollection services, MapGeneratorType type
    )
        where TService : class, IMapGenerator
    {
        return services.RegisterMapGeneratorType(typeof(TService), type);
    }

    public static IServiceCollection RegisterMapGeneratorType(
        this IServiceCollection services, Type serviceType, MapGeneratorType type
    )
    {
        services.AddScoped(serviceType);
        services.AddToRegisterTypedList(new MapGeneratorData(type, serviceType));
        return services;
    }
}
