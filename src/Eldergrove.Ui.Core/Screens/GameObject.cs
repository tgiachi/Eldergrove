using Eldergrove.Engine.Core.Actions.Player;
using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using SadRogue.Primitives;

namespace Eldergrove.Ui.Core.Screens;

public class GameObject : ScreenObject
{
    private readonly IEldergroveEngine _eldergroveEngine;

    private readonly GameMap _currentMap;


    private readonly ISchedulerService _schedulerService;

    private readonly IKeyActionCommandService _keyActionCommandService;

    private readonly INpcService _npcService;

    public readonly SurfaceComponentFollowTarget ViewLock;

    public GameObject(IEldergroveEngine eldergroveEngine, int width, int height)
    {
        _eldergroveEngine = eldergroveEngine;

        _npcService = _eldergroveEngine.GetService<INpcService>();
        _keyActionCommandService = _eldergroveEngine.GetService<IKeyActionCommandService>();

        _currentMap = _eldergroveEngine.GetService<IMapGenService>().CurrentMap;
        _schedulerService = _eldergroveEngine.GetService<ISchedulerService>();

        _npcService.BuildPlayer(new Point(10, 10));
        _currentMap.AddEntity(_npcService.Player);


        _currentMap.DefaultRenderer = _currentMap.CreateRenderer((width, height));

        Children.Add(_currentMap);

        IsFocused = true;
        UseKeyboard = true;

        ViewLock = new SurfaceComponentFollowTarget() { Target = _npcService.Player };

        _currentMap.DefaultRenderer?.SadComponents.Add(ViewLock);

        //_currentMap.AllComponents.Add(ViewLock);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        return _keyActionCommandService.ExecuteKeybinding("map", keyboard.ToKeybindingData());

        // if (keyboard.IsKeyPressed(Keys.Up))
        // {
        //     _schedulerService.AddAction(new EntityMovementAction(Direction.Up, _npcService.Player));
        //     _schedulerService.TickAsync();
        //
        //     return true;
        // }
        //
        // if (keyboard.IsKeyPressed(Keys.Down))
        // {
        //     _schedulerService.AddAction(new EntityMovementAction(Direction.Down, _npcService.Player));
        //     _schedulerService.TickAsync();
        //
        //     return true;
        // }
        //
        // if (keyboard.IsKeyPressed(Keys.Left))
        // {
        //     _schedulerService.AddAction(new EntityMovementAction(Direction.Left, _npcService.Player));
        //     _schedulerService.TickAsync();
        //
        //     return true;
        // }
        //
        // if (keyboard.IsKeyPressed(Keys.Right))
        // {
        //     _schedulerService.AddAction(new EntityMovementAction(Direction.Right, _npcService.Player));
        //     _schedulerService.TickAsync();
        //
        //     return true;
        // }
        //
        // return false;
    }
}
