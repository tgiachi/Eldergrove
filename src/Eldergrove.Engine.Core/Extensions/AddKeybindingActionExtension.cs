using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddKeybindingActionExtension
{
    public static IServiceCollection AddKeybindingAction<TKeyAction>(this IServiceCollection services)
        where TKeyAction : class, IKeybindingAction =>
        services.AddKeybindingAction(typeof(TKeyAction));

    public static IServiceCollection AddKeybindingAction(this IServiceCollection services, Type keyActionType)
    {
        services.AddSingleton(keyActionType);

        var attribute = keyActionType.GetCustomAttribute<KeybindingActionAttribute>();

        if (attribute is not null)
        {
            services.AddToRegisterTypedList(new KeyActionData(attribute.Key, keyActionType));
        }

        return services;
    }
}
