using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using GoRogue.Messaging;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class SchedulerService : ISchedulerService, ISubscriber<AddActionToSchedulerEvent>
{
    private readonly ILogger _logger;

    private readonly Queue<ISchedulerAction> _actions = new();

    private readonly IMessageBusService _messageBusService;


    public SchedulerService(
        ILogger<SchedulerService> logger, IMessageBusService messageBusService
    )
    {
        _logger = logger;
        _messageBusService = messageBusService;

        _messageBusService.Subscribe(this);
    }

    public int Turn { get; private set; }

    public void AddAction(ISchedulerAction action)
    {
        _actions.Enqueue(action);
    }

    public async Task TickAsync()
    {
        _logger.LogDebug("Tick {Turn}", Turn);
        Turn++;

        while (_actions.Count > 0)
        {
            var action = _actions.Dequeue();
            var result = await action.ExecuteAsync();
            _logger.LogDebug("Action {Action} executed with result {Result}", action.GetType().Name, result);

            if (result.Result == ActionResultType.Success)
            {
                _logger.LogDebug("Action {Action} succeeded", action.GetType().Name);
            }

            if (result.Result == ActionResultType.Failure)
            {
                _logger.LogDebug("Action {Action} failed", action.GetType().Name);
            }

            if (result.Result == ActionResultType.Repeat)
            {
                _logger.LogDebug("Action {Action} repeated", action.GetType().Name);
                _actions.Enqueue(action);
            }

            if (result.Result == ActionResultType.Wait)
            {
                _logger.LogDebug("Action {Action} waiting", action.GetType().Name);

                _actions.Enqueue(action);
            }
        }


        _messageBusService.Publish(new TickEvent(Turn));
    }

    public void Handle(AddActionToSchedulerEvent message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        _logger.LogDebug("Received action message {Message}", message.GetType());
        AddAction(message.Action);
    }
}
