namespace Eldergrove.Engine.Core.Attributes.Actions;

[AttributeUsage(AttributeTargets.Class)]
public class SchedulerActionAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
