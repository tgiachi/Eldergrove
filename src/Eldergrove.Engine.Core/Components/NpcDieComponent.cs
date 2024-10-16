using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Extensions;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Manager;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Utils;
using SadConsole;
using SadRogue.Integration.Components;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Components;

public class NpcDieComponent : RogueLikeComponentBase<NpcGameObject>
{
    private readonly IEldergroveEngine _eldergroveEngine;

    public NpcDieComponent() : base(false, false, false, false)
    {
        Added += OnAdded;
        _eldergroveEngine = EldergroveState.Engine;
    }

    private void OnAdded(object? sender, EventArgs e)
    {
        Parent.Die += OnDie;
    }

    private void OnDie(object? sender, object e)
    {
        var bloodCount = new JsonRandomObject(1, 4).GetRandomValue();
        var positionInRadius = Radius.Circle.PositionsInRadius(Parent.Position, 2).ToList();


        for (var i = 0; i < bloodCount; i++)
        {
            var blood = new PropGameObject(
                positionInRadius.RandomElement(),
                new ColoredGlyph(Color.DarkRed, Color.Transparent, 247)
            );

            Parent.CurrentMap.AddEntity(blood);
        }

        this.Parent.AppearanceSingle.Appearance.Glyph = 'X';

        Parent.GoRogueComponents.Add(new TimedRemoveComponent(TimeSpan.FromSeconds(5)));
    }
}
