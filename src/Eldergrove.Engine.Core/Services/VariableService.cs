using System.Text.RegularExpressions;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.Messaging;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public partial class VariableService : IVariablesService, ISubscriber<TickEvent>
{
    [GeneratedRegex(@"\{([^}]+)\}")] // example "Hello {name}, how are you? -> Hello John, how are you?"
    private static partial Regex TokenRegex();

    private readonly ILogger _logger;

    private readonly Regex _tokenRegex = TokenRegex();

    private readonly Dictionary<string, Func<object>> _variableBuilder = new();

    private readonly Dictionary<string, object> _variables = new();

    public VariableService(ILogger<VariableService> logger, IMessageBusService messageBusService)
    {
        _logger = logger;
        messageBusService.Subscribe(this);
    }

    public void AddVariableBuilder(string variableName, Func<object> builder)
    {
        _logger.LogDebug("Adding variable builder for {variableName}", variableName);
        _variableBuilder[variableName] = builder;
    }

    public void AddVariable(string variableName, object value)
    {
        _logger.LogDebug("Adding variable {variableName} with value {value}", variableName, value);
        _variables[variableName] = value;
    }

    public string TranslateText(string text)
    {
        MatchCollection matches = _tokenRegex.Matches(text);

        foreach (Match match in matches)
        {
            string token = match.Groups[1].Value;
            if (_variables.TryGetValue(token, out var variable))
            {
                text = text.Replace(match.Value, variable.ToString());
            }
            else if (_variableBuilder.TryGetValue(token, out var value))
            {
                text = text.Replace(match.Value, value().ToString());
            }
        }


        return text;
    }

    public void Handle(TickEvent message)
    {
        foreach (var builder in _variableBuilder)
        {
            _variables[builder.Key] = builder.Value();
        }
    }
}
