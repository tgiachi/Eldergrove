using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ActionCommandModule
{
    private readonly IKeyActionCommandService _keyActionCommandService;

    public ActionCommandModule(IKeyActionCommandService keyActionCommandService)
    {
        _keyActionCommandService = keyActionCommandService;
    }

    [ScriptFunction("action_register_cmd")]
    public void RegisterCommand(string command, Action<ActionContext> action)
    {
        _keyActionCommandService.RegisterCommand(command, action);
    }

    [ScriptFunction("action_unregister_cmd")]
    public void UnregisterCommand(string command)
    {
        _keyActionCommandService.UnregisterCommand(command);
    }

    [ScriptFunction("action_execute_cmd")]
    public void ExecuteCommand(string command)
    {
        _keyActionCommandService.ExecuteCommand(command);
    }

    [ScriptFunction("register_keybinding")]
    public void RegisterKeybinding(string key, string command)
    {
        _keyActionCommandService.RegisterKeybinding(key, command);
    }
}
