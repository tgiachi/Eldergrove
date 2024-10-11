using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Keyboard;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class KeyActionCommandService : IActionCommandService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, Action<ActionContext>> _commands = new();

    private readonly Dictionary<KeybindingData, string> _keybindings = new();


    private readonly IEldergroveEngine _engine;


    public KeyActionCommandService(ILogger<KeyActionCommandService> logger, IEldergroveEngine engine)
    {
        _logger = logger;
        _engine = engine;
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public void RegisterCommand(string command, Action<ActionContext> action)
    {
        _commands.Add(command, action);
    }

    public void RegisterKeybinding(string key, string command)
    {
        var keyBinding = KeybindingParser.Parse(key);
        _keybindings.Add(keyBinding, command);

        _logger.LogDebug("Keybinding '{Keybinding}' registered for command '{Command}'", key, command);
    }

    public void UnregisterCommand(string command)
    {
        _commands.Remove(command);
    }

    public void ExecuteCommand(string command)
    {
        if (_commands.TryGetValue(command, out var action))
        {
            var _context = new ActionContext()
            {
                Engine = _engine,
                CommandName = command
            };

            action(_context);
        }
        else
        {
            _logger.LogWarning("Command '{Command}' not found.", command);
        }
    }
}
