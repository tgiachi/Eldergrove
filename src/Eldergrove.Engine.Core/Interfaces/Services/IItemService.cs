using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Json.Items;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.GameObjects;
using Eldergrove.Engine.Core.Interfaces.Json;
using GoRogue.Factories;

namespace Eldergrove.Engine.Core.Interfaces.Services;

public interface IItemService : IGameObjectFactory<ItemGameObject>
{
    List<ItemGameObject> GetRandomItems(IEnumerable<IJsonRandomObject> randomBag);

    void AddItem(ItemObject item);

    void AddItemFeature(string id, Action<ItemFeatureContext> feature);
}
