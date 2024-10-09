using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class EngineEventModule
{
    private readonly ILogger _logger;

    public EngineEventModule(ILogger<EngineEventModule> logger)
    {
        _logger = logger;
    }


    [ScriptFunction("task")]
    public void ExecuteTask(Action action)
    {
        Task.Run(
            () =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error executing task {Task}", action.Method.Name);
                }
            }
        );
    }
}
