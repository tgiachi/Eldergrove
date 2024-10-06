using Eldergrove.Engine.Core.Data.Internal;
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
    public async Task TestRandomNameGenerator()
    {
        var nameGenerator = _engine.GetService<INameGeneratorService>();

        nameGenerator.AddName("test", "testName");
        nameGenerator.AddName("test", "testName2");
        nameGenerator.AddName("test", "testName3");


        var name = nameGenerator.GenerateName("test");

        Assert.That(name, Is.Not.Null);
    }

    [Test]
    public async Task Test_ActionCommandTests()
    {
        var actionCommandService = _engine.GetService<IActionCommandService>();

        actionCommandService.RegisterCommand("test", Assert.Pass);


        actionCommandService.ExecuteCommand("test");
    }
}
