using Eldergrove.Engine.Core.Data.Events;
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


    EldergroveState.ScreenSize = new Point(host.ScreenCellsX, host.ScreenCellsY);


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

                var map = new GameObject(EldergroveState.Engine, host.ScreenCellsX - 30, host.ScreenCellsY - 10);
                map.Position = new Point(0, 1);

                var sideControl = new SideControl(30, host.ScreenCellsY - 1);
                sideControl.Position = new Point(host.ScreenCellsX - 30, 1);


                // Set message log control to bottom
                var messageLogControl = new MessageLogControl(host.ScreenCellsX - 30, 10);


                messageLogControl.Position = new Point(0, host.ScreenCellsY - 10);


                Game.Instance.StartingConsole.Clear();

                Game.Instance.StartingConsole.Children.Clear();


                Game.Instance.StartingConsole.Children.Add(topBarControl);

                Game.Instance.StartingConsole.Children.Add(sideControl);
                Game.Instance.StartingConsole.Children.Add(messageLogControl);
                Game.Instance.StartingConsole.Children.Add(map);
            }
        );

    engine.GetService<IEventDispatcherService>()
        .SubscribeToEvent(
            "gui_pick_up_request",
            o =>
            {
                var message = o as GuiPickUpRequestEvent;

                var pickUp = new PickUpControl(15, 15, message.Player, message.Items);

                // Center of screen
                pickUp.Position = new Point(
                    (host.ScreenCellsX / 2) - (pickUp.Width / 2),
                    (host.ScreenCellsY / 2) - (pickUp.Height / 2)
                );

                Game.Instance.StartingConsole.Children.Add(pickUp);
            }
        );


    Game.Instance.StartingConsole.Clear();

    Game.Instance.StartingConsole.Children.Add(new LoadingPanel(host.ScreenCellsX, host.ScreenCellsY));

    await Task.Delay(1000);

    await engine.InitializeAsync();

    EldergroveState.GameConfig = EldergroveState.Engine.GetService<IScriptEngineService>()
        .GetContextVariable<GameConfig>("game_config");


    Settings.WindowTitle = EldergroveState.GameConfig.TitleName;

    EldergroveState.DefaultUiFont = EldergroveState.GameConfig.Fonts.GuiFont != null
        ? host.LoadFont(EldergroveState.GameConfig.Fonts.GuiFont)
        : Game.Instance.StartingConsole.Font;

    if (EldergroveState.GameConfig.Fonts.MapFont != null)

    {
        EldergroveState.DefaultMapFont = host.LoadFont(EldergroveState.GameConfig.Fonts.MapFont);
    }

    Game.Instance.DefaultFont = EldergroveState.DefaultUiFont;

    engine.SendEngineReady();
}
