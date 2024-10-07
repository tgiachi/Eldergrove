using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Services;

public class PropService : IPropService
{
    private readonly ILogger _logger;

    private readonly List<PropObject> _props = new();

    public PropService(ILogger<PropService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync() => Task.CompletedTask;

    public Task StopAsync() => Task.CompletedTask;

    public void AddProp(PropObject prop)
    {
        _logger.LogInformation("Adding prop {PropId}", prop.Id);
        _props.Add(prop);
    }

    public PropObject GetProp(string id) => _props.FirstOrDefault(p => p.Id == id);
}
