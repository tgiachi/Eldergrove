using System.Diagnostics;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Maps;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Map;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using Microsoft.Extensions.Logging;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace Eldergrove.Engine.Core.Generators.Base;

public abstract class AbstractMapGenerator : IMapGenerator
{
    protected ILogger _logger;

    private readonly ITileService _tileService;

    private readonly IMapGenService _mapGenService;


    private GlyphTileEntry _wallTile;

    private GlyphTileEntry _floorTile;

    private Generator _generator;

    private MapGeneratorObject _mapGeneratorObject;


    protected AbstractMapGenerator(ILogger logger, ITileService tileService, IMapGenService mapGenService)
    {
        _logger = logger;
        _tileService = tileService;
        _mapGenService = mapGenService;
    }


    public async Task<GameMap> GenerateMapAsync(
        MapGeneratorObject generatorObject, Point mapSize, MapGeneratorType generatorType
    )
    {
        _mapGeneratorObject = generatorObject;
        _wallTile = _tileService.GetTileEntry(generatorObject.Wall);
        _floorTile = _tileService.GetTileEntry(generatorObject.Floor);


        _logger.LogDebug("Generating map with size {MapSize}", mapSize);

        _generator = BuildGenerator(mapSize.X, mapSize.Y);

        var stopWatch = Stopwatch.StartNew();
        _generator.Generate();

        _logger.LogDebug("Map generated in {ElapsedMilliseconds}ms", stopWatch.ElapsedMilliseconds);

        var map = new GameMap(mapSize.X, mapSize.Y, null)
        {
            GeneratorType = generatorType
        };

        await BuildWallsIfExistsAsync(map);
        await PostGenerationAsync(map);

        return map;
    }




    /// <summary>
    ///  Gets the first component of the specified type from the generator context
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    protected TComponent GetGeneratorContext<TComponent>() where TComponent : class =>
        _generator.Context.GetFirst<TComponent>();


    /// <summary>
    ///  Post generation hook for additional processing
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    protected virtual Task PostGenerationAsync(GameMap map)
    {
        return Task.CompletedTask;
    }


    /// <summary>
    ///   Builds walls if they exist in the generator context
    /// </summary>
    /// <param name="map"></param>
    protected virtual async Task BuildWallsIfExistsAsync(GameMap map)
    {
        var wallFloors = _generator.Context.GetFirstOrDefault<ISettableGridView<bool>>("WallFloor");

        if (wallFloors != null)
        {
            map.ApplyTerrainOverlay(
                wallFloors,
                (pos, val) =>
                    val
                        ? new TerrainGameObject(pos, _floorTile.ColoredGlyph, _floorTile.Tile.Id)
                        : new TerrainGameObject(pos, _wallTile.ColoredGlyph, _wallTile.Tile.Id, false)
            );
        }
    }

    protected Generator BuildGenerator(int width, int height)
    {
        var generator = new Generator(width, height);

        generator.ConfigAndGenerateSafe(
            gen => { gen.AddSteps(GetGeneratorSteps()); }
        );

        return generator;
    }

    protected virtual IEnumerable<GenerationStep> GetGeneratorSteps()
    {
        return [];
    }



    public virtual Task PopulateMapAsync(GameMap map)
    {
        var generatedFabricLayersData = new List<GeneratedFabricLayersData>();

        foreach (var fab in _mapGeneratorObject.Fabrics)
        {
            _logger.LogDebug("Placing object with strategy {Strategy}", fab.Placement);

            switch (fab.Placement)
            {
                case MapPlacementStrategyType.Random:
                    generatedFabricLayersData.AddRange(PlaceFabricRandomly(map, fab));
                    break;
                case MapPlacementStrategyType.FreeSpace:
                    break;
                case MapPlacementStrategyType.Adjacent:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        map.AddGeneratedFabricLayersData(generatedFabricLayersData.ToArray());

        return Task.CompletedTask;
    }

    private IEnumerable<GeneratedFabricLayersData> PlaceFabricRandomly(GameMap map, FabricPlaceDataObject fabricPlace)
    {
        var listOfFabricObjects = fabricPlace.GetRandomValueAsRange()
            .Select(_ => _mapGenService.BuildGameObject(fabricPlace.Id, Point.None))
            .Select(fabric => _mapGenService.GenerateFabricAsync(fabric, _wallTile, _floorTile, map))
            .ToList();

        _logger.LogDebug("Generated {Count} fabric objects for {FabricId}", listOfFabricObjects.Count, fabricPlace.Id);


        return listOfFabricObjects;
    }
}