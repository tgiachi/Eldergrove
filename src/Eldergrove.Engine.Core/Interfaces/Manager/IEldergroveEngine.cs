using Eldergrove.Engine.Core.Interfaces.Services;

namespace Eldergrove.Engine.Core.Interfaces.Manager;

public interface IEldergroveEngine
{
    Task InitializeAsync();

    Task StartAsync();

    TService GetService<TService>() where TService : class;

    INpcService NpcService { get; }

    INpcService GetNpcService();


    void AddOnEngineStart(Action action);
}
