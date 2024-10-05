using System.Reflection;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddDataLoaderTypeExtension
{
    public static IServiceCollection AddDataLoaderType<TDataTypeClass>(this IServiceCollection services)
        where TDataTypeClass : class, IJsonDataObject
    {
        var attribute = typeof(TDataTypeClass).GetCustomAttribute<DataLoaderTypeAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"DataLoaderTypeAttribute not found on {typeof(TDataTypeClass).Name}");
        }

        var name = attribute.Name;

        services.AddToRegisterTypedList(new DataLoaderType(name, typeof(TDataTypeClass)));

        return services;
    }
}
