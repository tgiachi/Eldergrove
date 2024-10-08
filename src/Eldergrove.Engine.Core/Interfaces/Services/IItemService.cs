using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using Eldergrove.Engine.Core.Interfaces.Json;
using GoRogue.Factories;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IItemService : IGameObjectFactory<ItemGameObject>
{

    IEnumerable<ItemGameObject> GetRandomItems(IEnumerable<IJsonRandomObject> randomBag);

}
