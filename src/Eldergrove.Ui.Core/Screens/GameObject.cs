using Eldergrove.Engine.Core.Actions.Player;
using Eldergrove.Engine.Core.Components;
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

    private readonly PlayerGameObject _playerGameObject;

    private readonly ISchedulerService _schedulerService;

    /// <summary>
    /// Component which locks the map's view onto an entity (usually the player).
    /// </summary>
    public readonly SurfaceComponentFollowTarget ViewLock;

    public GameObject(IEldergroveEngine eldergroveEngine, int width, int height)
    {
        _eldergroveEngine = eldergroveEngine;


        _currentMap = _eldergroveEngine.GetService<IMapGenService>().CurrentMap;
        _schedulerService = _eldergroveEngine.GetService<ISchedulerService>();

        _playerGameObject = new PlayerGameObject((1, 1), new ColoredGlyph(Color.Azure, Color.Black, '@'));
        _playerGameObject.GoRogueComponents.Add(new PlayerFOVController());
        _currentMap.AddEntity(_playerGameObject);


        _currentMap.DefaultRenderer = _currentMap.CreateRenderer((width, height));

        Children.Add(_currentMap);

        //        _currentMap
        //            .DefaultRenderer.IsFocused = true;

        IsFocused = true;
        UseKeyboard = true;

        ViewLock = new SurfaceComponentFollowTarget() { Target = _playerGameObject };

        _currentMap.DefaultRenderer?.SadComponents.Add(ViewLock);

        //_currentMap.AllComponents.Add(ViewLock);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        if (keyboard.IsKeyPressed(Keys.Up))
        {
            _schedulerService.AddAction(new EntityMovementAction(Direction.Up, _playerGameObject));
            _schedulerService.TickAsync();

            return true;
        }

        if (keyboard.IsKeyPressed(Keys.Down))
        {
            _schedulerService.AddAction(new EntityMovementAction(Direction.Down, _playerGameObject));
            _schedulerService.TickAsync();

            return true;
        }

        if (keyboard.IsKeyPressed(Keys.Left))
        {
            _schedulerService.AddAction(new EntityMovementAction(Direction.Left, _playerGameObject));
            _schedulerService.TickAsync();

            return true;
        }

        if (keyboard.IsKeyPressed(Keys.Right))
        {
            _schedulerService.AddAction(new EntityMovementAction(Direction.Right, _playerGameObject));
            _schedulerService.TickAsync();

            return true;
        }

        return false;
    }
}
