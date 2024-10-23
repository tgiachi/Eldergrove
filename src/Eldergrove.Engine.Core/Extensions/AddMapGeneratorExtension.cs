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
        services.AddScoped<TService>();
        services.AddToRegisterTypedList(new MapGeneratorData(type, typeof(TService)));
        return services;
    }
}
