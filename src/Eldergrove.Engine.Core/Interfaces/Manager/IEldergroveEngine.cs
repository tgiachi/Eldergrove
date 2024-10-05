namespace Eldergrove.Engine.Core.Interfaces.Manager;

public interface IEldergroveEngine
{
    public Task InitializeAsync();

    public Task StartAsync();
}
