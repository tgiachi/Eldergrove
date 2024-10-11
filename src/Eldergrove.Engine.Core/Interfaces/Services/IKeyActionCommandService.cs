using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Keyboard;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using SadConsole.Input;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IKeyActionCommandService : IEldergroveService
{
    void RegisterCommand(string command, Action<ActionContext> action);
    void RegisterKeybinding(string context, string key, string command);

    void UnregisterCommand(string command);

    void ExecuteCommand(string command);
    bool ExecuteKeybinding(string context, KeybindingData keybindingData);
}
