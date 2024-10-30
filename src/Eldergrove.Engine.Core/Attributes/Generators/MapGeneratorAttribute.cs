using Eldergrove.Engine.Core.Types;

namespace Eldergrove.Engine.Core.Attributes.Generators;

[AttributeUsage(AttributeTargets.Class)]
public class MapGeneratorAttribute : Attribute
{
    public MapGeneratorType Type { get; }

    public MapGeneratorAttribute(MapGeneratorType type)
    {
        Type = type;
    }
}
