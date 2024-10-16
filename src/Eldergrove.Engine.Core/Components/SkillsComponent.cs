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

    public int Experience { get; set; }

    public int Level { get; set; }

    public NpcAlignmentType Alignment { get; set; }

    public int Gold { get; set; }



    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Parent.OnDie();
        }
    }

    public SkillsComponent() : base(false, false, false, false)
    {
    }
}
