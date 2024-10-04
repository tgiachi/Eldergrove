using System.Text.Json;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using Serilog;

namespace Eldergrove.Engine.Core.Services;

public class DataLoaderService : IDataLoaderService
{
    private readonly ILogger _logger = Log.ForContext<DataLoaderService>();

    private readonly Dictionary<string, List<object>> _loadedData = new();

    private readonly Dictionary<string, Type> _dataTypes = new();

    private readonly string _typeToken = "$type";

    private readonly DirectoryConfig _directoryConfig;

    public DataLoaderService(DirectoryConfig directoryConfig)
    {
        _directoryConfig = directoryConfig;
    }


    public async Task StartAsync()
    {
        var files = DirectoriesUtils.ScanDirectory(_directoryConfig[DirectoryType.Scripts], "*.json").ToArray();
        _logger.Information("Found {FileCount} files to load", files.Length);

        foreach (var file in files)
        {
            _logger.Debug("Loading file {File}", file);
            await LoadJsonDataFileAsync(file);
        }
    }


    private async Task LoadJsonDataFileAsync(string fileName)
    {
        var fileStream = File.OpenRead(fileName);
        var json = (await JsonDocument.ParseAsync(fileStream));

        if (json.RootElement.ValueKind == JsonValueKind.Object)
        {
            ProcessElement(json.RootElement);
        }


        if (json.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in json.RootElement.EnumerateArray())
            {
                ProcessElement(element);
            }
        }
    }

    private void ProcessElement(JsonElement element)
    {
        if (element.TryGetProperty(_typeToken, out var typeProperty))
        {
            _logger.Debug("Processing element with type {Type}", typeProperty.GetString());
            var typeToDeserialize = _dataTypes[typeProperty.GetString()];
            var data = JsonSerializer.Deserialize(
                element.GetRawText(),
                typeToDeserialize,
                JsonUtils.GetDefaultJsonSettings()
            );

            if (_loadedData.ContainsKey(typeProperty.GetString()))
            {
                _loadedData[typeProperty.GetString()].Add(data);
            }
            else
            {
                _loadedData.Add(typeProperty.GetString(), new List<object> { data });
            }
        }
        else
        {
            _logger.Warning("Type property not found in {DataFile}", element.GetRawText());
        }
    }

    public Task StopAsync() => Task.CompletedTask;

    public void AddDataType<T>(string name) where T : class
    {
        _dataTypes.Add(name, typeof(T));
    }
}
