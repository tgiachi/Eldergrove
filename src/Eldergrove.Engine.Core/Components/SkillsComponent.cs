using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Types;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class SkillsComponent : RogueLikeComponentBase<NpcGameObject>
{
    public int Health { get; set; }

    public int Strength { get; set; }

    public int Dexterity { get; set; }

    public int Intelligence { get; set; }

    public int Wisdom { get; set; }

    public int Constitution { get; set; }

    public int Charisma { get; set; }

    public NpcAlignmentType Alignment { get; set; }

    public int Gold { get; set; }

    public SkillsComponent() : base(false, false, false, false)
    {
    }
}
