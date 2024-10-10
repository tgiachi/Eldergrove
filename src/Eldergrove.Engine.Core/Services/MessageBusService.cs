using System.Reflection;
using Eldergrove.Engine.Core.Attributes.Events;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.Messaging;
using Serilog;

namespace Eldergrove.Engine.Core.Services;

[AutostartService(0)]
public class MessageBusService : IMessageBusService
{
    private readonly MessageBus _messageBus = new();

    private readonly ILogger _logger = Log.ForContext<MessageBusService>();

    private readonly IEventDispatcherService _dispatcherService;

    public MessageBusService(IEventDispatcherService dispatcherService)
    {
        _dispatcherService = dispatcherService;
    }


    public void Publish<T>(T message) where T : class
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        //_logger.Debug("Publishing message {Message}", message.GetType());
        _messageBus.Send(message);

        DispatchMessage(message);
    }

    private void DispatchMessage(object message)
    {
        var attribute = message.GetType().GetCustomAttribute<EventToDispatcherAttribute>();

        if (attribute == null)
        {
            return;
        }

        _dispatcherService.DispatchEvent(attribute.EventName, message);
    }

    public void Unsubscribe<T>(ISubscriber<T> action) where T : class
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        //_logger.Debug("Unsubscribing from message {Message}", typeof(T));
        _messageBus.UnregisterSubscriber<T>(action);
    }

    public void Subscribe<T>(ISubscriber<T> action) where T : class
    {
        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        //_logger.Debug("Subscribing to message {Message}", typeof(T));
        _messageBus.RegisterSubscriber<T>(action);
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;
}
