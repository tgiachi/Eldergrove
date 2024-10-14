using Eldergrove.Engine.Core.State;
using Eldergrove.Ui.Core.Data.Internal;
using Eldergrove.Ui.Core.Interfaces;
using SadConsole;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace Eldergrove.Ui.Core.Controls;

public class BarControl : Console
{
    private readonly string _barId;

    private readonly IBarService _barService;


    public BarControl(int width, int height, string barId) : base(width, height)
    {
        _barId = barId;

        _barService = EldergroveState.Engine.GetService<IBarService>();

        _barService.BuildBar(barId, DrawBar);
    }

    public void DrawBar(List<BarColoredObject> bars)
    {
        this.Clear();

        var index = 0;

        foreach (var bar in bars)
        {
            this.Print(index, 0, bar.RenderedText, bar.Foreground, bar.Background);
            index += bar.RenderedText.Length;
        }

        if (index < Width)
        {
            this.Print(index, 0, new string(' ', Width - index), Color.Black, Color.Black);
        }
    }
}
