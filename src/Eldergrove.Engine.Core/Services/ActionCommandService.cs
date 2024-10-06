using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class ActionCommandService : IActionCommandService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, Action> _commands = new();

    public ActionCommandService(ILogger<ActionCommandService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public void RegisterCommand(string command, Action action)
    {
        _commands.Add(command, action);
    }

    public void UnregisterCommand(string command)
    {
        _commands.Remove(command);
    }

    public void ExecuteCommand(string command)
    {
        if (_commands.TryGetValue(command, out var action))
        {
            action();
        }
        else
        {
            _logger.LogWarning("Command '{Command}' not found.", command);
        }
    }
}
