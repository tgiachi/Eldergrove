namespace Eldergrove.Engine.Core.Interfaces.Manager;

public interface IEldergroveEngine
{
    Task InitializeAsync();

    Task StartAsync();

    TService GetService<TService>() where TService : class;
}
