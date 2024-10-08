using Eldergrove.Engine.Core.Data.Json.Data;

namespace Eldergrove.Engine.Core.Interfaces.Json;

public interface IJsonStateObject
{
    JsonSymbolDataObject On { get; set; }

    JsonSymbolDataObject Off { get; set; }
}
