using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.Messaging;

namespace Eldergrove.Engine.Core.Services;

public class MessageBusService : IMessageBusService
{
    private readonly MessageBus _messageBus = new();


    public void Publish<T>(T message) where T : class
    {
        _messageBus.Send(message);
    }

    public void Subscribe<T>(ISubscriber<T> action) where T : class
    {
        _messageBus.RegisterSubscriber<T>(action);
    }

}
