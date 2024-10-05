using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Scripts;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class RegisterScriptClassExtension
{
    public static IServiceCollection RegisterScriptModule(this IServiceCollection services, Type classType)
    {
        var attribute = classType.GetCustomAttribute<ScriptModuleAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"The class {classType.Name} is not a script module.");
        }

        services.AddSingleton(classType);

        services.AddToRegisterTypedList(new ScriptClassData(classType));


        return services;
    }

    public static IServiceCollection RegisterScriptModule<TClass>(this IServiceCollection services) =>
        services.RegisterScriptModule(typeof(TClass));
}
