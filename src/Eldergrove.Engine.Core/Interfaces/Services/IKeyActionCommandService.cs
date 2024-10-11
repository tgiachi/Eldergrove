using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IKeyActionCommandService : IEldergroveService
{

    void RegisterCommand(string command, Action<ActionContext> action);
    void RegisterKeybinding(string key, string command);

    void UnregisterCommand(string command);

    void ExecuteCommand(string command);

}
