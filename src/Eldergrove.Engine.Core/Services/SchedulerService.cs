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

    private readonly List<IActionableEntity> _actionableEntities = new();


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

    private void PrepareActions()
    {
        foreach (var entity in _actionableEntities)
        {
            var actions = entity.TakeTurn();
            foreach (var action in actions)
            {
                AddAction(action);
            }
        }
    }

    public async Task TickAsync()
    {
        PrepareActions();

        _logger.LogDebug("Tick {Turn} total action to execute: {ActionCount}", Turn, _actions.Count);
        Turn++;

        var waitActions = new List<ISchedulerAction>();

        while (_actions.Count > 0)
        {
            var action = _actions.Dequeue();
            var result = await action.ExecuteAsync();
            _logger.LogDebug("Action {Action} executed with result {Result}", action.GetType().Name, result.Result);

            if (result.Result == ActionResultType.Success)
            {
                _logger.LogDebug("Action {Action} succeeded", action.GetType().Name);
            }

            if (result.Result == ActionResultType.Failure)
            {
                _logger.LogTrace("Action {Action} failed", action.GetType().Name);

                _actions.Clear();
                break;
            }

            if (result.Result == ActionResultType.Repeat)
            {
                _logger.LogTrace("Action {Action} repeated", action.GetType().Name);
                _actions.Enqueue(action);
            }

            if (result.Result == ActionResultType.Wait)
            {
                _logger.LogTrace("Action {Action} waiting", action.GetType().Name);

                waitActions.Add(action);
            }

            await Task.Delay(100);
        }

        foreach (var action in waitActions)
        {
            _actions.Enqueue(action);
        }


        _messageBusService.Publish(new TickEvent(Turn));
    }

    public void AddActionableEntity(IActionableEntity entity)
    {
        _actionableEntities.Add(entity);
    }

    public void RemoveActionableEntity(IActionableEntity entity)
    {
        _actionableEntities.Remove(entity);
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
