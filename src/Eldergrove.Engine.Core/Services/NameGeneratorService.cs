using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class NameGeneratorService : INameGeneratorService
{
    private readonly Dictionary<string, List<string>> _names = new();

    private readonly ILogger _logger;

    public NameGeneratorService(ILogger<NameGeneratorService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public void AddName(string type, string name)
    {
        _logger.LogTrace("Adding name '{Name}' to type '{Type}'.", name, type);
        if (!_names.TryGetValue(type, out var names))
        {
            names = new List<string>();
            _names.Add(type, names);
        }

        names.Add(name);
    }

    public string GenerateName(string type) => _names.TryGetValue(type, out var names)
        ? names[Random.Shared.Next(0, names.Count)]
        : string.Empty;
}
