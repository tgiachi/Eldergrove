using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddKeybindingActionExtension
{
    public static IServiceCollection AddKeybindingAction<TKeyAction>(this IServiceCollection services)
        where TKeyAction : class, IKeybindingAction
    {
        services.AddSingleton<TKeyAction>();

        var attribute = typeof(TKeyAction).GetCustomAttribute<KeybindingActionAttribute>();

        if (attribute is not null)
        {
            services.AddToRegisterTypedList(new KeyActionData(attribute.Key, typeof(TKeyAction)));
        }


        return services;
    }
}
