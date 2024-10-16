using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.MessageLog;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Types;
using GoRogue.Messaging;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Controls;

public class MessageLogControl : Console, ISubscriber<MessageLogEvent>
{
    private readonly List<MessageLogData> _messages = new();

    private int _currentIndex = 0;

    protected int visibleLines => Height - 1;

    public MessageLogControl(int width, int height) : base(width, height)
    {
        DrawBorder();
        EldergroveState.Engine.GetService<IMessageBusService>().Subscribe(this);
    }

    public void Draw()
    {
        var messagesToShow = _messages.Skip(_currentIndex).Take(visibleLines).ToList();

        for (int i = 0; i < messagesToShow.Count; i++)
        {
            var message = messagesToShow[i];
            var messageColor = message.Type switch
            {
                MessageLogType.Info     => Color.White,
                MessageLogType.Attack   => Color.Yellow,
                MessageLogType.Critical => Color.Red,
                _                       => Color.White
            };

            this.Print(1, i + 1, message.Message, messageColor);
        }
    }

    private void DrawBorder()
    {
        var horizontal = 196;  // ─ (ASCII 196 o Unicode '\u2500')
        var vertical = 179;    // │ (ASCII 179 o Unicode '\u2502')
        var topLeft = 218;     // ┌ (ASCII 218 o Unicode '\u250C')
        var topRight = 191;    // ┐ (ASCII 191 o Unicode '\u2510')
        var bottomLeft = 192;  // └ (ASCII 192 o Unicode '\u2514')
        var bottomRight = 217; // ┘ (ASCII 217 o Unicode '\u2518')

        this.SetGlyph(0, 0, topLeft);
        this.SetGlyph(Width - 1, 0, topRight);
        this.SetGlyph(0, Height - 1, bottomLeft);
        this.SetGlyph(Width - 1, Height - 1, bottomRight);

        for (int x = 1; x < Width - 1; x++)
        {
            this.SetGlyph(x, 0, horizontal);          // Top border
            this.SetGlyph(x, Height - 1, horizontal); // Bottom border
        }

        for (int y = 1; y < Height - 1; y++)
        {
            this.SetGlyph(0, y, vertical);         // Left border
            this.SetGlyph(Width - 1, y, vertical); // Right border
        }
    }

    public void Handle(MessageLogEvent message)
    {
        _messages.Add(message.Data);

        if (_messages.Count > visibleLines)
        {
            _currentIndex = _messages.Count - visibleLines;
        }

        Draw();
    }
}
