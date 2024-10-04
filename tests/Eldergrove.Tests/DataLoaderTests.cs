using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Services;
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

        _dataLoaderService = new DataLoaderService(directoryConfig);
    }

    [Test]
    public async Task LoadTests()
    {
        await _dataLoaderService.StartAsync();

        Assert.Pass();
    }


    [TearDown]
    public async Task TearDown()
    {
        await _dataLoaderService.StopAsync();
    }
}
