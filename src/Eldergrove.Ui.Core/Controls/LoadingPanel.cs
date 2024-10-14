using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using GoRogue.Messaging;
using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Controls;

public class LoadingPanel : ControlsConsole, ISubscriber<LoggerEvent>
{
    private readonly ProgressBar _progressBar;
    private readonly Label _label;


    public LoadingPanel(int width, int height) : base(width, height)
    {
        _progressBar = new ProgressBar(Width, 1, HorizontalAlignment.Left);
        _label = new Label("Loading");
        _label.Position = new Point(0, 1);
        _label.Resize(Width, 1);

        Controls.Add(_progressBar);
        Controls.Add(_label);
        EldergroveState.Engine.GetService<IMessageBusService>().Subscribe(this);
    }


    protected override void Dispose(bool disposing)
    {
        EldergroveState.Engine.GetService<IMessageBusService>().Unsubscribe(this);

        base.Dispose(disposing);
    }

    public void Handle(LoggerEvent message)
    {
        _label.DisplayText = message.Message;
        if (_progressBar.Progress <= 1)
        {
            _progressBar.Progress += 0.1f;
        }
        else
        {
            _progressBar.Progress = 0;
        }

        IsDirty = true;
    }
}
