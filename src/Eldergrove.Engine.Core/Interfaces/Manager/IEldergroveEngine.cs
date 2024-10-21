using Eldergrove.Engine.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Interfaces.Manager;

public interface IEldergroveEngine
{
    Task InitializeAsync();

    Task StartAsync();

    TService GetService<TService>() where TService : class;

    ILogger<TSource> GetLogger<TSource>();

    INpcService NpcService { get; }

    INpcService GetNpcService();

    void SendEngineReady();

    void AddOnEngineStart(Action action);
}
