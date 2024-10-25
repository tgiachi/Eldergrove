using SadConsole;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class PortalPropGameObject : PropGameObject
{
    public string Name { get; set; }

    public string MapGeneratorId { get; set; }

    public Point Size { get; set; }

    public string DestinationMapId { get; set; }

    public string SourceMapId { get; set; }

    public PortalPropGameObject(Point position, ColoredGlyph appearance) : base(position, appearance, true)
    {
    }
}
