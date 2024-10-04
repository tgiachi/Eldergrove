using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Services;
using Eldergrove.Engine.Core.Types;
using Serilog;

namespace Eldergrove.Tests;

public class DataLoaderTests
{
    private IDataLoaderService _dataLoaderService;

    [SetUp]
    public void Setup()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var directoryConfig = new DirectoryConfig(Path.Join(Path.GetTempPath(), "Eldergrove"));

        CopyDataFiles(directoryConfig[DirectoryType.Data]);

        _dataLoaderService = new DataLoaderService(directoryConfig);

        _dataLoaderService.AddDataType<TileSetObject>("tileset");
    }

    [Test]
    public async Task LoadTests()
    {
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


    [TearDown]
    public async Task TearDown()
    {
        await _dataLoaderService.StopAsync();
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
