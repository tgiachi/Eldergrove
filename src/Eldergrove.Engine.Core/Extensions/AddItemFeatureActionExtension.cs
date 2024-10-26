using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddItemFeatureActionExtension
{
    public static IServiceCollection AddItemFeatureAction<T>(
        this IServiceCollection services, string itemFeatureName, string actionName
    )
        where T : class, IItemFeature =>
        services.AddItemFeatureAction(itemFeatureName, actionName, typeof(T));


    public static IServiceCollection AddItemFeatureAction(
        this IServiceCollection services, string itemFeatureName, string actionName, Type type
    )

    {
        services.AddTransient(type);

        services.AddToRegisterTypedList(new ItemFeatureActionData(itemFeatureName, actionName, type));
        return services;
    }
}
