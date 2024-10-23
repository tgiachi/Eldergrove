using GoRogue.GameFramework;
using SadRogue.Primitives;

namespace Eldergrove.Engine.Core.Interfaces.GameObjects;

public interface IGameObjectFactory<out TFactoryItem> where TFactoryItem : class
{
    TFactoryItem BuildGameObject(string idOrCategory, Point position);
}
