using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class NpcGameObject : RogueLikeCell, INamedComponent
{
    public string Name { get; set; }


    public SkillsComponent Skills => GoRogueComponents.GetFirst<SkillsComponent>();


    public NpcGameObject(
        Point position, ColoredGlyph appearance
    ) : base(appearance, (int)MapLayerType.Npc, false, false)
    {
        Position = position;
    }

    protected void MoveTo(Direction direction)
    {
        Position += direction;
    }

    public override string ToString() => $"ID: {ID} Npc: {Name}";
}
