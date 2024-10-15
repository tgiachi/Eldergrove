using System.Diagnostics;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using GoRogue.Messaging;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class SchedulerService : ISchedulerService, ISubscriber<AddActionToSchedulerEvent>, ISubscriber<MapGeneratedEvent>
{
    private readonly ILogger _logger;

    private readonly Queue<ISchedulerAction> _actions = new();

    private readonly IMessageBusService _messageBusService;

    private readonly List<IActionableEntity> _actionableEntities = new();


    private Task _tickTask;

    private bool _isTurnBased;

    private readonly IScriptEngineService _scriptEngineService;

    //private readonly Task _tickTask;

    public SchedulerService(
        ILogger<SchedulerService> logger, IMessageBusService messageBusService, IScriptEngineService scriptEngineService
    )
    {
        _logger = logger;
        _messageBusService = messageBusService;
        _scriptEngineService = scriptEngineService;

        _messageBusService.Subscribe<AddActionToSchedulerEvent>(this);
        _messageBusService.Subscribe<MapGeneratedEvent>(this);

        _messageBusService.Publish(new AddVariableBuilderEvent("ticks", () => Turn));

        // _tickTask = Task.Run(async () =>
        // {
        //     while (true)
        //     {
        //         await Task.Delay(400);
        //         await TickAsync();
        //     }
        // });
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

    private async Task ProcessQueue()
    {
        var stopWatch = Stopwatch.StartNew();

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


                if (result.AlternateAction != null)
                {
                    _logger.LogTrace(
                        "Action {Action} has alternate action {AlternateAction}",
                        action.GetType().Name,
                        result.AlternateAction.GetType().Name
                    );
                    _actions.Enqueue(result.AlternateAction);
                }
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

            //  await Task.Delay(100);
        }

        foreach (var action in waitActions)
        {
            _actions.Enqueue(action);
        }


        stopWatch.Stop();
        _logger.LogDebug("Tick {Turn} took {Elapsed}ms", Turn, stopWatch.ElapsedMilliseconds);

        _messageBusService.Publish(new TickEvent(Turn));
    }

    public async Task TickAsync()
    {
        if (_isTurnBased)
        {
            await ProcessQueue();
        }
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

    public void Handle(MapGeneratedEvent message)
    {
        var gameConfig = _scriptEngineService.GetContextVariable<GameConfig>("game_config");

        _isTurnBased = gameConfig.Scheduler.IsTurnBased;

        if (!gameConfig.Scheduler.IsTurnBased)
        {
            _tickTask = Task.Run(
                async () =>
                {
                    while (true)
                    {
                        await Task.Delay(gameConfig.Scheduler.TickDelay);
                        await ProcessQueue();
                    }
                }
            );
        }
    }
}
