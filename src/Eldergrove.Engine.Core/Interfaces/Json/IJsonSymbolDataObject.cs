namespace Eldergrove.Engine.Core.Interfaces.Json;

public interface IJsonSymbolDataObject
{
    string Symbol { get; set; }

    string? Foreground { get; set; }

    string? Background { get; set; }

    bool IsBlocking { get; set; }

    bool IsTransparent { get; set; }
}
