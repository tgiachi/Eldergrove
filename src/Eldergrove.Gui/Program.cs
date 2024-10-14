using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Manager;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.State;
using Eldergrove.Ui.Core.Controls;
using Eldergrove.Ui.Core.Screens;
using SadConsole;
using SadConsole.Configuration;

Settings.WindowTitle = "SadConsole Examples";


var rootDirectory = Environment.GetEnvironmentVariable("ELDERGROVE_ROOT_DIRECTORY") ??
                    Path.Join(Directory.GetCurrentDirectory(), "Eldergrove");

var engine = new EldergroveEngine(new EldergroveOptions() { RootDirectory = rootDirectory });

EldergroveState.Engine = engine;

await engine.StartAsync();

// Configure how SadConsole starts up
var startup = new Builder()
        .SetScreenSize(90 * 2, 30 * 2)
        .UseDefaultConsole()
        .OnStart(Game_Started)
        .ConfigureFonts(
            (f, g) =>
            {
                f.AddExtraFonts("Fonts/Cheepicus12.font");
                f.AddExtraFonts("Fonts/C64.font");
            }
        )
        .IsStartingScreenFocused(true)
        .ConfigureFonts(true)
    ;

Game.Create(startup);
Game.Instance.Run();
Game.Instance.Dispose();

async void Game_Started(object? sender, GameHost host)
{
    //Game.Instance.StartingConsole.Font = host.Fonts["C64"];


    engine.GetService<IEventDispatcherService>()
        .SubscribeToEvent(
            "map_generated",
            (e) =>
            {
                Game.Instance.StartingConsole.Clear();

                Game.Instance.StartingConsole.Children.Clear();

                Game.Instance.StartingConsole.Children.Add(
                    new GameObject(EldergroveState.Engine, host.ScreenCellsX, host.ScreenCellsY)
                );
            }
        );

    Game.Instance.StartingConsole.Clear();

    Game.Instance.StartingConsole.Children.Add(new LoadingPanel(host.ScreenCellsX, host.ScreenCellsY));

    await Task.Delay(1000);

    await engine.InitializeAsync();
}
