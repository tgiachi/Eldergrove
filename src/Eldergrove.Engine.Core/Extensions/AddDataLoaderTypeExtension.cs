using System.Reflection;
using Eldergrove.Engine.Core.Attributes.DataLoader;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Eldergrove.Engine.Core.Extensions;

public static class AddDataLoaderTypeExtension
{
    public static IServiceCollection AddDataLoaderType<TDataTypeClass>(this IServiceCollection services)
        where TDataTypeClass : class, IJsonDataObject =>
        services.AddDataLoaderType(typeof(TDataTypeClass));

    public static IServiceCollection AddDataLoaderType(this IServiceCollection services, Type dataTypeClass)
    {
        var attribute = dataTypeClass.GetCustomAttribute<DataLoaderTypeAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"DataLoaderTypeAttribute not found on {dataTypeClass.Name}");
        }

        var name = attribute.Name;

        services.AddToRegisterTypedList(new DataLoaderType(name, dataTypeClass));

        return services;
    }
}
