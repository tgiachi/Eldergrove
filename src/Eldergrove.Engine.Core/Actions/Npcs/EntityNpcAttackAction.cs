using Eldergrove.Engine.Core.Actions.Base;
using Eldergrove.Engine.Core.Attributes.Actions;
using Eldergrove.Engine.Core.Data.Action;
using Eldergrove.Engine.Core.Data.Events;
using Eldergrove.Engine.Core.Data.Json.Random;
using Eldergrove.Engine.Core.Data.MessageLog;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.State;
using Eldergrove.Engine.Core.Types;
using Eldergrove.Engine.Core.Utils;
using Microsoft.Extensions.Logging;

namespace Eldergrove.Engine.Core.Actions.Npcs;


[SchedulerAction("entity_npc_attack")]
public class EntityNpcAttackAction : AbstractSchedulerAction
{
    private readonly NpcGameObject _source;

    private readonly NpcGameObject _target;

    private readonly ILogger _logger;

    public EntityNpcAttackAction(NpcGameObject source, NpcGameObject target)
    {
        _source = source;
        _target = target;

        _logger = EldergroveState.Engine.GetLogger<EntityNpcAttackAction>();
    }

    public override async Task<ActionResult> ExecuteAsync()
    {
        if (_target.Skills.IsAlive)

        {
            var name = _source.Name ?? "Player";

            var damage = new JsonRandomObject(1, 3).GetRandomValue();

            _logger.LogInformation(
                "{Name} attacks {Target} for {Damage} damage (Life: {Life})",
                name,
                _target.Name,
                damage,
                _target.Skills.Health
            );

            SendEventMessage(
                new MessageLogEvent(
                    new MessageLogData($"{name} attacks {_target.Name} for {damage} damage", MessageLogType.Attack)
                )
            );


            _target.Skills.TakeDamage(damage);


            return ActionResult.Succeed();
        }

        return ActionResult.Fail();
    }
}
