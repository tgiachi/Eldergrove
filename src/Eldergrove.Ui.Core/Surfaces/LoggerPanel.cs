using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using GoRogue.Messaging;
using SadConsole;
using SadRogue.Primitives;
using Serilog.Events;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Surfaces;

public class LoggerPanel : Console, ISubscriber<LoggerEvent>
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    private readonly List<LoggerEvent> _events = new();


    public LoggerPanel(int width, int height) : base(width, height)
    {

        EldergroveState.Engine.GetService<IMessageBusService>().Subscribe(this);

        ParentChanged += (s, e) =>
        {
            if (Parent != null)
            {
                PrintMessages();
            }
        };
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

        if (_events.Count > Height - 2)
        {
            _events.RemoveAt(0);
        }

        _lock.Release();

        PrintMessages();
    }

    private void PrintMessages()
    {
        //  _lock.Wait();
        this.Clear();
        for (var i = 0; i < _events.Count; i++)
        {
            var @event = _events[i];
            this.Print(
                0,
                i,
                $"{DateTime.Now:T} -- {@event.Source ?? ""}  -- {@event.Level} - {@event.Message}",
                GetColor(@event.Level)
            );
        }

        // _lock.Release();
    }

    private static Color GetColor(LogEventLevel level)
    {
        return level switch
        {
            LogEventLevel.Debug       => Color.Gray,
            LogEventLevel.Information => Color.White,
            LogEventLevel.Warning     => Color.Yellow,
            LogEventLevel.Error       => Color.Red,
            _                         => Color.White
        };
    }
}
