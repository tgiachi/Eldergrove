namespace Eldergrove.Engine.Core.Attributes.Services;

[AttributeUsage(AttributeTargets.Class)]
public class AutostartServiceAttribute : Attribute
{
    public int Order { get; }

    public AutostartServiceAttribute(int order)
    {
        Order = order;
    }
}
