using Eldergrove.Engine.Core.Data.Game;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Manager;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Ui.Core.Controls;
using Eldergrove.Ui.Core.Interfaces;
using Eldergrove.Ui.Core.Screens;
using Eldergrove.Ui.Core.Services;
using SadConsole;
using SadConsole.Configuration;
using SadRogue.Primitives;

Settings.WindowTitle = "";


var rootDirectory = Environment.GetEnvironmentVariable("ELDERGROVE_ROOT_DIRECTORY") ??
                    Path.Join(Directory.GetCurrentDirectory(), "Eldergrove");

var engine = new EldergroveEngine(
    new EldergroveOptions() { RootDirectory = rootDirectory, },
    services => { return services.AddEldergroveService<IBarService, BarService>(); }
);

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
                var topBarControl = new BarControl(
                    host.ScreenCellsX,
                    1,
                    EldergroveState.Engine.GetService<IBarService>().GetBarFromPosition(BarPositionType.Top)
                );

                topBarControl.Position = new Point(0, 0);

                var map = new GameObject(EldergroveState.Engine, host.ScreenCellsX - 50, host.ScreenCellsY - 10);
                map.Position = new Point(0, 1);

                var sideControl = new SideControl(50, host.ScreenCellsY - 1);
                sideControl.Position = new Point(host.ScreenCellsX - 50, 1);


                // Set message log control to bottom
                var messageLogControl = new MessageLogControl(host.ScreenCellsX - 50, 10);


                messageLogControl.Position = new Point(0, host.ScreenCellsY - 10);


                Game.Instance.StartingConsole.Clear();

                Game.Instance.StartingConsole.Children.Clear();


                Game.Instance.StartingConsole.Children.Add(topBarControl);

                Game.Instance.StartingConsole.Children.Add(sideControl);
                Game.Instance.StartingConsole.Children.Add(messageLogControl);
                Game.Instance.StartingConsole.Children.Add(map);
            }
        );

    Game.Instance.StartingConsole.Clear();

    Game.Instance.StartingConsole.Children.Add(new LoadingPanel(host.ScreenCellsX, host.ScreenCellsY));

    await Task.Delay(1000);

    await engine.InitializeAsync();

    Settings.WindowTitle = EldergroveState.Engine.GetService<IScriptEngineService>()
        .GetContextVariable<GameConfig>("game_config")
        .TitleName;
}
