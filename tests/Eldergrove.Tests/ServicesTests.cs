using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.Props;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Manager;

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

        propService.AddProp(new PropObject() { Id = "test", Symbol = "@"});

        var prop = propService.GetProp("test");

        Assert.That(prop, Is.Not.Null);
    }


    [Test]
    public void Test_TileService()
    {

        var tileService = _engine.GetService<ITileService>();

        var testTile = new TileEntry()
        {
            Symbol = "@",
            Id = "t_test",
            Background = "#000000",
            Foreground = "#FFFFFF"
        };

        tileService.AddTile(testTile);

        var tile = tileService.GetTile(testTile);

        Assert.That(tile, Is.Not.Null);

    }
}
