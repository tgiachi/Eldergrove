using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Manager;
using Eldergrove.Engine.Core.State;
using Eldergrove.Ui.Core.Surfaces;
using SadConsole;
using SadConsole.Configuration;

Settings.WindowTitle = "SadConsole Examples";


var engine = new EldergroveEngine(new EldergroveOptions() { RootDirectory = Path.Join(Path.GetTempPath(), "Eldergrove") });

EldergroveState.Engine = engine;

await engine.StartAsync();

// Configure how SadConsole starts up
var startup = new Builder()
        .SetScreenSize(90 * 2, 30 * 2)
        .UseDefaultConsole()
        .OnStart(Game_Started)
        .ConfigureFonts(
            (f, g) => { f.AddExtraFonts("Fonts/Cheepicus12.font"); }
        )
        .IsStartingScreenFocused(true)
        .ConfigureFonts(true)
    ;

Game.Create(startup);
Game.Instance.Run();
Game.Instance.Dispose();

async void Game_Started(object? sender, GameHost host)
{
    Game.Instance.StartingConsole.Font = host.Fonts.RandomElement().Value;

    Game.Instance.StartingConsole.Clear();

    Game.Instance.StartingConsole.Children.Add(new LoggerPanel(host.ScreenCellsX, host.ScreenCellsY));

    await Task.Delay(1000);

    await engine.InitializeAsync();
}
