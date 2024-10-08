using Eldergrove.Engine.Core.Data.Json.TileSet;
using Eldergrove.Engine.Core.Interfaces.Json;
using Eldergrove.Engine.Core.Interfaces.Services.Base;
using SadConsole;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface ITileService : IEldergroveService
{
    ColoredGlyph GetTile(string tileId);

    ColoredGlyph GetTile(IJsonSymbolDataObject tileData);

    (ColoredGlyph glyph, TileEntry tile) GetTileWithEntry(IJsonSymbolDataObject tileData);

    void AddTile(TileEntry tileEntry);
}
