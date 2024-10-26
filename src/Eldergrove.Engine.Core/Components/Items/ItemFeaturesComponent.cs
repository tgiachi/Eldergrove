using Eldergrove.Engine.Core.Contexts;
using Eldergrove.Engine.Core.Data.Internal;
using Eldergrove.Engine.Core.GameObject;
using Eldergrove.Engine.Core.Interfaces.Services;
using SadRogue.Integration.Components;

namespace Eldergrove.Engine.Core.Components.Items;

public class ItemFeaturesComponent : RogueLikeComponentBase<ItemGameObject>
{
    public List<ItemFeatureComponentData> AvailableFeatures { get; } = new();



    private readonly IItemService _itemService;

    public ItemFeaturesComponent(IItemService itemService) : base(false, false, false, false)
    {
        _itemService = itemService;
    }

    public void AddFeature(string name, string description, object parameters)
    {
        AvailableFeatures.Add(new ItemFeatureComponentData(name, description, parameters));
    }

    public void AddFeature(ItemFeatureComponentData feature)
    {
        AvailableFeatures.Add(feature);
    }

    public void ExecuteFeature(string name)
    {
        var feature = AvailableFeatures.FirstOrDefault(f => f.Name == name);
        if (feature == null)
        {
            throw new InvalidOperationException($"Feature {name} not found on item {Parent.Name}");
        }

        _itemService.ExecuteFeature(name, new ItemFeatureContext(Parent, name, feature.Parameters));
    }
}
