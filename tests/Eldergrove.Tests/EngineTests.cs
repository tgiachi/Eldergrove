using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Manager;

namespace Eldergrove.Tests;

public class EngineTests
{
    private IEldergroveEngine _engine;

    [SetUp]
    public void Setup()
    {
        _engine = new EldergroveEngine(
            new EldergroveOptions() { RootDirectory = Path.Join(Path.GetTempPath(), "Eldergrove") }
        );
    }

    [Test]
    public async Task InitializeTests()
    {
        await _engine.StartAsync();
        await _engine.InitializeAsync();

        Assert.Pass();
    }
}
