using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.Messaging;
using Serilog;

namespace Eldergrove.Engine.Core.Services;

public class MessageBusService : IMessageBusService
{
    private readonly MessageBus _messageBus = new();

    private readonly ILogger _logger = Log.ForContext<MessageBusService>();


    public void Publish<T>(T message) where T : class
    {
        _logger.Debug("Publishing message {Message}", message.GetType());
        _messageBus.Send(message);
    }

    public void Subscribe<T>(ISubscriber<T> action) where T : class
    {
        _logger.Debug("Subscribing to message {Message}", typeof(T));
        _messageBus.RegisterSubscriber<T>(action);
    }

}
