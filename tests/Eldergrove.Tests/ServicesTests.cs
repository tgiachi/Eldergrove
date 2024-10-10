using Eldergrove.Engine.Core.Actions;
using Eldergrove.Engine.Core.Actions.Tests;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Manager;
using SadRogue.Primitives;

namespace Eldergrove.Tests;

public class ServicesTests
{
    private IEldergroveEngine _engine;

    [SetUp]
    public async Task Setup()
    {
        _engine = new EldergroveEngine(
            new EldergroveOptions() { RootDirectory = Path.Join(Path.GetTempPath(), "Eldergrove") }
        );

        await _engine.StartAsync();
        await _engine.InitializeAsync();

        LoadData();
    }


    private void LoadData()
    {
        _engine.GetService<IColorService>().AddColor("black", 0, 0, 0);
        _engine.GetService<IColorService>().AddColor("white", 255, 255, 255);

        _engine.GetService<IItemService>()
            .AddItem(
                new ItemObject()
                {
                    Id = "i_test",
                    Symbol = "t_test",
                    Category = "test",
                }
            );

        _engine.GetService<ITileService>()
            .AddTile(
                new TileEntry()
                {
                    Id = "t_test",
                    Symbol = "@",
                    Background = "black",
                    Foreground = "white"
                }
            );

        _engine.GetService<IPropService>()
            .AddProp(
                new PropObject()
                {
                    Id = "rock_wall",
                    Symbol = "t_test",
                    Category = "walls",
                }
            );

        _engine.GetService<IPropService>()
            .AddProp(
                new PropObject()
                {
                    Id = "closet",
                    Symbol = "t_test",
                    Category = "walls",
                    Container = new List<JsonRandomObject>()
                    {
                        new()
                        {
                            Id = "i_test",
                            Min = 1,
                            Max = 10
                        }
                    }
                }
            );
    }

    [Test]
    public async Task Test_RandomNameGenerator()
    {
        var nameGenerator = _engine.GetService<INameGeneratorService>();

        nameGenerator.AddName("test", "testName");
        nameGenerator.AddName("test", "testName2");
        nameGenerator.AddName("test", "testName3");


        var name = nameGenerator.GenerateName("test");

        Assert.That(name, Is.Not.Null);
    }

    [Test]
    public async Task Test_ActionCommandService()
    {
        var actionCommandService = _engine.GetService<IActionCommandService>();

        actionCommandService.RegisterCommand("test", Assert.Pass);


        actionCommandService.ExecuteCommand("test");
    }


    [Test]
    public void Test_ColorService()
    {
        var colorService = _engine.GetService<IColorService>();

        colorService.AddColor("test", 255, 255, 255);

        var color = colorService.GetColor("test");

        Assert.That(color.A, Is.EqualTo(255));
    }

    [Test]
    public void Test_PropService()
    {
        var propService = _engine.GetService<IPropService>();

        propService.AddProp(new PropObject() { Id = "test", Symbol = "@" });

        var prop = propService.GetPropById("test");

        Assert.That(prop, Is.Not.Null);
    }


    [Test]
    public void Test_TileService()
    {
        var tileService = _engine.GetService<ITileService>();

        var testTile = new TileEntry()
        {
            Symbol = "@",
            Id = "t_test2",
            Background = "#000000",
            Foreground = "#FFFFFF"
        };

        tileService.AddTile(testTile);

        var tile = tileService.GetTile(testTile);

        Assert.That(tile, Is.Not.Null);
    }


    [Test]
    public async Task Test_GenerateTypeScript()
    {
        var scriptEngineService = _engine.GetService<IScriptEngineService>();

        var lua = await scriptEngineService.GenerateDefinitionsAsync();

        Assert.That(lua, Is.Not.Null);
    }

    [Test]
    public void Test_GeneratePropGameObjectById()
    {
        var propService = _engine.GetService<IPropService>();

        var propGameObject = propService.BuildGameObject("rock_wall", new Point(0, 0));

        Assert.That(propGameObject, Is.Not.Null);
    }

    [Test]
    public void Test_GeneratePropGameObjectByCategory()
    {
        var propService = _engine.GetService<IPropService>();

        var propGameObject = propService.BuildGameObject("walls", new Point(0, 0));

        Assert.That(propGameObject, Is.Not.Null);
    }

    [Test]
    public void Test_GeneratePropWithContainer()
    {
        var propService = _engine.GetService<IPropService>();

        var propGameObject = propService.BuildGameObject("closet", new Point(0, 0));

        Assert.That(propGameObject.ItemsContainer.Items.Count > 0, Is.True);
    }


    [Test]
    public async Task Test_SchedulerService()
    {
        var schedulerService = _engine.GetService<ISchedulerService>();

        schedulerService.AddAction(new DummyAction());

        foreach (var _ in Enumerable.Range(0, 10))
        {
            await schedulerService.TickAsync();
        }
    }

    [Test]
    public async Task Test_SchedulerServiceALotOfActions()
    {
        var schedulerService = _engine.GetService<ISchedulerService>();


        foreach (var _ in Enumerable.Range(0, 10))
        {
            schedulerService.AddAction(new DummyAction(Random.Shared.Next(1, 10)));
        }


        foreach (var _ in Enumerable.Range(0, 10))
        {
            await schedulerService.TickAsync();
        }
    }
}

