using Eldergrove.Engine.Core.Components;
using Eldergrove.Engine.Core.Interfaces.Actions;
using Eldergrove.Engine.Core.Interfaces.Components;
using Eldergrove.Engine.Core.Types;
using SadConsole;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.GameObject;

public class NpcGameObject : RogueLikeEntity, INamedComponent, IActionableEntity
{
    public string Name { get; set; }


    public SkillsComponent Skills => GoRogueComponents.GetFirst<SkillsComponent>();


    public NpcGameObject(
        Point position, ColoredGlyph appearance
    ) : base(appearance, false, false, (int)MapLayerType.Npc)
    {
        Position = position;
    }

    protected void MoveTo(Direction direction)
    {
        Position += direction;
    }

    public override string ToString() => $"ID: {ID} Npc: {Name}";

    public IEnumerable<ISchedulerAction> TakeTurn()
    {
        if (GoRogueComponents.GetFirstOrDefault<AiComponent>() != null)
        {
            var list = GoRogueComponents.GetFirstOrDefault<AiComponent>().TakeTurn();

            if (list != null)
            {
                return list;
            }
        }

        return new List<ISchedulerAction>();
    }
}
