namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IEventDispatcherService
{
    void DispatchEvent(string eventName, object? eventData = null);

    void SubscribeToEvent(string eventName, Action<object?> eventHandler);

    void UnsubscribeFromEvent(string eventName, Action<object?> eventHandler);
}
