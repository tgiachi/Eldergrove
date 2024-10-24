using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.Generators.Base;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using GoRogue.MapGeneration;
using GoRogue.MapGeneration.ContextComponents;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Generators;

public class RoomMapGenerator : AbstractMapGenerator
{
    private DoorList _doors;
    private ItemList<Rectangle> _rooms;

    private readonly IPropService _propService;
    private readonly INpcService _npcService;

    public RoomMapGenerator(
        ILogger<RoomMapGenerator> logger, ITileService tileService, IMapGenService mapGenService, IPropService propService,
        INpcService npcService
    ) : base(
        logger,
        tileService,
        mapGenService
    )
    {
        _propService = propService;
        _npcService = npcService;
    }


    protected override IEnumerable<GenerationStep> GetGeneratorSteps() =>
        DefaultAlgorithms.BasicRandomRoomsMapSteps();


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
