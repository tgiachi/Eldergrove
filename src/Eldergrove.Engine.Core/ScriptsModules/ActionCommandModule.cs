using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class ActionCommandModule
{
    private readonly IActionCommandService _actionCommandService;

    public ActionCommandModule(IActionCommandService actionCommandService)
    {
        _actionCommandService = actionCommandService;
    }

    [ScriptFunction("action_register_cmd")]
    public void RegisterCommand(string command, Action<ActionContext> action)
    {
        _actionCommandService.RegisterCommand(command, action);
    }

    [ScriptFunction("action_unregister_cmd")]
    public void UnregisterCommand(string command)
    {
        _actionCommandService.UnregisterCommand(command);
    }

    [ScriptFunction("action_execute_cmd")]
    public void ExecuteCommand(string command)
    {
        _actionCommandService.ExecuteCommand(command);
    }
}
