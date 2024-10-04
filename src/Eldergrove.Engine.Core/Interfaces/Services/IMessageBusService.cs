using GoRogue.Messaging;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IMessageBusService
{
    void Subscribe<T>(ISubscriber<T> action) where T : class;

    public void Publish<T>(T message) where T : class;
}
