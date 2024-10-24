using Eldergrove.Engine.Core.Generators.Base;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using GoRogue.MapGeneration;
using GoRogue.MapGeneration.ContextComponents;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;
using ShaiRandom.Generators;

namespace Eldergrove.Engine.Core.Generators;

public class DungeonMapGenerator : AbstractMapGenerator
{
    private readonly INpcService _npcService;


    private readonly IPropService _propService;

    private DoorList _doors;
    private ItemList<Rectangle> _rooms;

    public DungeonMapGenerator(
        ILogger<DungeonMapGenerator> logger, ITileService tileService, IMapGenService mapGenService, INpcService npcService,
        IPropService propService
    ) :
        base(logger, tileService, mapGenService)
    {
        _npcService = npcService;
        _propService = propService;
    }


    protected override IEnumerable<GenerationStep> GetGeneratorSteps()
    {
        var rng = new MizuchiRandom();
        var minRooms = Math.Max(4, MapArea / 500);
        var maxRooms = Math.Min(10, MapArea / 300);

        var roomMinSize = Math.Max(3, Math.Min(MapSize.X, MapSize.Y) / 10);
        var roomMaxSize = Math.Max(roomMinSize + 1, Math.Min(MapSize.X, MapSize.Y) / 6);

        var maxCreationAttempts = 10 + (MapArea / 1000);
        var maxPlacementAttempts = 10 + (MapArea / 1000);

        return DefaultAlgorithms.DungeonMazeMapSteps(
             rng
            // minRooms: 1,
            // maxRooms: 2,
            // roomMinSize: 5,
            // roomMaxSize: 11,
            // saveDeadEndChance: 0
            // roomMinSize: roomMinSize,
            // roomMaxSize: roomMaxSize,
            // roomSizeRatioX: 1f,
            // roomSizeRatioY: 1f,
            // maxCreationAttempts: maxCreationAttempts,
            // maxPlacementAttempts: maxPlacementAttempts
        );
    }

    protected override Task PostGenerationAsync(GameMap map)
    {
        _rooms = GetGeneratorContext<ItemList<Rectangle>>("Rooms");
        _doors = GetGeneratorContext<DoorList>("Doors");

        return Task.CompletedTask;
    }

    public override Task PopulateMapAsync(GameMap map)
    {
        foreach (var doors in _doors.DoorsPerRoom)
        {
            foreach (var door in doors.Value.Doors)
            {
                var doorGameObject = _propService.BuildGameObject("door", door);

                map.AddEntity(doorGameObject);
            }
        }

        foreach (var room in _rooms)
        {
            var howManyGoblins = Random.Shared.Next(1, 1);

            for (var i = 0; i < howManyGoblins; i++)
            {
                var randomPosition = room.Item.Center;
                var goblinGameObject = _npcService.BuildGameObject("monsters", randomPosition);

                map.AddEntity(goblinGameObject);
            }
        }

        return Task.CompletedTask;
    }
}
