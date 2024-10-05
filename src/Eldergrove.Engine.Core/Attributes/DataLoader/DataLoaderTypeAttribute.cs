namespace Eldergrove.Engine.Core.Attributes.DataLoader;

[AttributeUsage(AttributeTargets.Class)]
public class DataLoaderTypeAttribute : Attribute
{
    public string Name { get; }

    public DataLoaderTypeAttribute(string name)
    {
        Name = name;
    }
}
