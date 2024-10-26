using Eldergrove.Engine.Core.GameObject;

namespace Eldergrove.Engine.Core.Contexts;

public record ItemFeatureContext(ItemGameObject Item, string Id, params object[] Parameters);
