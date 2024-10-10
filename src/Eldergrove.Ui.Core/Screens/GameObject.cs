using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using SadConsole;
using SadConsole.Components;

namespace Eldergrove.Ui.Core.Screens;

public class GameObject : ScreenObject
{
    private readonly IEldergroveEngine _eldergroveEngine;

    private readonly GameMap _currentMap;

    /// <summary>
    /// Component which locks the map's view onto an entity (usually the player).
    /// </summary>
    public readonly SurfaceComponentFollowTarget ViewLock;

    public GameObject(IEldergroveEngine eldergroveEngine, int width, int height)
    {
        _eldergroveEngine = eldergroveEngine;

        _currentMap = _eldergroveEngine.GetService<IMapGenService>().CurrentMap;

        _currentMap.DefaultRenderer = _currentMap.CreateRenderer((width, height));

        Children.Add(_currentMap);

        _currentMap
            .DefaultRenderer.IsFocused = true;


        _currentMap.PlayerFOV.Calculate(0, 0, 30);

        // ViewLock = new SurfaceComponentFollowTarget { Target =  };

        // _currentMap.DefaultRenderer.Components.Add(ViewLock);
    }
}
