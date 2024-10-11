using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Keyboard;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;


[AutostartService]
public class KeyKeyActionCommandService : IKeyActionCommandService
{
    private readonly ILogger _logger;

    private readonly Dictionary<string, Action<ActionContext>> _commands = new();

    private readonly Dictionary<KeybindingData, string> _keybindings = new();

    private readonly List<KeyActionData> _keyActions;

    private readonly IServiceProvider _serviceProvider;

    private readonly IEldergroveEngine _engine;


    public KeyKeyActionCommandService(
        ILogger<KeyKeyActionCommandService> logger, IEldergroveEngine engine, List<KeyActionData> keyActions,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _engine = engine;
        _keyActions = keyActions;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync()
    {
        foreach (var keyActionData in _keyActions)
        {
            var keyAction = (IKeybindingAction)_serviceProvider.GetService(keyActionData.Type);

            if (keyAction == null)
            {
                _logger.LogError("Type {Type} must implement IKeybindingAction", keyActionData.Type);

                throw new InvalidOperationException("Type must implement IKeybindingAction");
            }

            RegisterCommand(keyActionData.Action, context => keyAction.Execute(context));
        }

        return Task.CompletedTask;
    }

    public Task StopAsync() => Task.CompletedTask;

    public void RegisterCommand(string command, Action<ActionContext> action)
    {
        _logger.LogDebug("Command '{Command}' registered.", command);
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
