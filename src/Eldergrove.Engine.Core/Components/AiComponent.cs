using Eldergrove.Engine.Core.Ai;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using Eldergrove.Engine.Core.Maps;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components;

public class AiComponent : RogueLikeComponentBase<NpcGameObject>
{
    public string BrainId { get; set; }

    private readonly AiContext _context;

    private readonly INpcService _npcService;

    public AiComponent(INpcService npcService)
        : base(false, false, false, false)
    {
        _npcService = npcService;
        // Only allow one type of AI on an entity at a time
        Added += IncompatibleWith<AiComponent>;

        _context = new AiContext();
    }

    public void TakeTurn()
    {
        if (Parent?.CurrentMap == null)
        {
            return;
        }

        _context.Map = (GameMap)Parent.CurrentMap;
        _npcService.InvokeBrain(BrainId, _context);
    }
}
