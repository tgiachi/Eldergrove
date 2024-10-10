namespace Eldergrove.Engine.Core.Attributes.Events;

[AttributeUsage(AttributeTargets.Class)]
public class EventToDispatcherAttribute(string eventName) : Attribute
{
    public string EventName { get; } = eventName;
}
