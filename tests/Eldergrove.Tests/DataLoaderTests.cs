using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Manager;
using Eldergrove.Engine.Core.Services;
using Eldergrove.Engine.Core.Types;
using Serilog;

namespace Eldergrove.Tests;

public class DataLoaderTests
{
    private IEldergroveEngine _engine;

    [SetUp]
    public async Task Setup()
    {
        _engine = new EldergroveEngine(
            new EldergroveOptions() { RootDirectory = Path.Join(Path.GetTempPath(), "Eldergrove") }
        );

        CopyDataFiles(Path.Join(Path.GetTempPath(), "Eldergrove", "data"));

        await _engine.StartAsync();
        await _engine.InitializeAsync();
    }

   // [Test]
    public async Task LoadTests()
    {
        var _dataLoaderService = _engine.GetService<IDataLoaderService>();
        _dataLoaderService.SubscribeData<TileSetObject>(
            objects =>
            {
                Assert.That(objects.Id, Is.Not.Null);
                return Task.CompletedTask;
            }
        );

        await _dataLoaderService.StartAsync();

        Assert.Pass();
    }


    private static void CopyDataFiles(string destination)
    {
        var source = Path.Join(Directory.GetCurrentDirectory(), "Data");

        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        foreach (var file in Directory.GetFiles(source))
        {
            try
            {
                File.Copy(file, Path.Join(destination, Path.GetFileName(file)));
            }
            catch (Exception e)
            {
                //   Log.Error(e, "Failed to copy file {File}", file);
            }
        }
    }


}
