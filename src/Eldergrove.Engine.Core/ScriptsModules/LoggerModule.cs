using Eldergrove.Engine.Core.Attributes.Scripts;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.ScriptsModules;

[ScriptModule]
public class LoggerModule
{
    private readonly ILogger _logger;

    private readonly IMessageBusService _messageBusService;

    public LoggerModule(ILogger<LoggerModule> logger, IMessageBusService messageBusService)
    {
        _logger = logger;
        _messageBusService = messageBusService;
    }

    [ScriptFunction("log_info")]
    public void LogInfo(object message, params string[] args)
    {
        if (message is string msg)
        {
            _logger.LogInformation(msg, args);
        }
        else
        {
            _logger.LogInformation(message.ToString(), args);
        }
    }


    [ScriptFunction("log_debug")]
    public void LogDebug(object message, params string[] args)
    {
        if (message is string msg)
        {
            _logger.LogDebug(msg, args);
        }
        else
        {
            _logger.LogDebug(message.ToString(), args);
        }
    }

    [ScriptFunction("log_warn")]
    public void LogWarning(object message, params string[] args)
    {
        if (message is string msg)
        {
            _logger.LogWarning(msg, args);
        }
        else
        {
            _logger.LogWarning(message.ToString(), args);
        }
    }

    [ScriptFunction("log_error")]
    public void LogError(object message, params string[] args)
    {
        if (message is string msg)
        {
            _logger.LogError(msg, args);
        }
        else
        {
            _logger.LogError(message.ToString(), args);
        }
    }

    [ScriptFunction("message_log", "Sends a message log event")]
    public void SendMessageLog(string message, string background, string foreground)
    {
        _messageBusService.Publish(new MessageLogEvent(message, background, foreground));
    }
}
