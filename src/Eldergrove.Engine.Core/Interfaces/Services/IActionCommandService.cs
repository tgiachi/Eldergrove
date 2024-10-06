using Eldergrove.Engine.Core.Interfaces.Services.Base;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IActionCommandService : IEldergroveService
{

    void RegisterCommand(string command, Action action);

    void UnregisterCommand(string command);

    void ExecuteCommand(string command);

}
