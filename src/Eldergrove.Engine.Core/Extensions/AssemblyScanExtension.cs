using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Attributes.Generators;
using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AssemblyScanExtension
{
    public static IServiceCollection ScanForScriptModules(this IServiceCollection serviceCollection)
    {
        foreach (var type in AssemblyUtils.GetAttribute<ScriptModuleAttribute>())
        {
            serviceCollection.RegisterScriptModule(type);
        }

        return serviceCollection;
    }

    public static IServiceCollection ScanForDataLoaderTypes(this IServiceCollection serviceCollection)
    {
        foreach (var type in AssemblyUtils.GetAttribute<DataLoaderTypeAttribute>())
        {
            serviceCollection.AddDataLoaderType(type);
        }

        return serviceCollection;
    }

    public static IServiceCollection ScanForKeybindingActions(this IServiceCollection serviceCollection)
    {
        foreach (var type in AssemblyUtils.GetAttribute<KeybindingActionAttribute>())
        {
            serviceCollection.AddKeybindingAction(type);
        }

        return serviceCollection;
    }

    public static IServiceCollection ScanForMapGenerators(this IServiceCollection serviceCollection)
    {
        foreach (var type in AssemblyUtils.GetAttribute<MapGeneratorAttribute>())
        {
            var attribute = type.GetCustomAttribute<MapGeneratorAttribute>();
            serviceCollection.RegisterMapGeneratorType(type, attribute.Type);
        }

        return serviceCollection;
    }
}
