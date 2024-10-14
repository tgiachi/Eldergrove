using System.Text;
using System.Text.RegularExpressions;
using Eldergrove.Engine.Core.Attributes.Services;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using GoRogue.Messaging;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;


[AutostartService(-1)]
public partial class VariableService
    : IVariablesService, ISubscriber<TickEvent>, ISubscriber<AddVariableEvent>, ISubscriber<AddVariableBuilderEvent>
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
        messageBusService.Subscribe<TickEvent>(this);
        messageBusService.Subscribe<AddVariableEvent>(this);
        messageBusService.Subscribe<AddVariableBuilderEvent>(this);
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
        var matches = _tokenRegex.Matches(text);
        var result = new StringBuilder(text);

        foreach (Match match in matches)
        {
            string token = match.Groups[1].Value;
            string replacement = null;

            if (_variables.TryGetValue(token, out var variable))
            {
                replacement = variable.ToString();
            }
            else if (_variableBuilder.TryGetValue(token, out var value))
            {
                replacement = value().ToString();
            }

            if (replacement != null)
            {
                result.Replace(match.Value, replacement, match.Index, match.Length);
            }
        }

        return result.ToString();
    }

    public List<string> GetVariables()
    {
        var list = new List<string>();
        list.AddRange(_variables.Keys);
        list.AddRange(_variableBuilder.Keys);

        list = list.OrderByDescending(x => x).ToList();

        return list;
    }

    public void RebuildVariables()
    {
        foreach (var builder in _variableBuilder.AsParallel())
        {
            _variables[builder.Key] = builder.Value();
        }
    }

    public void Handle(TickEvent message)
    {
        RebuildVariables();
    }

    public void Handle(AddVariableEvent message)
    {
        AddVariable(message.VariableName, message.Value);
    }

    public void Handle(AddVariableBuilderEvent message)
    {
        AddVariableBuilder(message.VariableName, message.Builder);
    }
}
