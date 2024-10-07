using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using GoRogue.Messaging;
using SadConsole;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Surfaces;

public class LoggerPanel : Console, ISubscriber<LoggerEvent>
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    private readonly List<LoggerEvent> _events = new();


    public LoggerPanel(int width, int height) : base(width, height)
    {
        EldergroveState.Engine.GetService<IMessageBusService>().Subscribe(this);
    }

    protected override void Dispose(bool disposing)
    {
        EldergroveState.Engine.GetService<IMessageBusService>().Unsubscribe(this);
        base.Dispose(disposing);
    }

    public void Handle(LoggerEvent message)
    {
        _lock.Wait();
        _events.Add(message);
        _lock.Release();
    }

    private void PrintMessages()
    {
        _lock.Wait();
        for (var i = 0; i < _events.Count; i++)
        {
            this.Print(0, i, _events[i].Message);
        }
        _lock.Release();
    }
}
