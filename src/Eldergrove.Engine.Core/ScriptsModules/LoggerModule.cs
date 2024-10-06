using Eldergrove.Engine.Core.Attributes.Scripts;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class LoggerModule
{
    private readonly ILogger _logger;

    public LoggerModule(ILogger<LoggerModule> logger)
    {
        _logger = logger;
    }

    [ScriptFunction("log_info")]
    public void LogInfo(string message, params string[] args)
    {
        _logger.LogInformation(message, args);
    }
}
