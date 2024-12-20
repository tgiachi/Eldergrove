using Eldergrove.Engine.Core.Components.Npcs;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
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

    private readonly IMapGenService _mapGenService;

    public readonly SurfaceComponentFollowTarget ViewLock;

    public GameObject(IEldergroveEngine eldergroveEngine, int width, int height)
    {
        _eldergroveEngine = eldergroveEngine;

        _mapGenService = _eldergroveEngine.GetService<IMapGenService>();

        _npcService = _eldergroveEngine.GetService<INpcService>();
        _keyActionCommandService = _eldergroveEngine.GetService<IKeyActionCommandService>();

        _currentMap = _eldergroveEngine.GetService<IMapGenService>().CurrentMap;
        _schedulerService = _eldergroveEngine.GetService<ISchedulerService>();


        var viewPort = ViewportUtils.CalculateViewport(
            new Point(width, height),
            EldergroveState.DefaultUiFont.GlyphWidth,
            EldergroveState.DefaultUiFont.GlyphHeight,
            EldergroveState.DefaultMapFont.GlyphWidth,
            EldergroveState.DefaultMapFont.GlyphHeight
        );

        viewPort = new(viewPort.X, viewPort.Y - 1);

        _currentMap.DefaultRenderer = _currentMap.CreateRenderer(viewPort, EldergroveState.DefaultMapFont);
        Position = new Point(0, 1);


        Children.Add(_currentMap);


        var randomProp = _mapGenService.GetEntities<NpcGameObject>(MapLayerType.Npc).RandomElement();

        var point = randomProp.Position + new Point(1, 0);

        _npcService.BuildPlayer(_currentMap);
        _currentMap.AddEntity(_npcService.Player);


        IsFocused = true;
        UseKeyboard = true;

        ViewLock = new SurfaceComponentFollowTarget() { Target = _npcService.Player };

        _currentMap.DefaultRenderer?.SadComponents.Add(ViewLock);

        _npcService.Player.AllComponents.GetFirstOrDefault<PlayerFOVController>().CalculateFOV();

        //_currentMap.AllComponents.Add(ViewLock);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        return _keyActionCommandService.ExecuteKeybinding("map", keyboard.ToKeybindingData());
    }
}
