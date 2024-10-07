using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Manager;
using Eldergrove.Engine.Core.State;
using Eldergrove.Ui.Core.Surfaces;
using SadConsole;
using SadConsole.Configuration;
using SadRogue.Primitives;

Settings.WindowTitle = "SadConsole Examples";


var engine = new EldergroveEngine(new EldergroveOptions() { RootDirectory = Path.Join(Path.GetTempPath(), "Eldergrove") });

EldergroveState.Engine = engine;

await engine.StartAsync();

// Configure how SadConsole starts up
Builder startup = new Builder()
        .SetScreenSize(90 * 2, 30 * 2)
        .UseDefaultConsole()
        .OnStart(Game_Started)
        .IsStartingScreenFocused(true)
        .ConfigureFonts(true)
    ;

// Setup the engine and start the game
Game.Create(startup);
Game.Instance.Run();
Game.Instance.Dispose();

async void Game_Started(object? sender, GameHost host)
{
    Game.Instance.StartingConsole.Clear();

    Game.Instance.StartingConsole.Children.Add(new LoggerPanel(host.ScreenCellsX, host.ScreenCellsY));

    await Task.Delay(1000);

    await engine.InitializeAsync();
}
