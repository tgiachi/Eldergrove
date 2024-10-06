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


    [ScriptFunction("log_debug")]
    public void LogDebug(string message, params string[] args)
    {
        _logger.LogDebug(message, args);
    }

    [ScriptFunction("log_warn")]
    public void LogWarning(string message, params string[] args)
    {
        _logger.LogWarning(message, args);
    }

    [ScriptFunction("log_error")]
    public void LogError(string message, params string[] args)
    {
        _logger.LogError(message, args);
    }
}
